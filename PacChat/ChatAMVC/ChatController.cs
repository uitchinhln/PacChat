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
        public void OnUserChanged()
        {
            var chatApplication = MainWindow.chatApplication;
            ChatPage.Instance.ChatTitle.Content = chatApplication.model.Title;
            
            // Store chat content before switching
            if (chatApplication.model.previousSelectedUser != "")
                ChatPage.Instance.StoreChatPage(chatApplication.model.previousSelectedUser);

            // Clear Scrollview content
            ChatPage.Instance.ClearChatPage();

            // Load target user messages (from model) and add to scrollview
            ChatPage.Instance.LoadChatPage(chatApplication.model.currentSelectedUser);

        }

        public void OnProfileChanged()
        {
            var chatApplication = MainWindow.chatApplication;

            // Get data from model and update database
            var model = chatApplication.model;
            // Update from here
            Console.WriteLine("Get data from model and update Database");
        }
    }
}
