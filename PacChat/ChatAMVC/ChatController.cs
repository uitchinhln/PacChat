﻿using System;
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

            // Clear Scrollview content
            ChatPage.Instance.MessagesContainer.Content = null;

            // Load target user messages (from model) and add to scrollview
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