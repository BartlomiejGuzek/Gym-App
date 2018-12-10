﻿using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace GymApp
{
    public partial class Payments : Window
    {
		int ID;
		string name;
		string surname;
		string phone;
		string gender;
		DateTime regDate;
		string cardID;
		DataTable dataTable = new DataTable();
		string status;
		string expireDate;
		DateTime expireDateFormat;

		public Payments(int _id, string _name, string _surname, string _phone, string _gender, DateTime _regDate, string _cardID)
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
			cardID = _cardID;
			expireDate = MySQLCommands.GetNewestPayment(ID);
			if(expireDate != null)
				expireDateFormat = DateTime.Parse(expireDate);
			RefreshPaymentList();
		}

		public void RefreshPaymentList()
		{
			dataTable.Load(MySQLCommands.GetPayments(ID).ExecuteReader());
			dg_Payments.DataContext = dataTable;
			if (expireDateFormat >= DateTime.Today)
			{
				tb_Status.Text = "Active";
				tb_Status.Foreground = System.Windows.Media.Brushes.GreenYellow;
			}
			else
			{
				tb_Status.Text = "Not Active";
				tb_Status.Foreground = System.Windows.Media.Brushes.Red;
			}
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void dg_Payments_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void btn_Pay_Click(object sender, RoutedEventArgs e)
		{
			AddPayment addPaymentWindow = new AddPayment(this, DateTime.Today.AddDays(30), ID);
			addPaymentWindow.Owner = Application.Current.MainWindow;
			addPaymentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			addPaymentWindow.Show();
		}
	}
}
