using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace GymApp
{
	//TODO: Fix live statistics for members and active members
	public partial class MainWindow : Window
	{
		System.Windows.Threading.DispatcherTimer refreshTimer = new System.Windows.Threading.DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();
			refreshTimer.Tick += refreshTimer_Tick;
			refreshTimer.Interval = new TimeSpan(0, 0, 1);
			refreshTimer.Start();
		}

		private void btn_Members_Click_1(object sender, RoutedEventArgs e)
		{
			Members membersWindow = new Members();
			membersWindow.Show();
		}

		private void refreshTimer_Tick(object sender, EventArgs e)
		{
			//lb_MaleMembers.Content = MySQLCommands.GetMaleMembersCount();
			//lb_FemaleMembers.Content = MySQLCommands.GetFemaleMembersCount();
			//lb_MaleActiveMembers.Content = MySQLCommands.GetActiveMaleMembersCount();
			//lb_FemaleActiveMembers.Content = MySQLCommands.GetActiveFemaleMembersCount();
		}

		private void btn_Members_Copy1_Click(object sender, RoutedEventArgs e)
		{
			ActiveMembers activeMembersWindow = new ActiveMembers();
			activeMembersWindow.Show();
		}
	}
}

