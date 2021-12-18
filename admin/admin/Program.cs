using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace Gestion_e_commerce
{
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

                /*                admin.ecommerce_admin_platform w = new admin.ecommerce_admin_platform();
                */
                admin.productCategorie f = new admin.productCategorie();

                using (SqlConnection connection = new SqlConnection("Data Source=THOMASHAMAM922E;Initial Catalog=ecommerce_projet_db;Integrated Security=True"))
                {
                    connection.Open();
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
