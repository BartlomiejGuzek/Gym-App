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
		static public string currentCardId;
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
			}
		}

		//Select funcitons
		static public MySqlCommand GetUsers()
		{
			Connect();
			MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", connection);
			return cmd;
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
			}
		}

		static public void InsertPayments()
		{

		}

		static public void InsertActiveUsers()
		{

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
				Console.WriteLine("InsertUsers");
				Console.WriteLine(e.Message);
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
				Console.WriteLine("InsertUsers");
				Console.WriteLine(e.Message);
			}
		}

		static public void DeletePayments()
		{

		}

		static public void DeleteActiveUsers()
		{

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
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM activeusers WHERE Users_idUsers", connection);
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
				MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM activeusers WHERE Gender = 'Female'", connection);
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
