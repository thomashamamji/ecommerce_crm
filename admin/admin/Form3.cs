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

namespace admin
{
    public partial class ProductItem : Form
    {
        public ProductItem()
        {
            InitializeComponent();
        }

        public void edit_Click(object sender, EventArgs e, MySqlConnection conn, int sessionId)
        {
            admin.Form7 edit = new admin.Form7();
            edit.ShowDialog();

            // Here handle the click event of the edit confirm button
        }
    }
}
