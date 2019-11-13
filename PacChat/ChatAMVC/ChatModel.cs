using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacChat.MVC;
using PacChat.Utils;
using PacChat.ChatPageContents.ViewModels;

namespace PacChat.ChatAMVC
{
    public class ChatModel : Model<ChatApplication>
    {
        #region OnChatPage
        public string Title { get; set; } = UserListDesignModel.Instance.Contacts[0].Name;

        public string previousSelectedUser { get; set; } = "";
        public string currentSelectedUser { get; set; } = UserListDesignModel.Instance.Contacts[0].Id;
        public List<BubbleInfo> CurrentUserMessages { get; set; } = new List<BubbleInfo>();
        public Dictionary<string, List<BubbleInfo>> ContactsMessages { get; set; } = new Dictionary<string, List<BubbleInfo>>();
        public void InitContacts()
        {
            List<UserMessageViewModel> users = UserListDesignModel.Instance.Contacts;
            foreach (var user in users)
            {
                ContactsMessages.Add(user.Id, new List<BubbleInfo>());
            }
        }
        #endregion

        #region OnSettingPage
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        #endregion
    }
}
