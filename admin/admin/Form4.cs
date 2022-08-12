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
using System.Text.RegularExpressions;

namespace admin
{
    public partial class adminLogin : Form
    {
        public adminLogin()
        {
            InitializeComponent();
        }

        public static class Session
        {
            public static int userId;
        }

        public static int sessionId = -1;

        public void confirm_Click(object sender, EventArgs e, MySqlConnection connection)
        {
            if (this.password.Text != "" && this.username.Text != "")
            {
                // Call the db request to authenticate ...
                MyUser table = new MyUser();
                // Setting credentials ...
                table.username = this.username.Text;
                table.password = this.password.Text;
                bool authenticated = table.AuthenticateAdmin(connection);
                if (authenticated)
                {
                    Session.userId = table.id;
                    adminLogin.sessionId = table.id;
                    Console.WriteLine("Authenticated !");
                    // Displaying the success
                    Status.ShowMessage(true, "Authentification réussie");
                    Console.WriteLine("user id : {0}", Session.userId);
                    Console.WriteLine("Closing login window ...");
                    this.Hide();
                    Console.WriteLine("Login window closed !");
                    // Add confirm click event

                    admin.Dashboard f = new admin.Dashboard();

                    // User needs to authenticate to continue ...

                    // Set fixed sizes to control form f

                    Console.WriteLine("Trying to list users ...");
                    MyUser.ListUsers(connection);
                    Console.WriteLine("Ended users listing !");
                    Console.WriteLine("Starting to display the lists ...");

                    // Display lists
                    Product.DisplayAllProducts(f.Products, connection);
                    Categorie.DisplayAllCategories(f.Categories, connection);
                    User.DisplayUsers(f.Users, connection);
                    Console.WriteLine("Ended listings !");
                    Console.WriteLine("Displaying widget ...");
                    f.Categories.DoubleClick += new EventHandler((s, ev) => f.categories_DoubleClick(s, ev, connection, Session.userId));
                    f.Users.DoubleClick += new EventHandler((s, ev) => f.users_DoubleClick(s, ev, connection, Session.userId));
                    f.confirm.Click += new EventHandler((s, ev) => f.button1_Click(s, ev, connection, Session.userId));
                    f.Products.DoubleClick += new EventHandler((s, ev) => f.products_DoubleClick(s, ev, connection, Session.userId));
                    f.ShowDialog();
                    Console.WriteLine("Widget displayed !");
                }

                else
                {
                    Console.WriteLine("Authentication failed.");
                    // Generate an error message
                    Status.ShowMessage(false, "Echec de la connexion");
                }
            }
        }

        private void adminLogin_Load(object sender, EventArgs e)
        {

        }
    }

    public class User
    {
        public int nbProducts;
        public int nbCategories;
        public string name; // Required
        public int id; // Required
        public string email; // Required
        public DateTime birth; // Required
        public string firstname; // Required
        public string lastname; // Required
        public bool seller;
        public bool buyer;

        public static string BoolToIntStr(bool val)
        {
            if (val == true) return "1";
            else return "0";
        }

        public static int sessionId = -1;

        public static bool StrIntToBool(string val)
        {
            return val == "1";
        }

        public static void DisplayUsers(System.Windows.Forms.ListBox Obj, MySqlConnection conn)
        {
            try
            {
                string sql = "select * from utilisateur";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Obj.Items.Add(rdr["pseudo"].ToString());
                }

                rdr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class Categorie
    {
        public int id;
        public string name;

        public static void DisplayAllCategories(System.Windows.Forms.ListBox Obj, MySqlConnection conn)
        {
            try
            {
                string sql = "select * from categorie";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Obj.Items.Add(rdr["nom"].ToString());
                }

                rdr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class Product
    {
        public int id;
        public string name;
        public string desc;
        public string addedAt;
        public double price;
        public Categorie cat;

        public void Initialise()
        {
            this.cat = new Categorie();
        }

        public void Read()
        {
            Console.WriteLine("[{0}, {1}, {2}, {3}]", name, desc, addedAt, price); // Formater la date                                                                      // Ajouter les affichages sur l'interface graphique      
        }

        public static void DisplayAllProducts(System.Windows.Forms.ListBox Obj, MySqlConnection conn)
        {
            try
            {
                string sql = "select * from produit";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Obj.Items.Add(rdr["nom"].ToString());
                }

                rdr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string ToPointStr(string str)
        {
            string mod = @"\s*,\s*";
            Regex reg = new Regex(mod);
            string[] nmbrs = reg.Split(str);
            if (nmbrs.Length == 2)
            {
                return (nmbrs[0] + "." + nmbrs[1]);
            }

            else
            {
                Console.WriteLine("Erreur de données.");
                return "";
            }
        }
    }
}
