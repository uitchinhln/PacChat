using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;

namespace PacChat.ChatAMVC
{
    public class ChatController : Controller<ChatApplication>
    {
        public int currentSelectedUser;

        public void OnUserChanged()
        {
            var chatApplication = MainWindow.chatApplication;
            ChatPage.Instance.ChatTitle.Content = chatApplication.model.Title;
        }
    }
}
