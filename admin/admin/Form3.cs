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
    public partial class ProductItem : Form
    {
        public ProductItem()
        {
            InitializeComponent();
        }

        public void edit_Click(object sender, EventArgs e, MySqlConnection conn, int sessionId)
        {
            MyProduct table = new MyProduct();
            table.id = sessionId;
            int st = table.Read(conn);
            Status.HandleCode(st);
            admin.Form7 editForm = new admin.Form7();
            if (st == Status.NO_ERROR) // successfull
            {
                editForm.category.Text = table.cat.name;
                editForm.price.Text = table.price.ToString();
                editForm.description.Text = table.desc;
                editForm.ShowDialog();
            }

            // Here handle the click event of the edit confirm button
            editForm.confirm.Click += new EventHandler((s, ev) => editForm.confirm_Click(s, ev, conn, sessionId));
        }
    }
}
