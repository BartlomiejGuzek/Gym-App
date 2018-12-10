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
		DateTime expires;
		int id;
		public AddPayment(Payments _paymenWindow, DateTime _expires, int _id)
        {
            InitializeComponent();
			paymentWindow = _paymenWindow;
			expires = _expires;
			id = _id;
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
			MySQLCommands.InsertPayment(expires, id);
			paymentWindow.RefreshPaymentList();
			this.Close();
		}
	}
}
