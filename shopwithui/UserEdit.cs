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
    public partial class UserEdit : Form
    {
        internal UserEdit(Hash_Table hash)
        {
            InitializeComponent();
            PrintUserData(hash);
            AddButton.Click += (sender, EventArgs) => { AddButton_Click(sender, EventArgs, hash); };
            DeleteButton.Click += (sender, EventArgs) => { DeleteButton_Click(sender, EventArgs, hash); };
        }

        private void PrintUserData(Hash_Table hash)
        {
            string logininput = "";
            string locationinput = "";
            int ageinput = 0;
            int x = 0;
            int i = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            while (logininput != "hashended" || ageinput != -1 || locationinput != "hashended")
            {
                logininput = hash.ReturnLogin(x);
                ageinput = hash.ReturnAge(x);
                locationinput = hash.ReturnLocation(x);
                if ((logininput != "hashempty" || ageinput != -2 || locationinput != "hashempty") && (logininput != "hashended" || ageinput != -1 || locationinput != "hashended"))
                {
                    if (logininput != " " && ageinput != 0 && locationinput != " ")
                    {
                        listBox1.Items.Insert(i, logininput);
                        listBox2.Items.Insert(i, locationinput);
                        listBox3.Items.Insert(i, ageinput);
                        i++;
                    }
                }
                x++;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e, Hash_Table hash)
        {
            string Age = AgeBox.Text;
            string Location = LocationBox.Text;
            string Login = LoginBox.Text;
            bool itsint = int.TryParse(Age, out int ageint);
            bool itsnotstring = string.IsNullOrEmpty(Location);
            bool itsnotstring2 = string.IsNullOrEmpty(Login);
            if (itsint && !itsnotstring && !itsnotstring2 && ageint > 0)
            {
                Hash_Node node = new Hash_Node(LoginBox.Text, LocationBox.Text, ageint);
                hash.Add(node);
                PrintUserData(hash);
            }
            else
                MessageBox.Show("Input Is Incorrect", "Error");
        }
        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }
        private void DeleteButton_Click(object sender, EventArgs e, Hash_Table hash)
        {
            string Age = AgeBox.Text;
            string Location = LocationBox.Text;
            string Login = LoginBox.Text;
            bool itsint = int.TryParse(Age, out int ageint);
            bool itsnotstring = string.IsNullOrEmpty(Location);
            bool itsnotstring2 = string.IsNullOrEmpty(Login);
            if (itsint && !itsnotstring && !itsnotstring2 && ageint > 0)
            {
                hash.Delite(LoginBox.Text, Convert.ToInt32(AgeBox.Text));
                PrintUserData(hash);
            }
            else
                MessageBox.Show("Input Is Incorrect", "Error");
                
        }
        private void DeleteButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
