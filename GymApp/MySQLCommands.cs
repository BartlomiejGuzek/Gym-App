using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Threading;

namespace GymApp
{
    class MySQLCommands
    {
		static MySqlConnection connection;

		//Utility functions
		static public void Connect()
		{
			string server = "localhost";
			string database = "gym";
			string uid = "admin";
			string password = "pa$$word";
			string connectionString;
			connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
			connection = new MySqlConnection(connectionString);
			connection.Open();
		}

		static public void Close()
		{
			connection.Close();
		}

		//Card related functions
		static public void EnterGetCardIdMode()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("INSERT INTO tempID (cardID) VALUES(1)", connection);
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("EnterGetCardIdMode");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "EnterGetCardIdMode", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static public void ExitGetCardIdMode()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("DELETE FROM tempID WHERE (cardID = 1);", connection);
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("ExitGetCardIdMode");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "ExitGetCardIdMode", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static public void WaitForID(AddMember _addMember, bool _isEdit)
		{
			FetchCardID fetchWindow = new FetchCardID(_addMember, _isEdit);
			fetchWindow.Show();
		}

		static public string GetCardID()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT * FROM tempID", connection);
				MySqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					while(reader.Read())
					{
						var _id = reader.GetString(0);
						Console.WriteLine(_id);
						return _id;
					}
				}
				reader.Close();
				Close();
				return null;
			}
			catch (Exception e)
			{
				Console.WriteLine("GetCardID");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "GetCardID", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}
		}

		static public void DropIDTable()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("DELETE FROM tempID", connection);
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("DropIDTable");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "DropIDTable", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//Select funcitons
		static public MySqlCommand GetUsers()
		{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", connection);
				return cmd;
		}

		static public MySqlDataAdapter TestGetUsers()
		{
			Connect();
			MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM users", connection);
			return adapter;
		}

		static public MySqlCommand GetActiveUsers()
		{
			Connect();
			MySqlCommand cmd = new MySqlCommand("SELECT * FROM activeusers JOIN users ON activeusers.Users_idUsers = users.idUsers", connection);
			return cmd;
		}

		static public MySqlDataAdapter TestGetActiveUsers()
		{
			Connect();
			MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM activeusers JOIN users ON activeusers.Users_idUsers = users.idUsers", connection);
			return adapter;
		}

		static public MySqlCommand GetPayments(int _id)
		{
			Connect();
			MySqlCommand cmd = new MySqlCommand("SELECT * FROM payments WHERE Users_idUsers = @id", connection);
			cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
			return cmd;
		}

		static public string GetNewestPayment(int _id)
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT MAX(Expires) FROM payments WHERE Users_idUsers = @id", connection);
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var date = reader.GetString(0);
						return date;
					}
				}
				reader.Close();
				Close();
				return "0";
			}
			catch (Exception e)
			{
				Console.WriteLine("GetNewestPayment");
				Console.WriteLine(e.Message);
				MessageBox.Show("No payments!", "GetNewestPayment", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}

		}

		//Insert functions
		static public void InsertUsers(string _name, string _surname, string _phone, string _gender, DateTime _date, string _cardId)
		{
			
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("INSERT INTO Users (Name, Surname, Phone, Gender, RegistrationDate, CardID) VALUES(@name, @surname, @phone, @gender, @date, @cardID)", connection);
				cmd.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = _name;
				cmd.Parameters.Add("@surname", MySqlDbType.VarChar, 45).Value = _surname;
				cmd.Parameters.Add("@phone", MySqlDbType.VarChar, 45).Value = _phone;
				cmd.Parameters.Add("@gender", MySqlDbType.VarChar, 45).Value = _gender;
				cmd.Parameters.Add("@date", MySqlDbType.Date).Value = _date;
				cmd.Parameters.Add("@cardID", MySqlDbType.VarChar, 12).Value = _cardId;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("InsertUsers");
				Console.WriteLine(e.Message);
				MessageBox.Show("User with this Card ID already exists", "InsertUsers", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static public void InsertPayment(DateTime _expires, int _id)
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("INSERT INTO Payments (Expires, Users_idUsers) VALUES(@expires, @Users_idUsers)", connection);
				cmd.Parameters.Add("@expires", MySqlDbType.Date).Value = _expires;
				cmd.Parameters.Add("@Users_idUsers", MySqlDbType.VarChar, 45).Value = _id;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("InsertPayment");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "InsertPayment", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//Update functions
		static public void UpdateUser (int _id, string _name, string _surname, string _phone, string _gender, DateTime _date, string _cardId)
		{
			try
			{
				Connect();

				MySqlCommand cmd = new MySqlCommand("UPDATE users SET Name = @name, Surname = @surname, Phone = @phone, Gender = @gender, RegistrationDate = @date, CardID = @cardID WHERE idUsers = @id", connection);
				cmd.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = _name;
				cmd.Parameters.Add("@surname", MySqlDbType.VarChar, 45).Value = _surname;
				cmd.Parameters.Add("@phone", MySqlDbType.VarChar, 45).Value = _phone;
				cmd.Parameters.Add("@gender", MySqlDbType.VarChar, 45).Value = _gender;
				cmd.Parameters.Add("@date", MySqlDbType.Date).Value = _date;
				cmd.Parameters.Add("@cardID", MySqlDbType.VarChar, 12).Value = _cardId;
				cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("UpdateUser");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "UpdateUser", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//Delete functions
		static public void DeleteUser(int _id)
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("DELETE from users WHERE idUsers = @id", connection);
				cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("DeleteUser");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "DeleteUser", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static public void DeleteUserPayments(int _id)
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("DELETE from payments WHERE Users_idUsers = @id", connection);
				cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("DeleteUserPayments");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "DeleteUserPayments", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static public void DeleteActiveUser(int _id)
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("DELETE from activeusers WHERE Users_idUsers = @id", connection);
				cmd.Parameters.Add("@id", MySqlDbType.VarChar, 12).Value = _id;
				cmd.ExecuteNonQuery();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("DeleteActiveUser");
				Console.WriteLine(e.Message);
				MessageBox.Show(e.Message, "DeleteActiveUser", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//Count functions
		static public string GetMaleMembersCount()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE Gender = 'Male'", connection);
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var count = reader.GetString(0);
						return count;
					}
				}
				reader.Close();
				Close();
				return "0";
			}
			catch (Exception e)
			{
				Console.WriteLine("GetMaleMembersCount");
				Console.WriteLine(e.Message);
				return null;
			}
		}

		static public string GetFemaleMembersCount()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE Gender = 'Female'", connection);
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var count = reader.GetString(0);
						return count;
					}
				}
				reader.Close();
				Close();
				return "0";
			}
			catch (Exception e)
			{
				Console.WriteLine("GetFemaleMembersCount");
				Console.WriteLine(e.Message);
				return null;
			}
		}

		static public string GetActiveMaleMembersCount()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users JOIN activeusers ON activeusers.Users_idUsers = users.idUsers WHERE users.Gender <> 'Male'", connection);
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var count = reader.GetString(0);
						return count;
					}
				}
				reader.Close();
				Close();
				return "0";
			}
			catch (Exception e)
			{
				Console.WriteLine("GetActiveMaleMembersCounts");
				Console.WriteLine(e.Message);
				return null;
			}
		}

		static public string GetActiveFemaleMembersCount()
		{
			try
			{
				Connect();
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users JOIN activeusers ON activeusers.Users_idUsers = users.idUsers WHERE users.Gender <> 'Female'", connection);
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var count = reader.GetString(0);
						return count;
					}
				}
				reader.Close();
				Close();
				return "0";
			}
			catch (Exception e)
			{
				Console.WriteLine("GetActiveFemaleMembersCount");
				Console.WriteLine(e.Message);
				return null;
			}
		}


	}
}
