using System;
using System.Windows.Forms;

namespace OpacLookup
{
    public partial class CollectionWin : Form
    {
        public CollectionWin(BindingSource binding)
        {
            InitializeComponent();
            this.libraryCollectionBindingSource.DataSource = binding;
            this.libraryCollectionBindingSource.DataMember = "Books_LibraryCollection";
        }

        void libraryCollectionDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}
