using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace GymApp
{
	public partial class ActiveMembers : Window
	{
		DataTable dataTable = new DataTable();
		MySqlDataAdapter adapter;
		DataSet dataSet;
		System.Windows.Threading.DispatcherTimer refreshTimer = new System.Windows.Threading.DispatcherTimer();
		int count;
		int ID;
		string name;
		string surname;
		string phone;
		string gender;
		DateTime regDate;
		string cardID;

		public ActiveMembers()
		{
			InitializeComponent();
			refreshTimer.Tick += refreshTimer_Tick;
			refreshTimer.Interval = new TimeSpan(0, 0, 1);
			refreshTimer.Start();
			RefreshMembers();

		}

		private void refreshTimer_Tick(object sender, EventArgs e)
		{
			RefreshMembers();
		}

		public void RefreshMembers()
		{
			//dataTable.Load(MySQLCommands.GetActiveUsers().ExecuteReader());
			//dg_Members.DataContext = dataTable;
			//dg_Members.Items.Refresh();
			adapter = MySQLCommands.TestGetActiveUsers();
			dataSet = new DataSet();
			adapter.Fill(dataSet, "Users");
			dg_Members.ItemsSource = dataSet.Tables["Users"].DefaultView;
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void dg_Members_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			btn_Logout.IsEnabled = true;
			DataRowView dataRowView = (DataRowView)dg_Members.SelectedItem;
			try
			{
				ID = Convert.ToInt32(dataRowView.Row[0]);
				name = dataRowView.Row[1].ToString();
				surname = dataRowView.Row[2].ToString();
				phone = dataRowView.Row[3].ToString();
				gender = dataRowView.Row[4].ToString();
				regDate = dataRowView.Row.Field<DateTime>("RegistrationDate");
				cardID = dataRowView.Row[6].ToString();
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
			
			Console.WriteLine(ID);
		}

		private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			//TODO: Fix filtering
			//DataView dataView = dataTable.DefaultView;
			//dataView.RowFilter = string.Format("Phone like '%{0}%'", tb_Search.Text);
			//(dg_Members.ItemsSource as DataTable).DefaultView.RowFilter = tb_Search.Text;
		}

		private void btn_Logout_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.DeleteActiveUser(ID);
		}
	}
}

