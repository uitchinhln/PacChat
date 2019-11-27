using PacChat.ChatAMVC;
using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserListDesignModel : UserListViewModel
    {
        public static UserListDesignModel Instance => _instance == null ? _instance = new UserListDesignModel() : _instance;
        private static UserListDesignModel _instance;

        public UserList userList;

        public UserListDesignModel()
        {
            // Load all user from server
            /*   
            RecentUsers = new List<UserMessageViewModel>()
            {
                new UserMessageViewModel
                {   
                    Id = "pac01",
                    Name = "Luke",
                    IncomingMsg = "Hi there, this is Luke. Please send me your plan soon."
                },

                new UserMessageViewModel
                {
                    Id = "pac02",
                    Name = "Lucy",
                    IncomingMsg = "This video tutorial is awesome! I bet it is the best"
                },

                new UserMessageViewModel
                {
                    Id = "pac03",
                    Name = "Liam",
                    IncomingMsg = "Hello, in which video you show how to handling exceptions?"
                },

                new UserMessageViewModel
                {
                    Id = "pac04",
                    Name = "Luke",
                    IncomingMsg = "Hi there, this is Luke. Please send me your plan soon."
                },
            };
            */
        }

        public void LoadContacts()
        {
            Console.WriteLine("Load contacts");

            // Load all contacts from server database
            Contacts = new List<UserMessageViewModel>();

            foreach (var userID in ChatModel.FriendIDs)
            {
                var contact =
                    new UserMessageViewModel
                    {
                        Id = userID,
                        Name = ChatModel.FriendShortProfiles[userID].Name,
                        IncomingMsg = ChatModel.FriendShortProfiles[userID].IncomingMsg 
                    };
                Contacts.Add(contact);
                Console.WriteLine("Con: " + contact.Name);
            }

            MainWindow.chatApplication.model.InitContacts();
            Application.Current.Dispatcher.Invoke(() => UserList.Instance.ListViewUpdate());
        }
    }
}
