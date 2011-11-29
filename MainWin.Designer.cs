using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpacLookup
{
	partial class MainWin
	{
		IContainer components = null;
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.booksBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.booksBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.bookDataset = new OpacLookup.BookDataset();
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.booksBindingNavigatorOpenItem = new System.Windows.Forms.ToolStripButton();
			this.booksBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
			this.booksDataGridView = new System.Windows.Forms.DataGridView();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EngLibrary = new System.Windows.Forms.DataGridViewLinkColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.NII = new System.Windows.Forms.DataGridViewLinkColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.booksBindingNavigator)).BeginInit();
			this.booksBindingNavigator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bookDataset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// booksBindingNavigator
			// 
			this.booksBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
			this.booksBindingNavigator.BindingSource = this.booksBindingSource;
			this.booksBindingNavigator.CountItem = this.bindingNavigatorCountItem;
			this.booksBindingNavigator.CountItemFormat = "{0} 件のうち";
			this.booksBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
			this.booksBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.bindingNavigatorSeparator3,
            this.booksBindingNavigatorOpenItem,
            this.booksBindingNavigatorSaveItem});
			this.booksBindingNavigator.Location = new System.Drawing.Point(0, 0);
			this.booksBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.booksBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.booksBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.booksBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.booksBindingNavigator.Name = "booksBindingNavigator";
			this.booksBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
			this.booksBindingNavigator.Size = new System.Drawing.Size(1043, 25);
			this.booksBindingNavigator.TabIndex = 0;
			this.booksBindingNavigator.Text = "bindingNavigator1";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorAddNewItem.Text = "Add new";
			// 
			// booksBindingSource
			// 
			this.booksBindingSource.DataMember = "Books";
			this.booksBindingSource.DataSource = this.bookDataset;
			this.booksBindingSource.PositionChanged += new System.EventHandler(this.booksBindingSource_PositionChanged);
			// 
			// bookDataset
			// 
			this.bookDataset.DataSetName = "BookDataset";
			this.bookDataset.Locale = new System.Globalization.CultureInfo("");
			this.bookDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(64, 22);
			this.bindingNavigatorCountItem.Text = "{0} 件のうち";
			this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveFirstItem.Text = "Move first";
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMovePreviousItem.Text = "Move previous";
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "Current position";
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
			this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveNextItem.Text = "Move next";
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveLastItem.Text = "Move last";
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
			this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorSeparator3
			// 
			this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
			this.bindingNavigatorSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// booksBindingNavigatorOpenItem
			// 
			this.booksBindingNavigatorOpenItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.booksBindingNavigatorOpenItem.Image = ((System.Drawing.Image)(resources.GetObject("booksBindingNavigatorOpenItem.Image")));
			this.booksBindingNavigatorOpenItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.booksBindingNavigatorOpenItem.Name = "booksBindingNavigatorOpenItem";
			this.booksBindingNavigatorOpenItem.Size = new System.Drawing.Size(23, 22);
			this.booksBindingNavigatorOpenItem.Click += new System.EventHandler(this.booksBindingNavigatorOpenItem_Click);
			// 
			// booksBindingNavigatorSaveItem
			// 
			this.booksBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.booksBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("booksBindingNavigatorSaveItem.Image")));
			this.booksBindingNavigatorSaveItem.Name = "booksBindingNavigatorSaveItem";
			this.booksBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
			this.booksBindingNavigatorSaveItem.Text = "Save Data";
			this.booksBindingNavigatorSaveItem.Click += new System.EventHandler(this.booksBindingNavigatorSaveItem_Click);
			// 
			// booksDataGridView
			// 
			this.booksDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.booksDataGridView.AutoGenerateColumns = false;
			this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.booksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.EngLibrary,
            this.dataGridViewTextBoxColumn6,
            this.NII,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
			this.booksDataGridView.DataSource = this.booksBindingSource;
			this.booksDataGridView.Location = new System.Drawing.Point(12, 28);
			this.booksDataGridView.Name = "booksDataGridView";
			this.booksDataGridView.Size = new System.Drawing.Size(1019, 376);
			this.booksDataGridView.TabIndex = 1;
			this.booksDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.booksDataGridView_CellContentClick);
			this.booksDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.booksDataGridView_DataError);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "*.csv";
			this.saveFileDialog1.Filter = "カンマ区切りファイル (*.csv)|*.csv|タブ区切りファイル (*.txt)|*.txt";
			this.saveFileDialog1.Title = "保存先のファイルを指定";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "テキスト ファイル (*.txt)|*.txt";
			this.openFileDialog1.Multiselect = true;
			this.openFileDialog1.Title = "ISBN 読み込みファイルの指定";
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "Output";
			this.dataGridViewCheckBoxColumn1.HeaderText = "出力";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Width = 40;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "ISBN";
			this.dataGridViewTextBoxColumn1.HeaderText = "ISBN";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 110;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
			this.dataGridViewTextBoxColumn2.HeaderText = "タイトル";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn2.Width = 400;
			// 
			// EngLibrary
			// 
			this.EngLibrary.DataPropertyName = "EngLibrary";
			this.EngLibrary.HeaderText = "工情所蔵";
			this.EngLibrary.Name = "EngLibrary";
			this.EngLibrary.ReadOnly = true;
			this.EngLibrary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.EngLibrary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.EngLibrary.Width = 80;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "Library";
			this.dataGridViewTextBoxColumn6.HeaderText = "東大所蔵";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewTextBoxColumn6.Width = 80;
			// 
			// NII
			// 
			this.NII.DataPropertyName = "NII";
			this.NII.HeaderText = "NII 登録";
			this.NII.Name = "NII";
			this.NII.ReadOnly = true;
			this.NII.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.NII.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.NII.Width = 80;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "BibID";
			this.dataGridViewTextBoxColumn7.HeaderText = "書誌 ID";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Width = 80;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "NCID";
			this.dataGridViewTextBoxColumn8.HeaderText = "NCID";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Width = 80;
			// 
			// MainWin
			// 
			this.ClientSize = new System.Drawing.Size(1043, 416);
			this.Controls.Add(this.booksDataGridView);
			this.Controls.Add(this.booksBindingNavigator);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "OPAC Lookup";
			((System.ComponentModel.ISupportInitialize)(this.booksBindingNavigator)).EndInit();
			this.booksBindingNavigator.ResumeLayout(false);
			this.booksBindingNavigator.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bookDataset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		BookDataset bookDataset;
		BindingSource booksBindingSource;
		BindingNavigator booksBindingNavigator;
		ToolStripButton bindingNavigatorAddNewItem;
		ToolStripLabel bindingNavigatorCountItem;
		ToolStripButton bindingNavigatorDeleteItem;
		ToolStripButton bindingNavigatorMoveFirstItem;
		ToolStripButton bindingNavigatorMovePreviousItem;
		ToolStripSeparator bindingNavigatorSeparator;
		ToolStripTextBox bindingNavigatorPositionItem;
		ToolStripSeparator bindingNavigatorSeparator1;
		ToolStripButton bindingNavigatorMoveNextItem;
		ToolStripButton bindingNavigatorMoveLastItem;
		ToolStripSeparator bindingNavigatorSeparator2;
		ToolStripSeparator bindingNavigatorSeparator3;
		ToolStripButton booksBindingNavigatorSaveItem;
		DataGridView booksDataGridView;
		SaveFileDialog saveFileDialog1;
		ToolStripButton booksBindingNavigatorOpenItem;
		OpenFileDialog openFileDialog1;
		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private DataGridViewLinkColumn EngLibrary;
		private DataGridViewLinkColumn dataGridViewTextBoxColumn6;
		private DataGridViewLinkColumn NII;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
	}
}