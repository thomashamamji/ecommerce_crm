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
using admin_db;
using System.Text.RegularExpressions;

namespace admin
{
    public partial class Dashboard : Form
    {
        public Dashboard()
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

        public void categories_DoubleClick(object sender, EventArgs e, SqlConnection connection)
        {
            if (this.Categories.SelectedItem != null)
            {
                string cat_name = this.Categories.SelectedItem.ToString();
                CategorieTable table = new CategorieTable();
                table.name = cat_name;
                table.Read(connection);
                // Display table.name and table.id values
            }
        }

        public int BoolToInt(bool boolean)
        {
            if (boolean) return 1;
            return 0;
        }

        public string GetAgeStr(string date)
        {
            DateTime d = DateTime.Parse(date);
            DateTime now = DateTime.Now;
            int age = now.Year - d.Year + BoolToInt(now.Month >= d.Month && now.Day >= d.Day);
            return age.ToString();
        }

        public void users_DoubleClick(object sender, EventArgs e, SqlConnection connection)
        {
            if (this.Users.SelectedItem != null)
            {
                string user_lastname = this.Users.SelectedItem.ToString();
                Console.WriteLine("Selected {0} !", user_lastname);
                UserTable table = new UserTable();
                table.lastname = user_lastname;
                table.Read(connection);
                UserItem item = new UserItem();
                item.firstname.Text = table.firstname;
                item.lastname.Text = table.lastname;
                item.age.Text = GetAgeStr(table.bornAt); // Get age from date of birth
                item.username.Text = table.lastname;
                item.email.Text = table.lastname;
                // Must count other tables for the rest
                item.nbProducts.Text = "10";
                item.nbCategories.Text = "3";
                item.nbSells.Text = "10";
                item.nbPurchases.Text = "5";
                item.ShowDialog();
            }
        }
    }
}
