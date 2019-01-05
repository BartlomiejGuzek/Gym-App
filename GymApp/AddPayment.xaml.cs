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
    /// Interaction logic for AddPayment.xaml
    /// </summary>
	/// 
    public partial class AddPayment : Window
    {
		Payments paymentWindow;
		DateTime issuedDate;
		int id;
		DateTime expDate;

		public AddPayment(Payments _paymenWindow, DateTime _issuedDate, int _id, DateTime _expDate)
        {
            InitializeComponent();
			paymentWindow = _paymenWindow;
			issuedDate = _issuedDate;
			id = _id;
			expDate = _expDate;
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.InsertPayment(issuedDate, id, expDate);
			paymentWindow.RefreshPaymentList();
			paymentWindow.CheckStatus();
			this.Close();
		}
	}
}
