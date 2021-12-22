using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace admin
{
    public partial class productCategorie : Form
    {
        public productCategorie()
        {
            InitializeComponent();
        }

        public void ReadProduct(SqlConnection connection)
        {
            Console.WriteLine("Reader product ...");

            Console.WriteLine("Product read !");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Products_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Events handling
        public void products_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.Products.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(index.ToString());
            }
        }

        public void categories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.Categories.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(index.ToString());
            }
        }

        public void users_DoubleClick(object sender, EventArgs e)
        {
            if (this.Users.SelectedItem != null)
            {
                MessageBox.Show(this.Users.SelectedItem.ToString());
            }
        }
    }
}
