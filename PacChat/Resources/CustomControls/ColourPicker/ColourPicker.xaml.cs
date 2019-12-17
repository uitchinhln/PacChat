using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace PacChat.Resources.CustomControls.ColourPicker
{

    public partial class ColourPicker : UserControl
    {
        public ColourPicker()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            var chatApplication = MainWindow.chatApplication;
            Console.WriteLine(ColorPicker.Color);
            ChatPage.Instance.bubbleColor = ColorPicker.Color;


            ChatPage.Instance.ChatTitle.Content = chatApplication.model.Title;

            // Store chat content before switching
            if (chatApplication.model.previousSelectedConversation != "")
                ChatPage.Instance.StoreChatPage(chatApplication.model.previousSelectedConversation);

            // Clear Scrollview content
            ChatPage.Instance.ClearChatPage();

            // Load target user messages (from model) and add to scrollview
            ChatPage.Instance.LoadChatPage(chatApplication.model.currentSelectedConversation, chatApplication.model.SelfID);
        }

        public Color GetColor()
        {
            return ColorPicker.Color;
        }
    }
}

