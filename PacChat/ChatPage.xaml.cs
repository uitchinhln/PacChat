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
        private BubbleChat _previousBubbleChat;
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

        private void SendMessage() //on the Rightside
        {
            if (ChatInput.Text.ToString().Replace(" ",string.Empty) == "") return;
            _previousBubbleChat = null;
            Bubble b = new Bubble();
            b.Messages = ChatInput.Text.ToString();
            b.SetBG(Color.FromRgb(50, 23, 108));
            b.SetTextColor(Colors.White);
            b.SetDirect(false);// true = left false = right
            b.SetSeen(false);
            spMessagesContainer.Children.Add(b);
            MessagesContainer.ScrollToEnd();
        }

        private void sendLeftMessages()
        {
            if (ChatInput.Text.ToString().Replace(" ", string.Empty) == "") return;
            if (_previousBubbleChat == null)
            {
                _previousBubbleChat = new BubbleChat();
                spMessagesContainer.Children.Add(_previousBubbleChat);
            }
            Bubble b = new Bubble();
            b.Messages = ChatInput.Text;
            b.SetSeen(false);
            b.SetBG(Color.FromRgb(246, 246, 246));
            b.SetDirect(true); // true = left false = right
            _previousBubbleChat.AddBubble(b);
            MessagesContainer.ScrollToEnd();
        }

        public void ClearChatPage()
        {
            spMessagesContainer.Children.Clear();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            sendLeftMessages();
            ChatInput.Text = "";
        }
    }
}
