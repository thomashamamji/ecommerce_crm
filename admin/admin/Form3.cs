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

        public void edit_Click(object sender, EventArgs e, MySqlConnection conn, int productId)
        {
            Console.WriteLine("Edit button clicked from the product windows.");
            MyProduct table = new MyProduct();
            table.id = productId;
            int st = table.ReadFromId(conn);
            Status.HandleCode(st);
            if (st < Status.NO_ERROR) return; // Interrupt the process
            st = table.GetCategorie(conn);
            Status.HandleCode(st);
            if (st < Status.NO_ERROR) return; // Interrupt the process
            admin.Form7 editForm = new admin.Form7();
            editForm.category.Text = table.cat.name;
            editForm.price.Text = table.price.ToString();
            editForm.description.Text = table.desc;

            // Here handle the click event of the edit confirm button
            editForm.confirm.Click += new EventHandler((s, ev) => editForm.confirm_Click(s, ev, conn, productId));

            editForm.ShowDialog();
        }
    }
}
