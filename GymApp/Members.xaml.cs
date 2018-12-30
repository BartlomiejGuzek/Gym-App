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
		int count;
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
			dataTable.Load(MySQLCommands.GetUsers().ExecuteReader());
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
			btn_ViewPayments.IsEnabled = true;
			try
			{
				DataRowView dataRowView = (DataRowView)dg_Members.SelectedItem;
				ID = Convert.ToInt32(dataRowView.Row[0]);
				name = dataRowView.Row[1].ToString();
				surname = dataRowView.Row[2].ToString();
				phone = dataRowView.Row[3].ToString();
				gender = dataRowView.Row[4].ToString();
				regDate = dataRowView.Row.Field<DateTime>("RegistrationDate");
				cardID = dataRowView.Row[6].ToString();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
			
			Console.WriteLine(ID);
		}

		private void btn_Delete_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				MySQLCommands.DeleteUserPayments(ID);
				MySQLCommands.DeleteUser(ID);
				DataRowView dataRowView = (DataRowView)dg_Members.SelectedItem;
				dataRowView.Delete();
				btn_Edit.IsEnabled = false;
				btn_Delete.IsEnabled = false;
				btn_ViewPayments.IsEnabled = false;
				ID = 0;
				name = null;
				surname = null;
				phone = null;
				gender = null;
				regDate = DateTime.Today;
				cardID = null;
			}
			RefreshMembers();
		}

		private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			DataView dataView = dataTable.DefaultView;
			dataView.RowFilter = string.Format("Phone like '%{0}%'", tb_Search.Text);
		}

		private void btn_ViewPayments_Click(object sender, RoutedEventArgs e)
		{
			Payments paymentsWindow = new Payments(ID, name, surname, phone, gender, regDate, cardID);
			paymentsWindow.Owner = Application.Current.MainWindow;
			paymentsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			paymentsWindow.Show();
		}
	}
}
