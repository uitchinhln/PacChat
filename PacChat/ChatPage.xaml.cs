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
using PacChat.MessageCore.Message;
using PacChat.Utils;

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

        // Chat Input KeyDown if and only if message is text message
        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Send message here
                Console.WriteLine("Send message");
                if (ChatInput.Text == "") return;

                SendMessage(new TextMessage() { Message = ChatInput.Text });  

                // Clear textbox
                ChatInput.Text = "";
            }
        }

        private void SendMessage(TextMessage msg, bool isSimulating = false) //on the Rightside
        {
            _previousBubbleChat = null;

            Bubble b = new Bubble();
            b.Messages = msg.Message;
            b.SetBG(Color.FromRgb(50, 23, 108));
            b.SetTextColor(Colors.White);
            b.SetDirect(false);// true = left false = right
            b.SetSeen(false);

            spMessagesContainer.Children.Add(b);
            MessagesContainer.ScrollToEnd();

            if (isSimulating) return;

            var app = MainWindow.chatApplication;
            app.model.CurrentUserMessages.Add(new BubbleInfo(msg.Message, false));
        }

        private void sendLeftMessages(TextMessage msg, bool isSimulating = false)
        {
            if (_previousBubbleChat == null)
            {
                _previousBubbleChat = new BubbleChat();
                spMessagesContainer.Children.Add(_previousBubbleChat);
            }

            Bubble b = new Bubble();
            b.Messages = msg.Message;
            b.SetSeen(false);
            b.SetBG(Color.FromRgb(246, 246, 246));
            b.SetDirect(true); // true = left false = right
            _previousBubbleChat.AddBubble(b);
            MessagesContainer.ScrollToEnd();

            if (isSimulating) return;

            var app = MainWindow.chatApplication;
            app.model.CurrentUserMessages.Add(new BubbleInfo(msg.Message, true));
        }


        private void addSticker(int id, int duration, int size)
        {

        }

        public void ClearChatPage()
        {
            _previousBubbleChat = null;
            spMessagesContainer.Children.Clear();
        }

        public void LoadChatPage(string id)
        {
            Console.WriteLine("Load chat page on id: " + id);

            var app = MainWindow.chatApplication;
            List<BubbleInfo> msgList = app.model.ContactsMessages[id];

            foreach (BubbleInfo bubbleInfo in msgList)
            {
                if (bubbleInfo.onLeft)
                    sendLeftMessages(new TextMessage() { Message = bubbleInfo.message }, true);
                else
                    SendMessage(new TextMessage() { Message = bubbleInfo.message }, true);
            }
        }

        public void StoreChatPage(string id)
        {
            var app = MainWindow.chatApplication;

            Console.WriteLine("Store chat page on id: " + id + " with length: " + app.model.CurrentUserMessages.Count);

            List<BubbleInfo> msgList = app.model.ContactsMessages[id];
            msgList.AddRange(app.model.CurrentUserMessages);

            Console.WriteLine("After additionally store: " + msgList.Count);

            app.model.CurrentUserMessages.Clear();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            sendLeftMessages(new TextMessage() { Message = ChatInput.Text });
            ChatInput.Text = "";
        }
    }
}
