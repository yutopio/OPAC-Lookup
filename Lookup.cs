using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OpacLookup
{
	class Lookup
	{
		const string searchUT = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_list.cgi?smode=1&cmode=0&kywd1_exp={0}&con1_exp=12";
		const string searchWebcat = null;
		const string lookupBibid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?lang=0&amode=11&bibid={0}";
		const string lookupNcid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?amode=13&dbname=BOOK&ncid={0}";

		const string resultMarker = "list_result";
		const string bibidMarker = "bibid=";
		const string ncidMarker = "ncid=";

		const string bookDetailTitleBegin = "<title>";
		const string bookDetailTitleEnd = "</title>";
		const string bookDetailEntryBegin = "bb_item_tr";
		const string bookDetailEntryType1 = "<td><span ";
		const string bookDetailEntryType2 = "=\"";
		const string bookDetailEntryValue = "\">";
		const string bookDetailEntryEnd = "</span></td>";

		const string libraryEntryBegin = "bl_item_tr\">";
		const string libraryEntryEnd = "</tr>";

		public static Tuple<List<Tuple<string, string>>, List<Dictionary<string, string>>> SearchByISBN(string ISBN, out string bibID, out string NCID)
		{
			try
			{
				int i, j;
				string detailPage = null;
				try
				{
					// First we look up the book by UT OPAC.
					var c = new WebClient();
					var searchPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(searchUT, ISBN)));
					i = searchPage.IndexOf(resultMarker);
					if (i != -1)
					{
						// If the book is available at UT library, obtain the bibID and download the detailed information.
						i = searchPage.IndexOf(bibidMarker, i) + bibidMarker.Length;
						bibID = searchPage.Substring(i, searchPage.IndexOf('&', i) - i);
						NCID = null;
						detailPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(lookupBibid, bibID)));
					}
					else
					{
						// If unavailable, look up the book from Webcat.
						searchPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(searchWebcat, ISBN)));
						i = searchPage.IndexOf(resultMarker);
						if (i != -1)
						{
							i = searchPage.IndexOf(ncidMarker, i) + ncidMarker.Length;
							bibID = null;
							NCID = searchPage.Substring(i, searchPage.IndexOf('&', i) - i);
							detailPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(lookupNcid, NCID)));
						}
						else
						{
							// Didn't find the book on UT or Webcat.
							throw new ApplicationException("書籍が見つかりませんでした。");
						}
					}

					// Assure no more result hit.
					if (searchPage.IndexOf(resultMarker, i) != -1)
						throw new ApplicationException("複数件の書籍が見つかりました。手動で検索してください。");
				}
				catch (WebException exp) { throw new ApplicationException("OPAC に接続中にエラーが発生しました。ネットワークに関係する問題が発生しています。", exp); }

				// Start to get the detail.
				var detail = new List<Tuple<string, string>>();

				// Obtain the book title.
				i = detailPage.IndexOf(bookDetailTitleBegin) + bookDetailTitleBegin.Length;
				var title = detailPage.Substring(i, detailPage.IndexOf(bookDetailTitleEnd, i) - i);
				detail.Add(new Tuple<string, string>("Title", title));

				// Obtain each detail field of the book.
				while ((j = detailPage.IndexOf(bookDetailEntryBegin, i)) != -1)
				{
					i = detailPage.IndexOf(bookDetailEntryType1, j + bookDetailEntryBegin.Length);
					i = detailPage.IndexOf(bookDetailEntryType2, i) + bookDetailEntryType2.Length;
					j = detailPage.IndexOf(bookDetailEntryValue, i);
					var type = detailPage.Substring(i, j - i);
					j += bookDetailEntryValue.Length;
					i = detailPage.IndexOf(bookDetailEntryEnd, j);
					var value = detailPage.Substring(j, i - j);
					detail.Add(new Tuple<string, string>(type, value));
				}

				// If the book is stored at UT Library, get the location list.
				var collectionList = new List<Dictionary<string, string>>();
				while ((i = detailPage.IndexOf(libraryEntryBegin, i)) != -1)
				{
					j = detailPage.IndexOf(libraryEntryEnd, i += libraryEntryBegin.Length);
					var doc = XDocument.Parse("<A>" + detailPage.Substring(i, j - i) + "</A>");

					var book = new Dictionary<string, string>();
					foreach (var elem in doc.Descendants("td"))
					{
						var fieldName = elem.Attribute("class").Value;
						string fieldValue = null;
						var link = elem.Element("a");
						if (link != null) fieldValue = link.Value;
						else if (elem.Element("br") == null) fieldValue = elem.Value;
						book.Add(fieldName, fieldValue);
					}
					collectionList.Add(book);
				}

				return new Tuple<List<Tuple<string, string>>, List<Dictionary<string, string>>>(detail, collectionList);
			}
			catch (IndexOutOfRangeException) { throw new ApplicationException("OPAC での検索結果の処理中にエラーが発生しました。このプログラムの設計に問題があります。"); }
		}

		public static string ValidateISBN(string code)
		{
			if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");
			code = code.Trim().Replace("-", "");
			if (code.Length == 10)
			{
				for (var i = 0; i < 9; i++)
				{
					var t = code[i] - '0';
					if (t < 0 || t > 9) throw new ArgumentOutOfRangeException("code");
				}
			}
			else throw new ArgumentOutOfRangeException("code");
			return code;
		}

		public static void AnalyzeCodeString(string codes, out string BibID, out string NCID)
		{
			try
			{
				codes = codes.Replace("&nbsp;", " ");
				var nodes = XElement.Parse("<A>" + codes + "</A>").Nodes().ToArray();
				if (nodes.Length == 2)
				{
					BibID = null;
					NCID = nodes[1].ToString();
				}
				else
				{
					BibID = nodes[1].ToString();
					NCID = nodes[3].ToString();
				}
			}
			catch (Exception exp) { throw new ApplicationException("書誌 ID および NCID の取得中にエラーが発生しました。", exp); }
		}
	}
}