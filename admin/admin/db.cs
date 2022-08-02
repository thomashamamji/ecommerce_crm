﻿using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace admin_db
{
	public class Status
    {
		// For error handling
		public const int NO_ERROR = 0;
		public const int MISSING_FIELD = -1;
		public const int DB_ERROR = -2;
		public const int ACCESSS_DENIED = -3;
		public const int NOT_FOUND = -4;

		public static void PrintCodeContextError (int code) {
			if (code >= Status.NO_ERROR) return;
			switch (code) {
				case Status.MISSING_FIELD :
					Console.WriteLine("Missing some fields.");
				break;
				case Status.DB_ERROR :
					Console.WriteLine("An error occured during the database request.");
				break;
				default :
					Console.WriteLine("Error not known.");
				break;
			}
		}

		// Only presentable texts for these functions

		public static void PrintSuccess(string txt)
        {
			admin.status f = new admin.status();
			f.msg.Text = txt;
			f.ShowDialog();
			f.Close();
        }

		public static void PrintError(string txt)
        {
			admin.status f = new admin.status();
			f.msg.Text = txt;
			f.ShowDialog();
			f.Close();
		}
    }

	public class MyCategorie
	{
		public int id;
		public string name;

		public int Add(MySqlConnection conn)
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
					return Status.NO_ERROR;
				}

				else
				{
					Console.WriteLine("Missing category name.");
					return Status.MISSING_FIELD;
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return Status.DB_ERROR;
			}
		}

		public int Delete(MySqlConnection conn)
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
					return Status.NO_ERROR;
				}

				else
                {
					Console.WriteLine("Missing name !");
					return Status.MISSING_FIELD;
                }
            }

			catch (Exception ex)
            {
				Console.WriteLine(ex);
				return Status.DB_ERROR;
            }
		}

		// Rename a category
		public int Edit(MySqlConnection conn)
		{
			try
            {
				if (this.name != "" && this.id != -1)
                {
					Console.WriteLine("Trying to edit categorie ...");
					string sql = String.Format("update categorie set nom='{1}' where Id_categorie='{0}'", this.id, this.name);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Deleted categorie successfully !");
					return Status.NO_ERROR;
				}

				else
                {
					Console.WriteLine("Missing some fields.");
					return Status.MISSING_FIELD;
                }
            }

			catch (Exception ex)
            {
				Console.WriteLine(ex);
				return Status.DB_ERROR;
            }
		}

		public int ReadId(MySqlConnection conn)
		{
			// Add validation
			if (this.name == "") {
				Console.WriteLine("Missing category name.");
				// Trigger the error panel display
				Status.PrintError("Sorry, an error occured while reading the category.");
				return Status.MISSING_FIELD;
			}

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
				return Status.NO_ERROR;
			}

			catch (Exception ex)
            {
				Console.WriteLine(ex);
				return Status.DB_ERROR;
            }
		}

		public int Read(MySqlConnection conn)
		{
			// Add validation
			if (this.name == "") {
				Console.WriteLine("Missing category name.");
				// Trigger the error panel display
				Status.PrintError("Sorry, an error occured while reading the category.");
				return Status.MISSING_FIELD;
			}

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
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return Status.DB_ERROR;
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

		public int GetCategorie(MySqlConnection conn)
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
					return Status.NO_ERROR;
				}


				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return Status.DB_ERROR;
				}
			}

			else
			{
				// Missing the category id
				Console.WriteLine("Missing the category id.");
				return Status.MISSING_FIELD;
			}
		}

		public int Add(MySqlConnection conn)
		{
            if (this.name != "") {
				try {
					string sql = String.Format("insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values('{0}', '{1}', CURRENT_DATE(), {2}, {3}, {4})", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.cat.id, this.userId);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Status.PrintSuccess("Le produit a été ajouté avec succès !");
					return Status.NO_ERROR;
				}

				catch (Exception ex) {
					Console.WriteLine(ex);
					return Status.DB_ERROR;
				}
			}

			else
            {
				Status.PrintError("Une erreur est survenue dans le programme :(");
				return Status.MISSING_FIELD;
			}
		}

		public int Edit(MySqlConnection conn)
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
					return Status.NO_ERROR;
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return Status.DB_ERROR;
				}
			}

			else
			{
				Console.WriteLine("Failed to edit table : missing some fields");
				return Status.MISSING_FIELD;
			}
		}

		public int Delete(MySqlConnection conn)
		{
			if (this.name == "") {
				Console.WriteLine("Missing the name.");
				return Status.MISSING_FIELD;
			}

			else {
				try {
					string sql = String.Format("delete from produit where name='{0}'", this.name);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					return Status.NO_ERROR;
				}

				catch (Exception ex) {
					Console.WriteLine(ex.ToString());
					return Status.DB_ERROR;
				}
			}
		}

		public int Read(MySqlConnection conn)
		{
			if (this.name == "") {
				Console.WriteLine("Missing the name.");
				return Status.MISSING_FIELD;
			}

			try {
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
				return Status.NO_ERROR;
			}

			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return Status.DB_ERROR;
			}
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
		public bool seller; // As admin
		public bool buyer; // Blocks the php login (shop catalog) if false
		public int nbProducts;
		public int nbCategories;
		public int[] users; // Id(s) to make operations to user(s)
		public int permission;

		// No error code for this one (but may change later)
		public bool AuthenticateAdmin(MySqlConnection conn)
        {
			if (this.username == "" || this.password == "") {
				Console.WriteLine("Missing username or password.");
				return false;
			}

            try
            {
				Console.WriteLine("Trying to authenticate as admin ...");
				string sql = String.Format("select Id_utilisateur from utilisateur where pseudo='{0}' and password='{1}'", this.username, this.password);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				bool ret = rdr.Read() == true;
				if (ret) this.id = Int32.Parse(rdr["Id_utilisateur"].ToString());
				Console.WriteLine("End of authentication !");
				rdr.Close();
				return true;
            }

			catch (Exception ex)
            {
				Console.WriteLine("Db connection failed : " + ex);
				return false;
            }
        }

		// New
		public int GrantPermissionsAsAdmin (MySqlConnection conn) {
			if (this.users.Length == 0 || this.permission == -1) {
				Console.WriteLine("Missing user(s) id(s).");
				return Status.MISSING_FIELD;
			}

			else {
				try
				{
					// First, check if all ids are valid (users are found)
					for (int id = 0; id < this.users.Length; id++)
					{
						string sql = String.Format("select Id_utilisateur from utilisateur where Id_utilisateur='{0}'", this.users[id]);
						MySqlCommand cmd = new MySqlCommand(sql, conn);
						MySqlDataReader rdr = cmd.ExecuteReader();
						if (rdr.Read() != true)
						{
							Console.WriteLine(String.Format("Id {0} not found.", this.users[id].ToString()));
							return Status.NOT_FOUND;
						}
					}

					string query = String.Format("update Utilisateur set vendeur={0} where Id_utilisateur={1}", this.permission, this.users[id].ToString());
					MySqlCommand command = new MySqlCommand(query, conn);
					MySqlDataReader reader = command.ExecuteReader();
					return Status.NO_ERROR;
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return Status.DB_ERROR;
				}
			}
		}

		public int ReadId(MySqlConnection conn)
		{
			if (this.username == "") {
				Console.WriteLine("Missing username.");
				return Status.MISSING_FIELD;
			}

			try
			{
				Console.WriteLine("Trying to read user id ...");
				string sql = String.Format("select Id_utilisateur from utilisateur where pseudo='{0}'", this.username);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader(); ;
				while (rdr.Read())
				{
					this.id = Int32.Parse(rdr["Id_utilisateur"].ToString());
				}

				rdr.Close();
				Console.WriteLine("User id ({0}) read successfully !", this.id);
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return Status.DB_ERROR;
			}
		}

		public static int ListUsers(MySqlConnection conn)
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
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return Status.DB_ERROR;
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

		public int Add(MySqlConnection conn)
		{
			if (this.username != "" && this.firstname != "" && this.lastname != "" && this.email != "" && this.bornAt != "")
            {
				try
                {
					Console.WriteLine("Adding new user ...");
					string sql = String.Format("insert into Utilisateur(pseudo, email, prenom, nom, naissance, vendeur, acheteur, createdAt, updatedAt) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, CURRENT_DATE(), GETFATE())", this.username, this.email, this.firstname, this.lastname, this.bornAt, BoolToIntStr(this.buyer), BoolToIntStr(this.seller));
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Added new user !");
					return Status.NO_ERROR;
				}

				catch(Exception ex)
                {
					Console.WriteLine("Failed : " + ex.ToString());
					return Status.DB_ERROR;
                }
            }

			else
            {
				Console.WriteLine("Missing some data :(");
				return Status.MISSING_FIELD;
            }
		}

		public int Edit(MySqlConnection conn)
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
					return Status.NO_ERROR;
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return Status.DB_ERROR;
				}
			}

			else
			{
				Console.WriteLine("Error : missing data");
				return Status.MISSING_FIELD;
			}
		}

		public int Delete(MySqlConnection conn)
		{
			if (this.email != "")
            {
				try
                {
					Console.WriteLine("Deleting user ...");
					string sql = String.Format("delete from Utilisateur(email) values ('{0}')", this.email);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					Console.WriteLine("Deleted user !");
					return Status.NO_ERROR;
				}
				catch(Exception ex)
                {
					Status.PrintError("Une erreur est survenue dans le programme :(");
					Console.WriteLine("Failed to delete user : " + ex.ToString());
					return Status.DB_ERROR;
                }
			}

			else
            {
				Console.WriteLine("Missing email field.");
				return Status.MISSING_FIELD;
            }
		}

		public int Read(MySqlConnection conn)
		{
			if (this.username == "") {
				Console.WriteLine("Missing username.");
				return Status.MISSING_FIELD;
			}

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
				// Dislpay success panel here
				Console.WriteLine("User read successfully !");
				return Status.NO_ERROR;
			}

            catch (Exception ex)
            {
				// Display error panel here
				Console.WriteLine(ex);
				return Status.DB_ERROR;
            }
		}

		public int CountCategories(MySqlConnection conn)
		{
			if (this.id != -1)
			{
				try
                {
					Console.WriteLine("Trying to count categories ...");
					string sql = String.Format("select count(Id_categorie) as nbCategories from categorie join utilisateur on (categorie.Id_utilisateur=utilisateur.Id_utilisateur) where utilisateur.Id_utilisateur={0};", this.id);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					while (rdr.Read())
					{
						// Takes the last and unique row
						this.nbCategories = Int32.Parse(rdr["nbCategories"].ToString());
					}

					rdr.Close();
					Console.WriteLine("Ended counting categories successfully !");
					return Status.NO_ERROR;
				}

				catch (Exception ex)
                {
					// Display error panel here
					Status.PrintError("Une erreur est survenue dans le programme :(");
					Console.WriteLine("An error occured while trying to count user categories : " + ex.ToString());
					return Status.DB_ERROR;
                }
			}

			else
            {
				// Display error panel here
				Console.WriteLine("Missing id :(");
				return Status.MISSING_FIELD;
            }
		}

		public int CountProducts(MySqlConnection conn)
		{
			if (id != -1)
            {
				try
                {
					Console.WriteLine("Trying to count products ...");
					string sql = String.Format("select count(Id_produit) as nbProduits from produit join utilisateur on (produit.Id_utilisateur=utilisateur.Id_utilisateur) where utilisateur.Id_utilisateur={0};", this.id);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					while (rdr.Read())
					{
						// Takes the last and unique row
						this.nbProducts = Int32.Parse(rdr["nbProduits"].ToString());
					}

					rdr.Close();
					// Display success panel here
					Console.WriteLine("Ended counting products successfully.");
					return Status.NO_ERROR;
				}

				catch (Exception ex)
                {
					// Display pannel here
					Console.WriteLine("Error while counting user products : " + ex.ToString());
					return Status.DB_ERROR;
                }
            }

			else
            {
				// Display pannel here
				Console.WriteLine("Missing some fields.");
				return Status.MISSING_FIELD;
            }
		}
	}
}
