using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.Entity;

namespace InstantMessagingService
{
    public class IMService
    {
        public static List<User> LoadUsers()
        {
            using (var c = new HttpClient())
            {
                var result = c.GetStringAsync("http://localhost:50354/api/users/").Result;
                var list = JsonConvert.DeserializeObject<List<User>>(result);
                return list;
            }
        }

        public static User GetUser(string username)
        {
            //using (var c = new HttpClient())
            //{
            //    var result = c.GetStringAsync("http://localhost:50354/api/users/" + username).Result;
            //    var user = JsonConvert.DeserializeObject<User>(result);
            //    return user;
            //}
            var userList = LoadUsers();
            var matchingUsers = userList.Where(c => c.Username == username);
            if (matchingUsers.Count() > 0)
                return matchingUsers.First();
            return null;
        }

        public static void AddUser(User user)
        {
            var userList = LoadUsers();
            if (userList.Count(c => c.Username == user.Username) > 0)
                throw new InvalidOperationException("Username already taken");

            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(user);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PostAsync("http://localhost:50354/api/users/", content).Result;
            }
        }

        public static void UpdateUser(User user)
        {
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(user);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PutAsync("http://localhost:50354/api/users/" + user.Username, content).Result;
            }
        }

        public static void DeleteUser(User user)
        {
            using (var c = new HttpClient())
            {
                var result = c.DeleteAsync("http://localhost:50354/api/users/" + user.Username).Result;
            }
        }

        // Get a user's contact list
        public static List<User> GetContacts(User user)
        {
            using (var c = new HttpClient())
            {
                var userList = new List<User>();
                var result = c.GetStringAsync("http://localhost:50354/api/contactinfoes/").Result;
                var allContactInfoes = JsonConvert.DeserializeObject<List<ContactInfo>>(result);
                foreach (var contactInfo in allContactInfoes)
                {
                    if (contactInfo.UserId == user.Username)
                    {
                        var contact = GetUser(contactInfo.ContactUsername);
                        userList.Add(contact);
                    }
                }
                return userList;
            }
        }

        public static void AddContact(User user, User contact)
        {
            using (var c = new HttpClient())
            {
                var contactInfo = new ContactInfo { UserId = user.Username, ContactUsername = contact.Username };
                var serialized = JsonConvert.SerializeObject(contactInfo);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PostAsync("http://localhost:50354/api/contactinfoes/", content).Result;
            }
        }

        public static void DeleteContact(User user, User contact)
        {
            using (var c = new HttpClient())
            {
                var messageList = GetAllMessagesBetweenContacts(user, contact);
                if (messageList != null)
                {
                    foreach (var message in messageList)
                        c.DeleteAsync("http://localhost:50354/api/messages/" + message.Id);
                }
                var result = c.DeleteAsync("http://localhost:50354/api/contactinfoes/" + contact.Username + "/" + user.Username).Result;
            }
        }

        public static List<Message> GetAllMessagesBetweenContacts(User contact1, User contact2)
        {
            using (var c = new HttpClient())
            {
                var returnedList = new List<Message>();
                var result = c.GetStringAsync("http://localhost:50354/api/messages").Result;
                var messageList = JsonConvert.DeserializeObject<List<Message>>(result);
                foreach (var message in messageList)
                {
                    if ((message.Sender == contact1.Username && message.Receiver == contact2.Username) || 
                        (message.Sender == contact2.Username && message.Receiver == contact1.Username)){
                        returnedList.Add(message);
                    }
                }
                return returnedList;
            }
        }

        public static Message GetLastMessageFromContact(User user, User contact)
        {
            var messageList = GetAllMessagesBetweenContacts(user, contact);
            if (messageList == null)
                return null;
            return messageList.OrderByDescending(c => c.TimeSent).FirstOrDefault();           
        }

        public static void SendMessage(Message message)
        {
            var sender = GetUser(message.Sender);
            var receiver = GetUser(message.Receiver);
            var senderContacts = GetContacts(sender);
            var receiverContacts = GetContacts(receiver);
            if (senderContacts.Count(c => c.Username == receiver.Username) == 0)
                AddContact(sender, receiver);
            if (receiverContacts.Count(c => c.Username == sender.Username) == 0)
                AddContact(receiver, sender);
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(message);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PostAsync("http://localhost:50354/api/messages", content).Result;
            }
        }

        public static string GetUsernameFromDb()
        {
            using (var db = new UserContext())
            {
                if (db.Users.Count() == 0)
                    return null;
                else
                    return db.Users.First().Username;
            }
        }

        public static async Task EraseUsernameFromDb()
        {
            using (var db = new UserContext())
            {
                foreach (var user in db.Users)
                    db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }

        public static async Task SetUsernameInDb(string username)
        {
            using (var db = new UserContext())
            {
                if (db.Users.Count() > 0)
                    await EraseUsernameFromDb();
                db.Users.Add(new User { Username = username });
                await db.SaveChangesAsync();
            }
        }
    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
