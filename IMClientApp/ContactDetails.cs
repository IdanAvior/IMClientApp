using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IMClientApp
{
    public class ContactDetails : INotifyPropertyChanged
    {
        private string _username;

        private string _firstName;

        private string _lastName;

        private string _lastMessageContent;

        private DateTime _lastMessageTime;

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

        public string LastMessageContent
        {
            get
            {
                return _lastMessageContent;
            }
            set
            {
                _lastMessageContent = value;
                NotifyPropertyChanged("LastMessageContent");
            }
        }

        public DateTime LastMessageTime
        {
            get
            {
                return _lastMessageTime;
            }
            set
            {
                _lastMessageTime = value;
                NotifyPropertyChanged("LastMessageTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
