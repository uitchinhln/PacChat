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
        #endregion
    }
}
