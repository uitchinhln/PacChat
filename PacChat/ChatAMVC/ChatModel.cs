using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacChat.MVC;
using PacChat.ChatPageContents.ViewModels;

namespace PacChat.ChatAMVC
{
    public class ChatModel : Model<ChatApplication>
    {
        #region OnChatPage
        public string Title 
        { 
            get
            {
                return _chatTitle;
            }

            set
            {
                _chatTitle = value;
                ChatViewModel.Title = _chatTitle;
            }
        }
        private string _chatTitle;

        public List<string> CurrentUserMessages { get; set; }
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
