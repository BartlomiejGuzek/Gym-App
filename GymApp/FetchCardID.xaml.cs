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
    /// Interaction logic for FetchCardID.xaml
    /// </summary>
    public partial class FetchCardID : Window
    {
		System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		string cardID;
		AddMember addMemberWindow;
		EditMember editMemberWindow;
		bool isEdit;

		public FetchCardID(AddMember _addMemberWindow, bool _isEdit)
        {
            InitializeComponent();
			addMemberWindow = _addMemberWindow;
			dispatcherTimer.Tick += dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
			dispatcherTimer.Start();
			isEdit = false;
		}

		public FetchCardID(EditMember _editMemberWindow, bool _isEdit)
		{
			InitializeComponent();
			editMemberWindow = _editMemberWindow;
			dispatcherTimer.Tick += dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
			dispatcherTimer.Start();
			isEdit = true;
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			if (MySQLCommands.GetCardID() == "1")
			{
					
			}
			else
			{
				cardID = MySQLCommands.GetCardID();
				if(isEdit)
				{
					editMemberWindow.SetID(cardID);
					dispatcherTimer.Stop();
					this.Close();
				}
				else
				{
					addMemberWindow.SetID(cardID);
					dispatcherTimer.Stop();
					this.Close();
				}
			}
				
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			dispatcherTimer.Stop();
			this.Close();
		}
	}
}
