using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace InstantMessagingService
{
    public class User : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _firstName;
        private string _lastName;
        private List<ContactInfo> _contacts;

        [Key]
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public List<ContactInfo> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                NotifyPropertyChanged("Contacts");
            }
        }

        public User()
        {
            _contacts = new List<ContactInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ContactInfo : INotifyPropertyChanged
    {
        private string _contactUsername;

        private string _userId;

        [Key, Column(Order = 0)]
        public string ContactUsername
        {
            get
            {
                return _contactUsername;
            }
            set
            {
                _contactUsername = value;
                NotifyPropertyChanged("ContactUsername");
            }
        }

        [Key, Column(Order = 1)]
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                NotifyPropertyChanged("UserId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
