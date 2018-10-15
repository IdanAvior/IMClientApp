using InstantMessagingService;
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

namespace IMClientApp
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public string SignInUsername { get; set; }

        public string SignInPassword { get; set; }

        public string RegisterUsername { get; set; }

        public string RegisterPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public SignInWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SignInUsername) || string.IsNullOrWhiteSpace(SignInPassword))
                    throw new InvalidOperationException("Please enter your username and password");
                var user = IMService.GetUser(SignInUsername);
                if (user == null)
                    throw new InvalidOperationException("Invalid username, please try again");
                if (user.Password != SignInPassword)
                    throw new InvalidOperationException("Wrong password, please try again");
                await IMService.SetUsernameInDb(SignInUsername);
                RaiseSignInEvent(this, new SignInEventArgs(SignInUsername));
                Close();
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event EventHandler<SignInEventArgs> RaiseSignInEvent;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IMService.GetUsernameFromDb() == null)
                Application.Current.Shutdown();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RegisterUsername) || string.IsNullOrWhiteSpace(RegisterPassword) || string.IsNullOrWhiteSpace(ConfirmPassword)
                    || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                    throw new InvalidOperationException("Please fill in all the fields.");
                if (RegisterPassword != ConfirmPassword)
                    throw new InvalidOperationException("Passwords don't match, please try again.");
                IMService.AddUser(new User { Username = RegisterUsername, Password = RegisterPassword, FirstName = FirstName, LastName = LastName });
                await IMService.SetUsernameInDb(RegisterUsername);
                RaiseSignInEvent(this, new SignInEventArgs(RegisterUsername));
                Close();
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class SignInEventArgs : EventArgs
    {
        public SignInEventArgs(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
