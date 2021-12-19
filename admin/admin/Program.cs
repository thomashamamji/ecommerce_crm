using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace Gestion_e_commerce
{
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

        public void Add(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("insert into users(username, email, firstname, lastname, birth) values('{0}', '{1}', '{2}', '{3}', '{4}')", this.name, this.email, this.firstname, this.lastname, this.birth);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public void Delete(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("delete from users(email) values ('{0}')", this.email);
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

        public static User[] ListUsers (SqlConnection connection)
        {
            User[] users = new User[100];
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("select * from users");
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.Write("\n");
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    users[i - 1].firstname = reader.GetValue(reader.GetOrdinal("firstname")).ToString();
                    users[i - 1].lastname = reader.GetValue(reader.GetOrdinal("lastname")).ToString();
                    var cultureInfo = new CultureInfo("fr-FR");
                    users[i - 1].birth = DateTime.Parse(reader.GetValue(reader.GetOrdinal("birth")).ToString(), cultureInfo);
                    users[i - 1].name = reader.GetValue(reader.GetOrdinal("username")).ToString();
                    users[i - 1].email = reader.GetValue(reader.GetOrdinal("email")).ToString();
                }
            }

            return users;
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

        public void listCategories (SqlConnection connection)
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
                    if (i == 1) miseenpage = 20;
                    else miseenpage = reader.GetName(i).Length + 2;
                    Console.Write(reader[i].ToString().PadRight(miseenpage, ' ') + "\t");
                }
            }

            while (reader.Read())
            {
                Console.Write("\n");
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    if (i == 1) miseenpage = 20;
                    else miseenpage = reader.GetValue(i).ToString().Length + 2;
                    Console.Write(reader[i].ToString().PadRight(miseenpage, ' ') + "\t");
                }
            }

            reader.Close();
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

                admin.productCategorie f = new admin.productCategorie();
                System.Windows.Forms.ListBox lu = f.Users;
                System.Windows.Forms.ListBox lp = f.Products;
                System.Windows.Forms.ListBox lc = f.Categories;

                // Set fixed sizes to control form f
                // ...

                using (SqlConnection connection = new SqlConnection("Data Source=THOMASHAMAM922E;Initial Catalog=ecommerce_projet_db;Integrated Security=True"))
                {
                    connection.Open();
                    User[] users = new User[100];
                    users = User.ListUsers(connection); // Needs tests
                    lu.Items.AddRange(users); // Needs tests
                    Categorie cat = new Categorie();
                    Product prod = new Product();
                    prod.name = "Strawberry";
                    prod.desc = "The best fruit you can eat !";
                    prod.price = 1.99;
                    cat.name = "fruits";
                    cat.ReadId(connection);
                    Console.WriteLine("Id categorie : {0}", cat.id);
                    prod.cat = new Categorie();
                    prod.cat.id = cat.id;
                    prod.Add(connection);
                    cat.listCategories(connection);
                    f.ShowDialog();
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
