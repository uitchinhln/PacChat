using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserListDesignModel : UserListViewModel
    {
        public static UserListDesignModel Instance => _instance == null ? _instance = new UserListDesignModel() : _instance;
        private static UserListDesignModel _instance;

        public UserListDesignModel()
        {
            // Load all user from server

            Users = new List<UserMessageViewModel>()
            {
                new UserMessageViewModel
                {
                    Name = "Luke",
                    IncomingMsg = "Hi there, this is Luke. Please send me your plan soon."
                },

                new UserMessageViewModel
                {
                    Name = "Lucy",
                    IncomingMsg = "This video tutorial is awesome! I bet it is the best"
                },

                new UserMessageViewModel
                {
                    Name = "Liam",
                    IncomingMsg = "Hello, in which video you show how to handling exceptions?"
                },

                new UserMessageViewModel
                {
                    Name = "Luke",
                    IncomingMsg = "Hi there, this is Luke. Please send me your plan soon."
                },
            };
        }

    }
}
