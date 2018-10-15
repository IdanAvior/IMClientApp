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
using InstantMessagingService;

namespace IMClientApp
{
    /// <summary>
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        public string ContactUsername { get; set; }

        public AddContactWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var user = IMService.GetUser(ContactUsername);
            if (user == null)
                MessageBox.Show("Invalid username. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                RaiseContactAddedEvent(this, new ContactAddedEventArgs(user.Username));
                Close();
                return;
            }
        }

        public event EventHandler<ContactAddedEventArgs> RaiseContactAddedEvent;
    }

    public class ContactAddedEventArgs : EventArgs
    {
        public ContactAddedEventArgs(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
