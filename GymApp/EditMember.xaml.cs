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
	/// <summary>
	/// Interaction logic for EditMember.xaml
	/// </summary>
	public partial class EditMember : Window
	{
		int ID;
		string name;
		string surname;
		string phone;
		string gender;
		DateTime regDate;
		string cardID;
		Members membersWindow;

		public EditMember(int _id, string _name, string _surname, string _phone, string _gender, DateTime _regDate, string _cardID, Members _membersWindow)
		{
			InitializeComponent();
			ID = _id;
			name = _name;
			surname = _surname;
			phone = _phone;
			regDate = _regDate;
			tb_Name.Text = name;
			tb_Surname.Text = surname;
			tb_PhoneNumber.Text = phone;
			tb_CardID.Text = _cardID;
			membersWindow = _membersWindow;
			cardID = _cardID;

			if (_gender == "Male")
			{
				rb_Male.IsChecked = true;
				rb_Female.IsChecked = false;
			}
			else
			{
				rb_Male.IsChecked = false;
				rb_Female.IsChecked = true;
			}			
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(tb_Name.Text) && !string.IsNullOrWhiteSpace(tb_Surname.Text) && !string.IsNullOrWhiteSpace(tb_PhoneNumber.Text) && !string.IsNullOrWhiteSpace(tb_CardID.Text) && gender != null && dp_RegistrationDate.SelectedDate != null)
			{
				name = tb_Name.Text;
				surname = tb_Surname.Text;
				phone = tb_PhoneNumber.Text;
				regDate = dp_RegistrationDate.SelectedDate.Value;
				MySQLCommands.DropIDTable();
				MySQLCommands.UpdateUser(ID, name, surname, phone, gender, regDate, cardID);
				membersWindow.RefreshMembers();
				this.Close();
			}
			else
				MessageBox.Show("Please enter all the data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

		}

		private void btn_GetID_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.EnterGetCardIdMode();
			FetchCardID fetchCardIDWindow = new FetchCardID(this, true);
			fetchCardIDWindow.Show();
		}

		private void rb_Male_Checked(object sender, RoutedEventArgs e)
		{
			gender = "Male";
		}

		private void rb_Female_Checked(object sender, RoutedEventArgs e)
		{
			gender = "Female";
		}

		public void SetID(string _cardId)
		{
			cardID = _cardId;
			tb_CardID.Text = cardID;
		}
	}
}
