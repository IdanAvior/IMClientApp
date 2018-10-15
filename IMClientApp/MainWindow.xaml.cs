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
using InstantMessagingService;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Timers;

namespace IMClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //Data members

        private User _loggedInUser;

        private List<User> _userList;

        private ObservableCollection<User> _contactsCollection;

        private ObservableCollection<ContactDetails> _contactsDetails;

        private ObservableCollection<Message> _messagesCollection;

        private ContactDetails _selectedContact;

        private string _messageContent;

        private readonly int DefaultMessageRefreshTime = 2000;

        //Properties

        public User LoggedInUser
        {
            get
            {
                return _loggedInUser;
            }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged("LoggedInUser");
            }
        }

        public List<User> UserList
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList = value;
                OnPropertyChanged("UserList");
            }
        }

        public ObservableCollection<User> ContactsCollection
        {
            get
            {
                return _contactsCollection;
            }
            set
            {
                _contactsCollection = value;
                OnPropertyChanged("ContactsCollection");
            }
        }

        public ObservableCollection<ContactDetails> ContactsDetails
        {
            get
            {
                return _contactsDetails;
            }
            set
            {
                _contactsDetails = value;
                OnPropertyChanged("ContactsDetails");
            }
        }

        public ObservableCollection<Message> MessagesCollection
        {
            get
            {
                return _messagesCollection;
            }
            set
            {
                _messagesCollection = value;
                OnPropertyChanged("MessagesCollection");
            }
        }

        public ContactDetails SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        public string MessageContent
        {
            get
            {
                return _messageContent;
            }
            set
            {
                _messageContent = value;
                OnPropertyChanged("MessageContent");
            }
        }

        //Constructor

        public MainWindow()
        {
            var username = IMService.GetUsernameFromDb();
            if (username != null && IMService.GetUser(username) != null)
            {
                LoggedInUser = IMService.GetUser(username);
                InitProgram();
                var timer = new System.Threading.Timer(state => CheckForNewMessagesAndContacts(), null, DefaultMessageRefreshTime, Timeout.Infinite);
            }
            else
            {
                Hide();
                Login();
            }
        }

        private void CheckForNewMessagesAndContacts()
        {       
            if (LoggedInUser != null && SelectedContact != null)
            {
                var messagesList = IMService.GetAllMessagesBetweenContacts(LoggedInUser, GetSelectedUser());
                foreach (var message in messagesList)
                    if (MessagesCollection.Count(c => c.Id == message.Id) == 0)
                        MessagesCollection.Add(message);
                var contactsList = IMService.GetContacts(LoggedInUser);
                foreach (var contact in contactsList)
                    if (ContactsCollection.Count(c => c.Username == contact.Username) == 0)
                    {
                        ContactsCollection.Add(contact);
                        AddToContactsDetails(contact);
                    }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitProgram()
        {
            InitializeComponent();
            DataContext = this;

            UserList = IMService.LoadUsers();

            var contactList = IMService.GetContacts(LoggedInUser);
            InitContactsCollection(contactList);

            InitContactsDetails();
            if (ContactsDetails != null && ContactsDetails.Count > 0)
                SelectedContact = ContactsDetails.First();
            InitMessagesCollection();
            Show();
        }

        private void InitContactsCollection(List<User> contactList)
        {
            if (ContactsCollection == null)
                ContactsCollection = new ObservableCollection<User>();
            else
                foreach (var contact in ContactsCollection.ToList())
                    ContactsCollection.Remove(contact);
            foreach (var contact in contactList)
                ContactsCollection.Add(contact);
        }

        private void InitContactsDetails()
        {
            ContactsDetails = new ObservableCollection<ContactDetails>();
            PopulateContactsDetails();
        }

        private void ResetContactsDetails()
        {
            foreach (var contactDetails in ContactsDetails.ToList())
                ContactsDetails.Remove(contactDetails);
            PopulateContactsDetails();
        }

        private void PopulateContactsDetails()
        {
            foreach (var contact in ContactsCollection)
            {
                AddToContactsDetails(contact);
            }
        }

        private void AddToContactsDetails(User contact)
        {
            var lastMessage = IMService.GetLastMessageFromContact(LoggedInUser, contact);
            if (lastMessage != null)
                ContactsDetails.Add(new ContactDetails
                {
                    Username = contact.Username,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    LastMessageTime = lastMessage.TimeSent,
                    LastMessageContent = lastMessage.Content
                });
            else
                ContactsDetails.Add(new ContactDetails { Username = contact.Username, FirstName = contact.FirstName, LastName = contact.LastName });
        }

        private void InitMessagesCollection()
        {
            MessagesCollection = new ObservableCollection<Message>();
            if (SelectedContact != null)
                PopulateMessageCollection();
        }

        private void PopulateMessageCollection()
        {
            var messageList = IMService.GetAllMessagesBetweenContacts(LoggedInUser, GetSelectedUser());
            foreach (var message in messageList)
                MessagesCollection.Add(message);
        }

        private void ResetMessagesCollection()
        {
            foreach (var message in MessagesCollection.ToList())
                MessagesCollection.Remove(message);
            if (SelectedContact != null)
                PopulateMessageCollection();
        }

        public void Login()
        {
            if (IMService.GetUsernameFromDb() == null)
            {
                var signInWindow = new SignInWindow();
                signInWindow.RaiseSignInEvent += SignInButton_RaiseSignInEvent;
                signInWindow.Show();
            }
        }

        private void Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedContact = Contacts.SelectedItem as ContactDetails;
            ResetMessagesCollection();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageContent != null && MessageContent != "" && SelectedContact != null)
            {
                var message = new Message { Sender = LoggedInUser.Username, Receiver = SelectedContact.Username, TimeSent = DateTime.Now, Content = MessageContent };
                IMService.SendMessage(message);
                MessagesCollection.Add(message);
                MessageContent = "";
                SelectedContact.LastMessageContent = message.Content;
                SelectedContact.LastMessageTime = message.TimeSent;
            }
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            var addContactWindow = new AddContactWindow();
            addContactWindow.RaiseContactAddedEvent += AddContactButton_RaiseContactAddedEvent;
            addContactWindow.Show();
        }

        public void AddContactButton_RaiseContactAddedEvent(object sender, ContactAddedEventArgs e)
        {
            var contact = IMService.GetUser(e.Username);
            if (contact != null)
            {
                IMService.AddContact(LoggedInUser, contact);
                ContactsCollection.Add(contact);
                ContactsDetails.Add(new ContactDetails { Username = contact.Username, FirstName = contact.FirstName, LastName = contact.LastName });
            }
        }

        public void SignInButton_RaiseSignInEvent(object sender, SignInEventArgs e)
        {
            LoggedInUser = IMService.GetUser(e.Username);
            InitProgram();
        }

        private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedContact != null)
                if (MessageBox.Show("Are you sure?", "Delete contact", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    DeleteSelectedContact();
        }

        private void DeleteSelectedContact()
        {
            IMService.DeleteContact(LoggedInUser, GetSelectedUser());
            foreach (var contact in ContactsCollection.ToList())
                if (contact.Username == SelectedContact.Username)
                    ContactsCollection.Remove(contact);
            foreach (var message in MessagesCollection.ToList())
                MessagesCollection.Remove(message);
            ResetContactsDetails();
        }

        private User GetSelectedUser()
        {
            if (SelectedContact == null)
                return null;
            return IMService.GetUser(SelectedContact.Username);
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await IMService.EraseUsernameFromDb();
                var signInWindow = new SignInWindow();
                signInWindow.RaiseSignInEvent += SignInButton_RaiseSignInEvent;
                signInWindow.Show();
                Hide();
            }
        }

    }
}
