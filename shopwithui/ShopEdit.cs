using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shopwithui
{
    public partial class ShopEdit : Form
    {
        internal ShopEdit(Tree tree)
        {
            InitializeComponent();
            PrintShopData(tree);
            AddButton.Click += (sender, EventArgs) => { AddButton_Click(sender, EventArgs, tree); };
            DeleteButton.Click += (sender, EventArgs) => { DeleteButton_Click(sender, EventArgs, tree); };
        }

        private void PrintShopData(Tree tree)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            tree.NameListOut(tree.Root, listBox1);
            tree.LocationListOut(tree.Root, listBox2);
            tree.ProductListOut(tree.Root, listBox3);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e, Tree tree)
        {
            string shop = ShopBox.Text;
            string location = LocationBox.Text;
            string product = ProductBox.Text;
            bool itsnotstring = string.IsNullOrEmpty(shop);
            bool itsnotstring2 = string.IsNullOrEmpty(location);
            bool itsnotstring3 = string.IsNullOrEmpty(product);
            if (!itsnotstring && !itsnotstring2 && !itsnotstring3)
            {
                tree.Insert(product, shop, location);
                PrintShopData(tree);
            }
            else
                MessageBox.Show("Input Is Incorrect", "Error");
        }
        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }

        private void LocationBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShopBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void DeleteButton_Click(object sender, EventArgs e, Tree tree)
        {
            string shop = ShopBox.Text;
            string location = LocationBox.Text;
            string product = ProductBox.Text;
            bool itsnotstring = string.IsNullOrEmpty(shop);
            bool itsnotstring2 = string.IsNullOrEmpty(location);
            bool itsnotstring3 = string.IsNullOrEmpty(product);
            if (!itsnotstring && !itsnotstring2 && !itsnotstring3)
            {
                tree.Delete(product, shop, location);
                PrintShopData(tree);
            }
            else
                MessageBox.Show("Input Is Incorrect", "Error");
        }
        private void DeleteButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
