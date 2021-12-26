using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;
using admin_db;

namespace Gestion_e_commerce
{
/*    public class ReadProduct : EventArgs
    {
        public Product Product { get; set; }
        public void ReadProductEventArgs (string name, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("insert into Utilisateur(pseudo, email, prenom, nom, naissance, vendeur, acheteur, createdAt, updatedAt) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, GETDATE(), GETFATE())", this.name, this.email, this.firstname, this.lastname, this.birth, BoolToIntStr(this.buyer), BoolToIntStr(this.seller));
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Product localProduct = new Product();
            localProduct.name = reader.GetValue(reader.GetOrdinal("nom")).ToString();
            localProduct.desc = reader.GetValue(reader.GetOrdinal("description")).ToString();
            localProduct.price = Double.Parse(reader.GetValue(reader.GetOrdinal("prix")).ToString());
            localProduct.addedAt = reader.GetValue(reader.GetOrdinal("createdAt")).ToString();

            // End
            Product = localProduct;
            reader.Close();
        }
    }
*/

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

        public static bool StrIntToBool(string val)
        {
            return val == "1";
        }

        public void Add(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("insert into Utilisateur(pseudo, email, prenom, nom, naissance, vendeur, acheteur, createdAt, updatedAt) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, GETDATE(), GETFATE())", this.name, this.email, this.firstname, this.lastname, this.birth, BoolToIntStr(this.buyer), BoolToIntStr(this.seller));
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public void Delete(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("delete from Utilisateur(email) values ('{0}')", this.email);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public static void DisplayUsers (System.Windows.Forms.ListBox Obj, SqlConnection connection)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from utilisateur";
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                connection.ResetStatistics();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Reading field ...");
                    Obj.Items.Add(reader.GetValue(reader.GetOrdinal("pseudo")).ToString());
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public static void ListUsers (SqlConnection connection)
        {
       
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("select * from utilisateur");
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.Write("\n");
                Console.WriteLine(reader.GetValue(reader.GetOrdinal("nom")));
            }

            reader.Close();

            return;
        }
    }

    public class Categorie
    {
        public int id;
        public string name;

        public void Add(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("insert into categorie(nom) values('{0}')", this.name);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public void Delete(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("delete from categorie where nom='{0}'", this.name);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public void ReadId (SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("select Id_categorie from categorie where nom='{0}'", this.name);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
                this.id = Int32.Parse(reader[0].ToString());
            }
      
            reader.Close();
        }

        public static void DisplayAllCategories (System.Windows.Forms.ListBox Obj, SqlConnection connection)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from categorie";
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Obj.Items.Add(reader.GetValue(reader.GetOrdinal("nom")).ToString());
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ListCategories (SqlConnection connection)
        {
            // Add Try catch
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from categorie";
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                int miseenpage;
                while (reader.Read())
                {
                    Console.Write("\n");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine("Reading value {0} ...", i);
                        if (i == 1) miseenpage = 20;
                        else miseenpage = reader.GetName(i).Length + 2;
                        Console.Write(reader[i].ToString().PadRight(miseenpage, ' ') + "\t");
                    }
                }

                reader.Close();
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

        public void Initialise ()
        {
            this.cat = new Categorie();
        }

        public void Read()
        {
            Console.WriteLine("[{0}, {1}, {2}, {3}]", name, desc, addedAt, price); // Formater la date                                                                      // Ajouter les affichages sur l'interface graphique      
        }

        public static void DisplayAllProducts (System.Windows.Forms.ListBox Obj, SqlConnection connection)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from produit";
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Obj.Items.Add(reader.GetValue(reader.GetOrdinal("nom")).ToString());
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string ToPointStr (string str)
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
        public void Add(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("insert into produit(nom, description, addedAt, prix, Id_categorie) values('{0}', '{1}', GETDATE(), {2}, {3})", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.cat.id);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Manage ecommerce website");
            try
            {
                Console.WriteLine("Trying to read data ...");                

                admin.Dashboard f = new admin.Dashboard();

                // Set fixed sizes to control form f
                // ...

                // I need to add new columns in mssql db

                using (SqlConnection connection = new SqlConnection("Data Source=THOMASHAMAM922E;Initial Catalog=ecommerce_projet_db;Integrated Security=True"))
                {
                    connection.Open();
                    Console.WriteLine("Trying to list users ...");
                    User.ListUsers(connection);
                    Console.WriteLine("Ended users listing !");
                    Console.WriteLine("Starting to display the lists ...");
                    Categorie.DisplayAllCategories(f.Categories, connection);
                    User.DisplayUsers(f.Users, connection);
                    Product.DisplayAllProducts(f.Products, connection);
                    Console.WriteLine("Ended listings !");
                    Console.WriteLine("Displaying widget ...");
                    f.Categories.DoubleClick += new EventHandler((sender, e) => f.categories_DoubleClick(sender, e, connection));
                    f.Users.DoubleClick += new EventHandler((sender, e) => f.users_DoubleClick(sender, e, connection));
                    f.ShowDialog();
                    Console.WriteLine("Widget displayed !");
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erreur d'accès à la base de données (" + ex.Message + ")");
            }
        }

    }
}
