using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OpacLookup
{
	public partial class MainWin : Form
	{
		const int MaximumConcurrentRequests = 5;

		Semaphore processSemaphore;
		Dictionary<int, Thread> processingThreads;
		Dictionary<int, object> rowLocks;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());
		}

		public MainWin()
		{
			InitializeComponent();

			var versions = (AssemblyInformationalVersionAttribute[])
					Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyInformationalVersionAttribute), false);
			if (versions.Length == 1) this.Text = string.Format("{0} ({1})", this.Text, versions[0].InformationalVersion);

			processSemaphore = new Semaphore(MaximumConcurrentRequests, MaximumConcurrentRequests);
			processingThreads = new Dictionary<int, Thread>();
			rowLocks = new Dictionary<int, object>();
			this.bookDataset.Books.RowChanged += new DataRowChangeEventHandler(_Books_RowChanged);
			this.bookDataset.Books.RowDeleting += new DataRowChangeEventHandler(_Books_RowDeleting);
		}

		void _Books_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			var table = this.bookDataset.Books;
			var row = (BookDataset.BooksRow)e.Row;

			// If the ISBN column is set to null, delete the row.
			if (row.IsISBNNull())
			{
				row.Delete();
				return;
			}

			// If the ISBN column is not changed, ignore this event.
			// This check supresses the multiple recursive event.
			// Changing this line may be harmful.
			if (!row.IsISBNInternalNull() && row.ISBN == row.ISBNInternal) return;

			// If the current row is being processed, stop the thread first.
			if (processingThreads.ContainsKey(row.QueryID))
			{
				processingThreads[row.QueryID].Abort();
				processingThreads.Remove(row.QueryID);
			}

			// Clear the previous erorrs.
			row.ClearErrors();

			// Validate and normalize the user input of ISBN.
			string ISBN = null;
			try { ISBN = Lookup.ValidateISBN(row.ISBN); }
			catch (ArgumentNullException) { row.SetColumnError(table.ISBNColumn, "ISBN を入力してください。"); }
			catch (ArgumentOutOfRangeException) { row.SetColumnError(table.ISBNColumn, "ISBN の形式に誤りがあります。"); }
			catch (ArgumentException) { row.SetColumnError(table.ISBNColumn, "ISBN に誤りがあります。"); }

			if (row.HasErrors)
			{
				// There is an error with ISBN.
				// Suppress the multiple RowChanged event with the ISBN unchanged.
				row.ISBNInternal = row.ISBN;
				return;
			}

			// Refresh the database.
			row.BeginEdit();
			
			row.Output = false;
			row.ISBNInternal = row.ISBN = ISBN;
			row.Title = "待機中";
			row.Edition = null;
			row.PublishDate = null;
			row.Language = null;
			row.CallNo = null;
			row.Library = null;
			row.BibID = null;
			row.NCID = null;
			row.EndEdit();

			// Delete all the related rows in LibraryCollection.
			this.bookDataset.LibraryCollection.Where(x => x.BooksRow == row).ToList().ForEach(x => x.Delete());

			// Start the query.
			var id = row.QueryID;
			var rowLock = new object();
			var t = new Thread(() => SearchStart(ISBN, row, id, rowLock));
			processingThreads.Add(id, t);
			rowLocks.Add(id, rowLock);
			t.IsBackground = true;
			t.Start();
		}

		void _Books_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			var id = ((BookDataset.BooksRow)e.Row).QueryID;
			try
			{
				// Abort the thread.
				lock (rowLocks[id])
					processingThreads[id].Abort();
			}
			catch
			{
				// Even if we couldn't acquire the lock,
				// it only means there is no running thread for the row.
				// We don't have to do anything.
			}
		}

		void SearchStart(string ISBN, BookDataset.BooksRow row, int rowID, object rowLock)
		{
			try
			{
				Thread.Sleep(1000);

			WaitSemaphore:
				Thread.Sleep(100);
				Monitor.Enter(rowLocks[rowID]);

				// Limit the number of threads that query to OPAC.
				if (!processSemaphore.WaitOne(500))
				{
					// If failed to acquire the lock in specified timespan,
					// once release the row lock in order to
					// enable the row to be deleted on _Books_RowDeleting.
					Monitor.Exit(rowLocks[rowID]);
					goto WaitSemaphore;
				}

				try
				{
					// When succeeded to acquire the lock,
					// we must be inside the try clause to release the semaphore
					// when this thread is requested to abort.
					Monitor.Exit(rowLocks[rowID]);

					lock (rowLock) row.Title = "問い合わせ中";

					// Look up the book by ISBN.
					Tuple<List<Tuple<string, string>>, List<Dictionary<string, string>>> result;
					string bibid, ncid;
					try
					{
						// Go search at CiNii Books.
						var records = CiNiiBooks.SearchByISBN(ISBN);
						if (records.Length != 1) throw new ApplicationException("複数件の書籍がヒットしました。");
						result = Lookup.ExtractDataByDetailPage(records[0]);
					}
					catch (ApplicationException exp)
					{
						var message = exp.Message;

						// Trace the most inner exception.
						Exception iExp = exp;
						while (iExp.InnerException != null) iExp = iExp.InnerException;
						if (iExp is ThreadAbortException) message = "処理が中断されました。";

						lock (rowLock) row.Title = row.RowError = message;
						return;
					}

					// Process the data.
					Dictionary<string, string> detail = new Dictionary<string, string>();
					foreach (var field in result.Item1)
						if (!detail.ContainsKey(field.Item1))
							detail.Add(field.Item1, field.Item2);

					// Analyze the codes.
					Lookup.AnalyzeCodeString(detail["CODES"], out bibid, out ncid, out ISBN);

					// Add the library collection.
					var collectionCount = 0;
					var callNoList = new List<string>();
					foreach (var collection in result.Item2)
					{
						// Filter only to Engineer 2 Building Library.
						//if (collection["LOCATION"] != "工2・図書室") continue;
						//if (!collection["LOCATION"].Contains("工") && !collection["LOCATION"].Contains("総合図")) continue;

						// Add to the data table.
						lock (rowLock)
							this.bookDataset.LibraryCollection.AddLibraryCollectionRow(row,
								collection.ContainsKey("LOCATION") ? collection["LOCATION"] : "",
								collection.ContainsKey("VOLUME") ? collection["VOLUME"] : "",
								collection.ContainsKey("CALLNO") ? collection["CALLNO"] : "",
								collection.ContainsKey("BARCODE") ? collection["BARCODE"] : "",
								collection.ContainsKey("CONDITION") ? collection["CONDITION"] : "",
								collection.ContainsKey("RESTRICTION") ? collection["RESTRICTION"] : "",
								collection.ContainsKey("YEAR") ? collection["YEAR"] : "",
								ISBN,
								collection.ContainsKey("COMMENTS") ? collection["COMMENTS"] : "");
						collectionCount++;
						if (!callNoList.Contains(collection["CALLNO"])) callNoList.Add(collection["CALLNO"]);
					}

					// Try to update the data.
					Action<string, Action<string>> TrySetValue = (x, y) => { if (detail.ContainsKey(x)) y(detail[x]); };
					lock (rowLock)
						try
						{
							row.BeginEdit();
							row.Output = true;
							row.Title = detail["Title"];
							TrySetValue("ED", x => row.Edition = x);
							TrySetValue("PUBDT", x => row.PublishDate = x);
							TrySetValue("TXTL", x => row.Language = x);
							if (callNoList.Count != 0) row.CallNo = callNoList[0];
							if (callNoList.Count > 1) row.SetColumnError(this.bookDataset.Books.CallNoColumn, "複数の請求記号が存在します。");
							if (collectionCount > 0) row.Library = collectionCount.ToString() + " 冊";
							row.BibID = bibid;
							row.NCID = ncid;
						}
						finally { row.EndEdit(); }
				}
				finally
				{
					processSemaphore.Release(1);
				}
			}
			finally
			{
				processingThreads.Remove(rowID);
				rowLocks.Remove(rowID);
			}
		}

		void booksDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 7) return;
			var current = (BookDataset.BooksRow)((DataRowView)this.booksBindingSource.Current).Row;

			var collectionWin = new CollectionWin(this.booksBindingSource);
			collectionWin.Text = current.Title;
			collectionWin.ShowDialog(this);
		}

		void booksBindingNavigatorOpenItem_Click(object sender, EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog(this) != DialogResult.OK) return;
			foreach (var file in this.openFileDialog1.FileNames)
			{
				StreamReader sr;
				try { sr = File.OpenText(file); }
				catch (IOException exp)
				{
					MessageBox.Show(this, "次のファイルは開けませんでした。\n" + file + "\n\n" + exp.Message,
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					continue;
				}

				try
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						// Try to extract the first sequence of characters as ISBN.
						if ((line = line.Trim().Replace("-", "")) == string.Empty) continue;

						string ISBN;
						try { ISBN = Lookup.ValidateISBN(line.Substring(0, 13)); }
						catch
						{
							try { ISBN = Lookup.ValidateISBN(line.Substring(0, 10)); }
							catch { continue; }
						}

						// Insert it into the table as it is.
						var row = this.bookDataset.Books.NewBooksRow();
						row.ISBN = ISBN;
						this.bookDataset.Books.AddBooksRow(row);
					}
				}
				finally { sr.Close(); }
			}
		}

		void booksBindingNavigatorSaveItem_Click(object sender, EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;
			bool csv = this.saveFileDialog1.FilterIndex == 1;

			StreamWriter sw = null;
			try
			{
				var fs = this.saveFileDialog1.OpenFile();
				sw = new StreamWriter(fs, Encoding.UTF8);

				var header = "ISBN,タイトル,版,出版日,本文言語,請求記号,所蔵状況,書誌ID,NCID";
				sw.WriteLine(csv ? header : header.Replace(',', '\t'));
				foreach (var row in this.bookDataset.Books.Where(x => x.Output))
					try
					{
						sw.WriteLine(string.Join(csv ? "," : "\t",
							new[] { "ISBN", "Title", "Edition", "PublishDate", "Language", "CallNo", "Library", "BibID", "NCID" }
							.Select(x => row[x]).Select(x => x == DBNull.Value ? null : ((string)x).Replace("\t", "").Replace("\"", "\"\""))
							.Select(x => x == null ? "" : "\"" + x + "\"").ToArray()));
					}
					catch { }
			}
			catch (IOException exp)
			{
				MessageBox.Show(this, "次のファイルに書き込めませんでした。ファイルが読み取り専用になっていないか、またファイルに適切な書き込み権限を持っているか確認してください。\n"
					+ this.saveFileDialog1.FileName + "\n\n" + exp.Message,
					"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally { if (sw != null) sw.Close(); }
		}

		void booksBindingSource_PositionChanged(object sender, EventArgs e)
		{
			// Suppress the InvalidOperationException when there is no row to delete.
			this.bindingNavigatorDeleteItem.Enabled = this.bookDataset.Books.Count != 0;
		}

		void booksDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			var src = this.booksDataGridView.DataSource;
			this.booksDataGridView.DataSource = null;
			this.booksDataGridView.Invalidate();
			this.booksDataGridView.DataSource = src;

			e.Cancel = true;
			e.ThrowException = false;
		}
	}
}
