using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;
using MySql.Data.MySqlClient;

namespace admin_db
{
	public class MyCategorie
	{
		public int id;
		public string name;

		public void Add(MySqlConnection conn)
		{
			try
			{
				if (this.name != "") {
					Console.WriteLine("Trying to insert data (categorie) ...");
					string sql = String.Format("insert into categorie(nom) values('{0}')", this.name);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Added categorie successfully !");
				}

				else
				{
					Console.WriteLine("Missing name !");
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public void Delete(MySqlConnection conn)
		{
			try
            {
				if (this.name != "")
                {
					Console.WriteLine("Trying to delete categorie ...");
					string sql = String.Format("delete from categorie where nom='{0}'", this.name);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Deleted categorie successfully !");
				}

				else
                {
					Console.WriteLine("Missing name !");
                }
            }

			catch (Exception ex)
            {
				Console.WriteLine(ex);
            }
		}

		public void ReadId(MySqlConnection conn)
		{
            try
            {
				Console.WriteLine("Trying to read categorie id ...");
				string sql = String.Format("select Id_categorie from categorie where nom='{0}'", this.name);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader(); ;
				while (rdr.Read())
				{
					this.id = Int32.Parse(rdr["Id_categorie"].ToString());
				}

				rdr.Close();
				Console.WriteLine("Categorie id read successfully !");
			}

			catch (Exception ex)
            {
				Console.WriteLine(ex);
            }
		}

		public void Read(MySqlConnection conn)
		{
			try
			{
				Console.WriteLine("Trying to read categorie ...");
				string sql = String.Format("select Id_categorie, nom from categorie where nom='{0}'", this.name);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();
				Console.WriteLine(rdr[0]);
				this.id = Int32.Parse(rdr["Id_categorie"].ToString());
				this.name = rdr["nom"].ToString();
				Console.WriteLine("id : {0}, name : {1}", this.id, this.name);
				rdr.Close();
				Console.WriteLine("Categorie read successfully !");
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}

	public class MyProduct
	{
		public int id;
		public int userId;
		public string name;
		public string desc;
		public string addedAt;
		public double price;
		public MyCategorie cat;
		public int nbSells;

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
				Console.WriteLine("Erreur de données pour " + str);
				return "";
			}
		}

		public void GetCategorie(MySqlConnection conn)
		{
			if (this.cat.id != -1)
			{
				try
				{
					string sql = String.Format("select nom from categorie where Id_categorie={0}", this.cat.id);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Read();
					this.cat.name = rdr.GetValue(rdr.GetOrdinal("nom")).ToString();
					rdr.Close();
				}


				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			else
			{
				Console.WriteLine("Get the categorie from the product name");
			}
		}

		public void Add(MySqlConnection conn)
		{
			string sql = String.Format("insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values('{0}', '{1}', CURRENT_DATE(), {2}, {3}, {4})", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.cat.id, this.userId);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			rdr.Close();
		}

		public void Edit(MySqlConnection conn)
		{
			if (this.name != "" && this.desc != "" && this.price != 0 && this.id != -1)
			{
				try
				{
					Console.WriteLine("Trying to edit product table ...");
					string sql = String.Format("update produit set nom='{0}', description='{1}', prix={2}) where Id_produit={3}", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.id);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Table edited successfully !");
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			else
			{
				Console.WriteLine("Failed to edit table : missing some fields");
			}
		}

		public void Delete(MySqlConnection conn)
		{
			string sql = String.Format("delete from produit where name='{0}'", this.name);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			rdr.Close();
		}

		public void Read(MySqlConnection conn)
		{
			string sql = String.Format("select * from produit where nom='{0}'", this.name);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			rdr.Read();
			this.id = Int32.Parse(rdr["Id_produit"].ToString());
			this.name = rdr["nom"].ToString();
			this.price = Double.Parse(rdr["prix"].ToString());
			this.desc = rdr["description"].ToString();
			this.addedAt = rdr["addedAt"].ToString();
			this.cat.id = Int32.Parse(rdr["Id_categorie"].ToString());
			rdr.Close();
		}
	}


	public class MyUser
	{
		public int id;
		public string email;
		public string password;
		public string bornAt;
		public string firstname;
		public string lastname;
		public string username;
		public bool seller;
		public bool buyer;
		public int nbProducts;
		public int nbCategories;

		public static void ListUsers(MySqlConnection conn)
		{
			try
			{
				string sql = "select * from utilisateur";
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					Console.WriteLine(rdr["prenom"] + " -- " + rdr["nom"]);
				}
				rdr.Close();
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static string BoolToIntStr(bool val)
		{
			if (val == true) return "1";
			else return "0";
		}

		public static bool StrIntToBool(string val)
		{
			return val == "1";
		}

		public void Add(MySqlConnection conn)
		{
			string sql = String.Format("insert into Utilisateur(pseudo, email, prenom, nom, naissance, vendeur, acheteur, createdAt, updatedAt) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, CURRENT_DATE(), GETFATE())", this.username, this.email, this.firstname, this.lastname, this.bornAt, BoolToIntStr(this.buyer), BoolToIntStr(this.seller));
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			rdr.Close();
		}

		public void Edit(MySqlConnection conn)
		{
			if (this.email != "" && this.username != "" && this.firstname != "" && this.lastname != "" && this.id != -1)
			{
				try
				{
					Console.WriteLine("Trying to update user table ...");
					string sql = String.Format("update Utilisateur set pseudo='{0}', email='{1}', prenom='{2}', nom='{3}', vendeur={4}, acheteur={5}, updatedAt=CURRENT_DATE() where Id_utilisateur={6}", this.username, this.email, this.firstname, this.lastname, BoolToIntStr(this.seller), BoolToIntStr(this.buyer), this.id); ;
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Updated table successfully !");
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			else
			{
				Console.WriteLine("Error : missing data");
			}
		}

		public void Delete(MySqlConnection conn)
		{
			string sql = String.Format("delete from Utilisateur(email) values ('{0}')", this.email);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			rdr.Close();
		}

		public void Read(MySqlConnection conn)
		{
			try
            {
				Console.WriteLine("Trying to read user ...");
				string sql = String.Format("select * from utilisateur where pseudo='{0}'", this.username); // Will add name column later
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					// Takes the last and unique row
					this.id = Int32.Parse(rdr["Id_utilisateur"].ToString());
					this.firstname = rdr["prenom"].ToString();
					this.lastname = rdr["nom"].ToString();
					this.bornAt = rdr["naissance"].ToString();
					this.username = rdr["pseudo"].ToString();
					this.email = rdr["email"].ToString();
				}

				rdr.Close();
				Console.WriteLine("User read successfully !");
			}

            catch (Exception ex)
            {
				Console.WriteLine(ex);
            }
		}

		public void CountCategories(MySqlConnection conn)
		{
			string sql = String.Format("select count(Id_categorie) as nbCategories from categorie join utilisateur on (categorie.Id_utilisateur={0});", this.id);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			while (rdr.Read())
			{
				// Takes the last and unique row
				this.nbCategories = Int32.Parse(rdr["nbCategories"].ToString());
			}

			rdr.Close();
		}

		public void CountProducts(MySqlConnection conn)
		{
			string sql = String.Format("select count(Id_produit) as nbProduits from produit join utilisateur on (produit.Id_utilisateur={0});", this.id);
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			while (rdr.Read())
			{
				// Takes the last and unique row
				this.nbProducts = Int32.Parse(rdr["nbProduits"].ToString());
			}

			rdr.Close();
		}
	}

	public class UserTable
	{
		public int id;
		public string name;
		public string email;
		public string password;
		public string bornAt;
		public string firstname;
		public string lastname;
		public string username;
		public bool seller;
		public bool buyer;
		public int nbProducts;
		public int nbCategories;

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
			command.CommandText = String.Format("insert into Utilisateur(pseudo, email, prenom, nom, naissance, vendeur, acheteur, createdAt, updatedAt) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, CURRENT_DATE(), GETFATE())", this.name, this.email, this.firstname, this.lastname, this.bornAt, BoolToIntStr(this.buyer), BoolToIntStr(this.seller));
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();
			reader.Close();
		}

		public void Edit(SqlConnection connection)
		{
			if (this.email != "" && this.username != "" && this.firstname != "" && this.lastname != "" && this.id != -1)
			{
				try
				{
					Console.WriteLine("Trying to update user table ...");
					SqlCommand command = connection.CreateCommand();
					command.CommandText = String.Format("update Utilisateur set username='{0}', email='{1}', firstname='{2}', lastname='{3}', vendeur={4}, acheteur={5}, updatedAt=CURRENT_DATE() where Id_utilisateur={6}", this.username, this.email, this.firstname, this.lastname, BoolToIntStr(this.seller), BoolToIntStr(this.buyer), this.id); ;
					command.CommandTimeout = 15;
					command.CommandType = CommandType.Text;
					SqlDataReader reader = command.ExecuteReader();
					reader.Close();
					Console.WriteLine("Updated table successfully !");
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			else
			{
				Console.WriteLine("Error : missing data");
			}
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

		public void Read(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("select * from utilisateur where pseudo='{0}'", this.username); // Will add name column later
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();

			reader.Read();
			Console.WriteLine(reader[0]);
			this.id = Int32.Parse(reader.GetValue(reader.GetOrdinal("Id_utilisateur")).ToString());
			this.firstname = reader.GetValue(reader.GetOrdinal("prenom")).ToString();
			this.lastname = reader.GetValue(reader.GetOrdinal("nom")).ToString();
			this.bornAt = reader.GetValue(reader.GetOrdinal("naissance")).ToString();
			this.username = reader.GetValue(reader.GetOrdinal("pseudo")).ToString();
			this.email = reader.GetValue(reader.GetOrdinal("email")).ToString();
			reader.Close();
		}

		public void CountCategories(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("select count(Id_categorie) as nbCategories from categorie join utilisateur on (categorie.Id_utilisateur={0});", this.id);
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			this.nbCategories = Int32.Parse(reader.GetValue(reader.GetOrdinal("nbCategories")).ToString());
			reader.Close();
		}

		public void CountProducts(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("select count(Id_produit) as nbProduits from produit join utilisateur on (produit.Id_utilisateur={0});", this.id);
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			this.nbProducts = Int32.Parse(reader.GetValue(reader.GetOrdinal("nbProduits")).ToString());
			reader.Close();
		}
	}

	public class CategorieTable
	{
		public int id;
		public string name;
		public int userId;
		public int nbProducts;

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

		public void ReadId(SqlConnection connection)
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

		public void Read(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("select Id_categorie, nom from categorie where nom='{0}'", this.name);
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();

			reader.Read();
			Console.WriteLine(reader[0]);
			this.id = Int32.Parse(reader.GetValue(reader.GetOrdinal("Id_categorie")).ToString());
			this.name = reader.GetValue(reader.GetOrdinal("nom")).ToString();
			Console.WriteLine("id : {0}, name : {1}", this.id, this.name);
			reader.Close();
		}
	}

	public class ProductTable
	{
		public int id;
		public int userId;
		public string name;
		public string desc;
		public string addedAt;
		public double price;
		public int nbSells;
		public CategorieTable cat;

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
				Console.WriteLine("Erreur de données pour " + str);
				return "";
			}
		}

		public void GetCategorie(SqlConnection connection)
		{
			if (this.cat.id != -1)
			{
				try
				{
					SqlCommand command = connection.CreateCommand();
					command.CommandText = String.Format("select nom from categorie where Id_categorie={0}", this.cat.id);
					command.CommandTimeout = 15;
					command.CommandType = CommandType.Text;
					SqlDataReader reader = command.ExecuteReader();
					reader.Read();
					this.cat.name = reader.GetValue(reader.GetOrdinal("nom")).ToString();
					reader.Close();
				}


				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			else
			{
				Console.WriteLine("Get the categorie from the product name");
			}
		}

		public void Add(SqlConnection connection)
		{
			try
            {
				Console.WriteLine("Inserting into products ...");
				SqlCommand command = connection.CreateCommand();
				Console.WriteLine("{0}", this.ToPointStr(this.price.ToString()));
				command.CommandText = String.Format("insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values('{0}', '{1}', GETDATE(), {2}, {3}, {4})", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.cat.id, this.userId);
				command.CommandTimeout = 15;
				command.CommandType = CommandType.Text;
				SqlDataReader reader = command.ExecuteReader();
				reader.Close();
				Console.WriteLine("Product inserted correctly !");
			}

			catch (Exception ex)
            {
				Console.WriteLine(ex);
            }
		}

		public void Edit(SqlConnection connection)
		{
			if (this.name != "" && this.desc != "" && this.price != 0 && this.id != -1) {
				try
				{
					Console.WriteLine("Trying to edit product table ...");
					SqlCommand command = connection.CreateCommand();
					command.CommandText = String.Format("update produit set nom='{0}', description='{1}', prix={2}) where Id_produit={3}", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.id);
					command.CommandTimeout = 15;
					command.CommandType = CommandType.Text;
					SqlDataReader reader = command.ExecuteReader();
					reader.Close();
					Console.WriteLine("Table edited successfully !");
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			else
			{
				Console.WriteLine("Failed to edit table : missing some fields");
			}
		}

		public void Delete(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("delete from produit where name='{0}'",  this.name);
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();
			reader.Close();
		}

		public void Read(SqlConnection connection)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandText = String.Format("select * from produit where nom='{0}'", this.name);
			command.CommandTimeout = 15;
			command.CommandType = CommandType.Text;
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			Console.WriteLine(reader[0]);
			this.id = Int32.Parse(reader.GetValue(reader.GetOrdinal("Id_produit")).ToString());
			this.name = reader.GetValue(reader.GetOrdinal("nom")).ToString();
			this.price = Double.Parse(reader.GetValue(reader.GetOrdinal("prix")).ToString());
			this.desc = reader.GetValue(reader.GetOrdinal("description")).ToString();
			this.addedAt = reader.GetValue(reader.GetOrdinal("addedAt")).ToString();
			this.cat.id = Int32.Parse(reader.GetValue(reader.GetOrdinal("Id_categorie")).ToString());
			reader.Close();
		}
	}
}