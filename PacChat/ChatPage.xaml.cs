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
using Microsoft.Win32;
using PacChat.Network.Packets.AfterLoginRequest.Message;
using PacChat.Network;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        private BubbleChat _previousBubbleChat;

        public static ChatPage Instance;

        public ChatPage()
        {
            InitializeComponent();
            Instance = this;

            spTabStickerContainner.Children.Add(new TabStickerContainner(this));

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

        public void SendMessage(TextMessage msg, bool isSimulating = false) //on the Rightside
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
            app.model.Conversations[app.model.currentSelectedConversation].Bubbles.Add(new BubbleInfo(msg.Message, false));

            SendTextMessage packet = new SendTextMessage();
            packet.ConversationID = app.model.ContactsMessages[app.model.currentSelectedUser].ConversationID;
            packet.Message = msg;
            packet.TargetID = app.model.currentSelectedUser;
            _ = ChatConnection.Instance.Send(packet);
        }

        public void SendLeftMessages(TextMessage msg, bool isSimulating = false)
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
            app.model.Conversations[app.model.currentSelectedConversation].Bubbles.Add(new BubbleInfo(msg.Message, true));
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
            ConversationBubble msgList = app.model.ContactsMessages[id];

            if (msgList.ConversationID.Equals("~"))
            {
                SingleConversationFrUserID packet = new SingleConversationFrUserID();
                packet.UserID = id;
                _ = ChatConnection.Instance.Send(packet);
                return;
            }

            for (int i = 0; i < msgList.Bubbles.Count; ++i)
            {
                var bubbleInfo = msgList.Bubbles[i];
                if (bubbleInfo.onLeft)
                    SendLeftMessages(new TextMessage() { Message = bubbleInfo.message }, true);
                else
                    SendMessage(new TextMessage() { Message = bubbleInfo.message }, true);
            }
        }

        public void StoreChatPage(string id)
        {
            return;

            var app = MainWindow.chatApplication;

            Console.WriteLine("Store chat page on id: " + id + " with length: " + app.model.CurrentUserMessages.Count);

            ConversationBubble msgList = app.model.ContactsMessages[id];
            msgList.Bubbles.AddRange(app.model.CurrentUserMessages);

            app.model.CurrentUserMessages.Clear();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendLeftMessages(new TextMessage() { Message = ChatInput.Text });
            ChatInput.Text = "";
        }

        public void sendSticker(bool clickable, int id, int cateid, int size, int duration, string uriSheet)
        {
            Sticker sticker = new Sticker(this, clickable, id, cateid, size, duration, uriSheet);
            
            Thickness margin = sticker.Margin;
            margin.Right = 30;
            sticker.HorizontalAlignment = HorizontalAlignment.Right;
            sticker.Margin = margin;
            spMessagesContainer.Children.Add(sticker);
            MessagesContainer.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sticker sticker = new Sticker(this, false, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (13).png");
            Thickness margin = sticker.Margin;
            _previousBubbleChat = null;
            margin.Left = 30;
            sticker.HorizontalAlignment = HorizontalAlignment.Left;
            sticker.Margin = margin;
            spMessagesContainer.Children.Add(sticker);
            MessagesContainer.ScrollToEnd();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSendImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            ImageContainner image;
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _previousBubbleChat = null;
                image = new ImageContainner(1, op.FileName);
                image.HorizontalAlignment = HorizontalAlignment.Right;

                Thickness margin = image.Margin;
                margin.Right = 30;
                image.Margin = margin;
                spMessagesContainer.Children.Add(image);
                MessagesContainer.ScrollToEnd();
            }

        }

        private void showLeftImage(int id, string uri)
        {
            ImageContainner image = new ImageContainner(1, uri);
            image.HorizontalAlignment = HorizontalAlignment.Right;

            Thickness margin = image.Margin;
            margin.Left = 30;
            image.Margin = margin;
            spMessagesContainer.Children.Add(image);
            MessagesContainer.ScrollToEnd();
        }
    }
}
