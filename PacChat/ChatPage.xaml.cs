using System;
using System.Collections.Generic;
using System.Linq;
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
using PacChat.ChatPageContents;
using PacChat.Resources.CustomControls;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        public string chatTitle 
        {  
            set
            {
                ChatTitle.Content = value;
            }
        }

        public static ChatPage Instance;

        public ChatPage()
        {
            InitializeComponent();
            Instance = this;
        }

        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Send message here
                Console.WriteLine("Send message");
                SendMessage();

                // Clear textbox
                ChatInput.Text = "";
            }
        }

        private void SendMessage()
        {
            // Add bubble chat with message here
            Bubble b = new Bubble();
            b.Messages = ChatInput.Text.ToString();
            b.SetBG(Color.FromRgb(241, 240, 240));
            b.SetSeen(false);
            spMessagesContainer.Children.Add(b);
        }
    }
}
