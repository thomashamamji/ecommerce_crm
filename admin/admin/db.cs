using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Threading.Tasks;

// Functions to check the tables to see if some unique fields already have the input value stored in the db or not

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
		public const int ALREADY_EXISTS = -5;

		// Alert colors
		public static Color SUCCESS = Color.FromArgb(0, 190, 0);
		public static Color ERROR = Color.FromArgb(190, 0, 0);

		public static void PrintCodeContextError (int code) {
			if (code >= Status.NO_ERROR) return;
			switch (code) {
				case Status.MISSING_FIELD:
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

		public static void DisplayError (int code)
        {
			if (code >= Status.NO_ERROR) Status.ShowMessage(true, "Opération réussie.");
			else
            {
				switch (code)
                {
					case Status.MISSING_FIELD:
						Status.ShowMessage(false, "Des informations sont manquantes. Assurez vous de remplir tous les champs requis du formulaire.");
					break;
					case Status.DB_ERROR:
						Status.ShowMessage(false, "Une erreur interne est survenue dans le programme.");
					break;
					case Status.ACCESSS_DENIED:
						Status.ShowMessage(false, "Vous n'avez pas l'autorisation de réaliser cette opération.");
					break;
					case Status.NOT_FOUND:
						Status.ShowMessage(false, "Elément non trouvé.");
					break;
					case Status.ALREADY_EXISTS:
						Status.ShowMessage(false, "L'élément que vous souhaitez ajouter existe déjà.");
					break;
					default: break;
				}
            }
		}

		public static void HandleCode(int code)
        {
			Status.PrintCodeContextError(code);
			Status.DisplayError(code);
        }

		// Only presentable texts for these functions

		public static void PrintSuccess(string txt)
        {
			admin.status f = new admin.status();
			f.msg.Text = txt;
			f.ShowDialog();
			f.Close();
        }

		public static void ShowMessage(bool success, string message)
		{
			admin.status f = new admin.status();
			// Setting the color
			if (success) f.msg.ForeColor = Status.SUCCESS;
			else f.msg.ForeColor = Status.ERROR;

			// Setting the message
			if (message == "")
			{
				if (success) f.msg.Text = "Réussite !";
				else f.msg.Text = "Echec ! ";
			}
			else
			{
				f.msg.Text = message;
			}

			f.Show();

			// Triggering the delay
			/*Task t = Task.Run(() => {
				Console.WriteLine("Showing message ...");
				f.Show();
				// Add the delay here
				Task.Delay(5000); // 5 seconds
			});
			t.Wait();
			Console.WriteLine("Close message window.");
			f.Close();*/
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
		public int productsNb;
		public int sellsNb;
		public string createdBy; // username
		public string createdAt; // String format(dd/mm/yyyy)

		public MyCategorie () {
			this.id = -1;
			this.name = "";
			this.productsNb = 0;
			this.sellsNb = 0;
			this.createdBy = "";
			this.createdAt = "";
		}

		public MyCategorie (int id, string name) {
			this.id = id;
			this.name = name;
		}

		public int Check(MySqlConnection conn) {
			if (this.name == "") return Status.MISSING_FIELD;
			try {
				string sql = String.Format("select Id_category from categorie where nom='{0}'", this.name);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader(); ;
				if (rdr.Read()) return Status.ALREADY_EXISTS;
				return Status.NO_ERROR;
			}

			catch (Exception ex) {
				Console.WriteLine(ex.Message);
				return Status.DB_ERROR;
			}
		}

		public int ListProducts (MySqlConnection conn) {
			if (this.id == -1) {
				Console.WriteLine("No category.");
				return Status.MISSING_FIELD;
			}

			try {
				Console.WriteLine("Reading category's products ...");
				string sql = String.Format("select Id_produit,nom, description, price, addedAt from Produit where Id_categorie = '{0}'", this.id);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					Console.WriteLine(rdr["Id_produit"] + " -- " + rdr["nom"]);
					// Display the product on the screen from that function
					// ...
				}
				rdr.Close();
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return Status.DB_ERROR;
			}
		}

		// Call Check() before inserting
		public int Add(MySqlConnection conn)
		{
			try
			{
				int st = this.Check(conn);
				if (st < Status.NO_ERROR) return st;
				Console.WriteLine("Trying to insert data (categorie) ...");
				string sql = String.Format("insert into categorie(nom) values('{0}')", this.name);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Close();
				Console.WriteLine("Added categorie successfully !");
				return Status.NO_ERROR;
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
				if (this.id != -1)
                {
					Console.WriteLine("Trying to delete categorie ...");
					string sql = String.Format("delete from produit where Id_categorie={0}", this.id);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					rdr.Close();
					sql = String.Format("delete from categorie where Id_categorie={0}", this.id);
					cmd = new MySqlCommand(sql, conn);
					rdr = cmd.ExecuteReader();
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

		// products, createdBy

		public int GetProducts (MySqlConnection conn)
        {
			if (this.id == -1)
            {
				return Status.NO_ERROR;
            }
			try
            {
				string sql = String.Format("select count(p.Id_produit) as productsNb from categorie as c join produit as p on (p.Id_categorie=c.Id_categorie) where c.Id_categorie={0}", this.id);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();
				this.productsNb = Int32.Parse(rdr.GetValue(rdr.GetOrdinal("productsNb")).ToString());
				rdr.Close();
				Console.WriteLine("{0} products in that category.", this.productsNb);
				return Status.NO_ERROR;
			}

			catch (Exception ex)
            {
				Console.WriteLine("An error occured while counting the category's products : ", ex.Message);
				return Status.DB_ERROR;
            }
        }

		public int GetUser(MySqlConnection conn)
		{
			if (this.id == -1)
			{
				return Status.NO_ERROR;
			}
			try
			{
				string sql = String.Format("select u.pseudo from categorie as c join utilisateur as u on (u.Id_utilisateur=c.Id_utilisateur) where c.Id_categorie={0}", this.id);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();
				this.createdBy = rdr.GetValue(rdr.GetOrdinal("pseudo")).ToString();
				rdr.Close();
				Console.WriteLine("User '{0}' read successfully", this.createdBy);
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine("An error occured while getting the category's user : ", ex.Message);
				return Status.DB_ERROR;
			}
		}

		public int GetSells(MySqlConnection conn)
        {
			if (this.id == -1)
            {
				Console.WriteLine("Missing the category id.");
				return Status.MISSING_FIELD;
            }

			try
            {
				string sql = String.Format("select p.Id_produit from vente as v join produit as p on (p.Id_produit=v.Id_produit) join categorie as c on (c.Id_categorie=p.Id_categorie) where c.Id_categorie={0}", this.id);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();
				this.sellsNb = Int32.Parse(rdr.GetValue(rdr.GetOrdinal("pseudo")).ToString());
				rdr.Close();
				Console.WriteLine("{0} sells for that category.", this.sellsNb);
				return Status.NO_ERROR;
			}

			catch (Exception ex)
            {
				Console.WriteLine(ex);
				return Status.MISSING_FIELD;
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

		public MyProduct () {
			this.id = -1;
			this.userId = -1;
			this.name = "";
			this.desc = "";
			this.addedAt = "";
			this.cat = new MyCategorie();
			this.cat.id = -1;
			this.cat.name = "";
			this.nbSells = 0;
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
			int st = this.Check(conn);
			if (st < Status.NO_ERROR) return st;
			try {
				string sql = String.Format("insert into produit(nom, description, addedAt, prix, Id_categorie, Id_utilisateur) values('{0}', '{1}', CURRENT_DATE(), {2}, {3}, {4})", this.name, this.desc, this.ToPointStr(this.price.ToString()), this.cat.id, this.userId);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				Console.WriteLine("Execution query '{0}' ...", sql);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Close();
				Status.ShowMessage(true, "Le produit a été ajouté avec succès !");
				return Status.NO_ERROR;
			}

			catch (Exception ex) {
				Console.WriteLine(ex);
				return Status.DB_ERROR;
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

		public int Check(MySqlConnection conn)
		{
			if (this.name == "" || this.price < 0 || this.cat.name == "") return Status.MISSING_FIELD;
			try
			{
				string sql = String.Format("select Id_produit from produit where nom='{0}'", this.name);
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader(); ;
				if (rdr.Read()) return Status.ALREADY_EXISTS;
				return Status.NO_ERROR;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
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

		public MyUser() {
			this.id = -1;
			this.email = "";
			this.password = "";
			this.bornAt = "";
			this.firstname = "";
			this.lastname = "";
			this.username = "";
			// May change it to int type
			this.seller = false;
			this.buyer = false;
			this.nbProducts = 0;
			this.nbCategories = 0;
			this.users = new Int32[50];
			this.permission = -1;
		}

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

		// Write a function to add a user id from the selected user(s) of the users list displayed on the window.

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

					string query = String.Format("update utilisateur set vendeur={0} where Id_utilisateur={1}", this.permission, this.users[id].ToString());
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
					string sql = String.Format("delete from utilisateur(pseudo) values ('{0}')", this.username);
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

		// Value displayed at the top of the categories list
		public int CountCategories(MySqlConnection conn)
		{
			if (this.id != -1)
			{
				try
                {
					Console.WriteLine("Trying to count categories ...");
					string sql = String.Format("select count(c.Id_categorie) as nbCategories from utilisateur as u join categorie as c on (c.Id_utilisateur=u.Id_utilisateur) where u.Id_utilisateur={0}", this.id); // Updated (needs tests)
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
					string sql = String.Format("select count(p.Id_produit) as nbProduits from produit as p join utilisateur as u on (p.Id_utilisateur=u.Id_utilisateur) where u.Id_utilisateur={0}", this.id);
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
