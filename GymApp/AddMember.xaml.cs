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
using System.Windows.Shapes;

namespace GymApp
{

    public partial class AddMember : Window
    {
		string gender;
		string cardID;

        public AddMember()
        {
            InitializeComponent();
			dp_RegistrationDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(1990, 1, 1),
			DateTime.Now.AddDays(-1)));
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
			if( !string.IsNullOrWhiteSpace(tb_Name.Text) && !string.IsNullOrWhiteSpace(tb_Surname.Text) && !string.IsNullOrWhiteSpace(tb_PhoneNumber.Text) && !string.IsNullOrWhiteSpace(tb_CardID.Text) && gender != "" && dp_RegistrationDate.SelectedDate != null)
			{
				MySQLCommands.DropIDTable();
				MySQLCommands.InsertUsers(tb_Name.Text, tb_Surname.Text, tb_PhoneNumber.Text, gender, dp_RegistrationDate.SelectedDate.Value, cardID);
				this.Close();
			}
			else
				MessageBox.Show("Please enter all the data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void rb_Male_Checked(object sender, RoutedEventArgs e)
		{
			gender = "Male";
		}

		private void rb_Female_Checked(object sender, RoutedEventArgs e)
		{
			gender = "Female";
		}

		private void btn_GetID_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.EnterGetCardIdMode();
			FetchCardID fetchCardIDWindow = new FetchCardID(this, false);
			fetchCardIDWindow.Show();
		}

		public void SetID(string _cardID)
		{
			cardID = _cardID;
			tb_CardID.Text = cardID;
		}
	}
}
