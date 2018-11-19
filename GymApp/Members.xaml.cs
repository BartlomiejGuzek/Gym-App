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

	public partial class Members : Window
	{
		DataTable dataTable = new DataTable();
		System.Windows.Threading.DispatcherTimer refreshTimer = new System.Windows.Threading.DispatcherTimer();

		int ID;
		string name;
		string surname;
		string phone;
		string gender;
		DateTime regDate;
		string cardID;

		public Members()
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
			Console.WriteLine("refreshed");
			dataTable.Load(MySQLCommands.GetUsers().ExecuteReader());
			MySQLCommands.Close();
			dg_Members.DataContext = dataTable;
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_Add_Click(object sender, RoutedEventArgs e)
		{
			AddMember addMemberWindow = new AddMember();
			addMemberWindow.Owner = Application.Current.MainWindow;
			addMemberWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			addMemberWindow.Show();
		}

		private void btn_Edit_Click(object sender, RoutedEventArgs e)
		{
			EditMember editMemerWindow = new EditMember(ID, name, surname, phone, gender, regDate, cardID, this);
			editMemerWindow.Owner = Application.Current.MainWindow;
			editMemerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			editMemerWindow.Show();
		}

		private void dg_Members_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			btn_Edit.IsEnabled = true;
			btn_Delete.IsEnabled = true;
			DataRowView dataRowView = (DataRowView)dg_Members.SelectedItem;
			ID = Convert.ToInt32(dataRowView.Row[0]);
			name = dataRowView.Row[1].ToString();
			surname = dataRowView.Row[2].ToString();
			phone = dataRowView.Row[3].ToString();
			gender = dataRowView.Row[4].ToString();
			regDate = dataRowView.Row.Field<DateTime>("RegistrationDate");
			cardID = dataRowView.Row[6].ToString();
		}

		private void btn_Delete_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.DeleteUser(ID);
		}

		private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			DataView dataView = dataTable.DefaultView;
			dataView.RowFilter = string.Format("Phone like '%{0}%'", tb_Search.Text);
			
		}
	}
}
