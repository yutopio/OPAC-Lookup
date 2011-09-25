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
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MainWin));
			this.booksBindingNavigator = new BindingNavigator(this.components);
			this.bindingNavigatorAddNewItem = new ToolStripButton();
			this.booksBindingSource = new BindingSource(this.components);
			this.bookDataset = new BookDataset();
			this.bindingNavigatorCountItem = new ToolStripLabel();
			this.bindingNavigatorDeleteItem = new ToolStripButton();
			this.bindingNavigatorMoveFirstItem = new ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new ToolStripButton();
			this.bindingNavigatorSeparator = new ToolStripSeparator();
			this.bindingNavigatorPositionItem = new ToolStripTextBox();
			this.bindingNavigatorSeparator1 = new ToolStripSeparator();
			this.bindingNavigatorMoveNextItem = new ToolStripButton();
			this.bindingNavigatorMoveLastItem = new ToolStripButton();
			this.bindingNavigatorSeparator2 = new ToolStripSeparator();
			this.bindingNavigatorSeparator3 = new ToolStripSeparator();
			this.booksBindingNavigatorOpenItem = new ToolStripButton();
			this.booksBindingNavigatorSaveItem = new ToolStripButton();
			this.booksDataGridView = new DataGridView();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.dataGridViewLinkColumn6 = new DataGridViewLinkColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.saveFileDialog1 = new SaveFileDialog();
			this.openFileDialog1 = new OpenFileDialog();
			((ISupportInitialize)(this.booksBindingNavigator)).BeginInit();
			this.booksBindingNavigator.SuspendLayout();
			((ISupportInitialize)(this.booksBindingSource)).BeginInit();
			((ISupportInitialize)(this.bookDataset)).BeginInit();
			((ISupportInitialize)(this.booksDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// booksBindingNavigator
			// 
			this.booksBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
			this.booksBindingNavigator.BindingSource = this.booksBindingSource;
			this.booksBindingNavigator.CountItem = this.bindingNavigatorCountItem;
			this.booksBindingNavigator.CountItemFormat = "{0} 件のうち";
			this.booksBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
			this.booksBindingNavigator.Items.AddRange(new ToolStripItem[] {
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
			this.booksBindingNavigator.Location = new Point(0, 0);
			this.booksBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.booksBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.booksBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.booksBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.booksBindingNavigator.Name = "booksBindingNavigator";
			this.booksBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
			this.booksBindingNavigator.Size = new Size(1175, 25);
			this.booksBindingNavigator.TabIndex = 0;
			this.booksBindingNavigator.Text = "bindingNavigator1";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new Size(23, 22);
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
			this.bindingNavigatorCountItem.Size = new Size(64, 22);
			this.bindingNavigatorCountItem.Text = "{0} 件のうち";
			this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new Size(23, 22);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new Size(23, 22);
			this.bindingNavigatorMoveFirstItem.Text = "Move first";
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new Size(23, 22);
			this.bindingNavigatorMovePreviousItem.Text = "Move previous";
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new Size(6, 25);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new Size(50, 21);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "Current position";
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
			this.bindingNavigatorSeparator1.Size = new Size(6, 25);
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new Size(23, 22);
			this.bindingNavigatorMoveNextItem.Text = "Move next";
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new Size(23, 22);
			this.bindingNavigatorMoveLastItem.Text = "Move last";
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
			this.bindingNavigatorSeparator2.Size = new Size(6, 25);
			// 
			// bindingNavigatorSeparator3
			// 
			this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
			this.bindingNavigatorSeparator3.Size = new Size(6, 25);
			// 
			// booksBindingNavigatorOpenItem
			// 
			this.booksBindingNavigatorOpenItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.booksBindingNavigatorOpenItem.Image = ((Image)(resources.GetObject("booksBindingNavigatorOpenItem.Image")));
			this.booksBindingNavigatorOpenItem.ImageTransparentColor = Color.Magenta;
			this.booksBindingNavigatorOpenItem.Name = "booksBindingNavigatorOpenItem";
			this.booksBindingNavigatorOpenItem.Size = new Size(23, 22);
			this.booksBindingNavigatorOpenItem.Click += new System.EventHandler(this.booksBindingNavigatorOpenItem_Click);
			// 
			// booksBindingNavigatorSaveItem
			// 
			this.booksBindingNavigatorSaveItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.booksBindingNavigatorSaveItem.Image = ((Image)(resources.GetObject("booksBindingNavigatorSaveItem.Image")));
			this.booksBindingNavigatorSaveItem.Name = "booksBindingNavigatorSaveItem";
			this.booksBindingNavigatorSaveItem.Size = new Size(23, 22);
			this.booksBindingNavigatorSaveItem.Text = "Save Data";
			this.booksBindingNavigatorSaveItem.Click += new System.EventHandler(this.booksBindingNavigatorSaveItem_Click);
			// 
			// booksDataGridView
			// 
			this.booksDataGridView.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
						| AnchorStyles.Left)
						| AnchorStyles.Right)));
			this.booksDataGridView.AutoGenerateColumns = false;
			this.booksDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.booksDataGridView.Columns.AddRange(new DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewLinkColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
			this.booksDataGridView.DataSource = this.booksBindingSource;
			this.booksDataGridView.Location = new Point(12, 28);
			this.booksDataGridView.Name = "booksDataGridView";
			this.booksDataGridView.Size = new Size(1151, 497);
			this.booksDataGridView.TabIndex = 1;
			this.booksDataGridView.CellContentClick += new DataGridViewCellEventHandler(this.booksDataGridView_CellContentClick);
			this.booksDataGridView.DataError += new DataGridViewDataErrorEventHandler(this.booksDataGridView_DataError);
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
			this.dataGridViewTextBoxColumn1.HeaderText = "登録番号";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 110;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
			this.dataGridViewTextBoxColumn2.HeaderText = "タイトル";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 400;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Edition";
			this.dataGridViewTextBoxColumn3.HeaderText = "版";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 70;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "PublishDate";
			this.dataGridViewTextBoxColumn4.HeaderText = "出版日";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 70;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "Language";
			this.dataGridViewTextBoxColumn5.HeaderText = "本文言語";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 80;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "CallNo";
			this.dataGridViewTextBoxColumn9.HeaderText = "請求記号";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Width = 90;
			// 
			// dataGridViewLinkColumn6
			// 
			this.dataGridViewLinkColumn6.DataPropertyName = "Library";
			this.dataGridViewLinkColumn6.HeaderText = "所蔵状況";
			this.dataGridViewLinkColumn6.Name = "dataGridViewLinkColumn6";
			this.dataGridViewLinkColumn6.ReadOnly = true;
			this.dataGridViewLinkColumn6.Width = 70;
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
			// MainWin
			// 
			this.ClientSize = new Size(1175, 537);
			this.Controls.Add(this.booksDataGridView);
			this.Controls.Add(this.booksBindingNavigator);
			this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWin";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "OPAC Lookup";
			((ISupportInitialize)(this.booksBindingNavigator)).EndInit();
			this.booksBindingNavigator.ResumeLayout(false);
			this.booksBindingNavigator.PerformLayout();
			((ISupportInitialize)this.booksBindingSource).EndInit();
			((ISupportInitialize)this.bookDataset).EndInit();
			((ISupportInitialize)this.booksDataGridView).EndInit();
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
		DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		DataGridViewLinkColumn dataGridViewLinkColumn6;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		SaveFileDialog saveFileDialog1;
		ToolStripButton booksBindingNavigatorOpenItem;
		OpenFileDialog openFileDialog1;
	}
}