using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace Gestion_e_commerce
{
    public class Categorie
    {
        public int id;
        public string nom;
    }

    public class Produit
    {
        public int id;
        public string nom;
        public string description;
        public string addedAt;
        public int prix;

        public static void Initialise()
        {
            try
            {
                Console.WriteLine("Trying to read data ...");

                using (SqlConnection connection = new SqlConnection("Data Source=THOMASHAMAM922E;Initial Catalog=ecommerce_projet_db;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from categorie";
                    command.CommandTimeout = 15;
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    int miseenpage;
                    int n;
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
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erreur d'accès à la base de données ("
                + ex.Message + ")");
            }
            }
            /*public void Initialise(Produit P)
            {
                this.nom = P.nom;
                this.description = P.description;
                this.addedAt = P.addedAt;
                this.prix = P.prix;
            }

            public void Initialise(int id, string nom, string description, string addedAt, int prix)
            {
                this.nom = nom;
                this.description = description;
                this.addedAt = addedAt;
                this.prix = prix;
                this.id = id;
            }*/

            void Lire()
            {
                Console.WriteLine("[{0}, {1}, {2}, {3}]", nom, description, addedAt, prix); // Formater la date
                                                                                            // Ajouter les affichages sur l'interface graphique      
            }

            void Ajouter(SqlConnection connexionSql, Categorie cat)
            {

            }

            void Ajouter(SqlConnection connexionSql, string nom, string description, string prix, Categorie categorie)
            {

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

                    using (SqlConnection connection = new SqlConnection("Data Source=THOMASHAMAM922E;Initial Catalog=ecommerce_projet_db;Integrated Security=True"))
                    {
                        connection.Open();
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
                        connection.Close();
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Erreur d'accès à la base de données ("
                    + ex.Message + ")");
                }
            }

        }
}
