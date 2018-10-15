using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace InstantMessagingService
{
    public class Message : INotifyPropertyChanged
    {
        private int _id;

        private DateTime _timeSent;

        private string _sender;

        private string _receiver;

        private string _content;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public DateTime TimeSent
        {
            get
            {
                return _timeSent;
            }
            set
            {
                _timeSent = value;
                NotifyPropertyChanged("TimeSent");
            }
        }

        public string Sender
        {
            get
            {
                return _sender;
            }
            set
            {
                _sender = value;
                NotifyPropertyChanged("Sender");
            }
        }

        public string Receiver
        {
            get
            {
                return _receiver;
            }
            set
            {
                _receiver = value;
                NotifyPropertyChanged("Receiver");
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
