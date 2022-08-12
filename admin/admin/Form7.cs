using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using admin_db;

namespace admin
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        public void confirm_Click(object sender, EventArgs e, MySqlConnection conn, int sessionId)
        {
            MyProduct product = new MyProduct();
            // Check if the new values are correct
            bool categoryNameExists = MyCategorie.FindName(conn, this.category.Text);
            if (!categoryNameExists)
            {
                // We can edit table with new data
                int st = product.Edit(conn);
                Status.HandleCode(st);
            }
        }
    }
}
