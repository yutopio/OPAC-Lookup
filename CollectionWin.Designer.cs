using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpacLookup
{
    partial class CollectionWin
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
            this.bookDataset = new BookDataset();
            this.libraryCollectionBindingSource = new BindingSource(this.components);
            this.libraryCollectionDataGridView = new DataGridView();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(this.bookDataset)).BeginInit();
            ((ISupportInitialize)(this.libraryCollectionBindingSource)).BeginInit();
            ((ISupportInitialize)(this.libraryCollectionDataGridView)).BeginInit();
            this.SuspendLayout();
            //
            // bookDataset
            //
            this.bookDataset.DataSetName = "BookDataset";
            this.bookDataset.Locale = new System.Globalization.CultureInfo("");
            this.bookDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            //
            // libraryCollectionBindingSource
            //
            this.libraryCollectionBindingSource.DataMember = "LibraryCollection";
            this.libraryCollectionBindingSource.DataSource = this.bookDataset;
            //
            // libraryCollectionDataGridView
            //
            this.libraryCollectionDataGridView.AllowUserToAddRows = false;
            this.libraryCollectionDataGridView.AllowUserToDeleteRows = false;
            this.libraryCollectionDataGridView.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.libraryCollectionDataGridView.AutoGenerateColumns = false;
            this.libraryCollectionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.libraryCollectionDataGridView.Columns.AddRange(new DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.libraryCollectionDataGridView.DataSource = this.libraryCollectionBindingSource;
            this.libraryCollectionDataGridView.Location = new Point(12, 12);
            this.libraryCollectionDataGridView.Name = "libraryCollectionDataGridView";
            this.libraryCollectionDataGridView.ReadOnly = true;
            this.libraryCollectionDataGridView.Size = new Size(901, 261);
            this.libraryCollectionDataGridView.TabIndex = 1;
            this.libraryCollectionDataGridView.KeyDown += new KeyEventHandler(this.libraryCollectionDataGridView_KeyDown);
            //
            // dataGridViewTextBoxColumn2
            //
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Location";
            this.dataGridViewTextBoxColumn2.HeaderText = "所蔵館";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 90;
            //
            // dataGridViewTextBoxColumn3
            //
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Volume";
            this.dataGridViewTextBoxColumn3.HeaderText = "巻次";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 70;
            //
            // dataGridViewTextBoxColumn4
            //
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CallNo";
            this.dataGridViewTextBoxColumn4.HeaderText = "請求記号";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 85;
            //
            // dataGridViewTextBoxColumn5
            //
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Barcode";
            this.dataGridViewTextBoxColumn5.HeaderText = "登録番号";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            //
            // dataGridViewTextBoxColumn6
            //
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Condition";
            this.dataGridViewTextBoxColumn6.HeaderText = "状態";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 110;
            //
            // dataGridViewTextBoxColumn7
            //
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Restriction";
            this.dataGridViewTextBoxColumn7.HeaderText = "利用注記";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            //
            // dataGridViewTextBoxColumn8
            //
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn8.HeaderText = "刷年";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 60;
            //
            // dataGridViewTextBoxColumn9
            //
            this.dataGridViewTextBoxColumn9.DataPropertyName = "ISBN";
            this.dataGridViewTextBoxColumn9.HeaderText = "ISBN";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 90;
            //
            // dataGridViewTextBoxColumn10
            //
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Comments";
            this.dataGridViewTextBoxColumn10.HeaderText = "コメント";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 150;
            //
            // CollectionWin
            //
            this.ClientSize = new Size(925, 285);
            this.Controls.Add(this.libraryCollectionDataGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CollectionWin";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            ((ISupportInitialize)(this.bookDataset)).EndInit();
            ((ISupportInitialize)(this.libraryCollectionBindingSource)).EndInit();
            ((ISupportInitialize)(this.libraryCollectionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        BookDataset bookDataset;
        BindingSource libraryCollectionBindingSource;
        DataGridView libraryCollectionDataGridView;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    }
}
