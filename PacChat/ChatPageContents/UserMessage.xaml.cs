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
using PacChat.MessageCore.Message;
using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest.Notification;
using PacChat.Network.Packets.AfterLoginRequest.Profile;
using PacChat.Utils;

namespace PacChat.ChatPageContents
{
    /// <summary>
    /// Interaction logic for UserMessage.xaml
    /// </summary>
    public partial class UserMessage : UserControl
    {
        public bool IsFriend { get; set; }

        public UserMessage()
        {
            InitializeComponent();
            UserMessage1.FontWeight = FontWeights.Normal;
            IncomingMask.Visibility = Visibility.Hidden;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsFriend) return;
            UserMessage1.FontWeight = FontWeights.Normal;
            IncomingMask.Visibility = Visibility.Hidden;
            LoadDataToModel();
            MainWindow.chatApplication.controller.OnUserChanged(ClickMask.Content.ToString());
        }

        // This method load data to model and controller get it from model and update view
        private void LoadDataToModel()
        {
            var app = MainWindow.chatApplication;
            app.model.Title = UserName.Text;
            app.model.previousSelectedConversation = app.model.currentSelectedConversation;

            if (app.model.PrivateConversations.ContainsKey(ClickMask.Content.ToString()))
            {
                app.model.currentSelectedConversation = app.model.PrivateConversations[ClickMask.Content.ToString()];
            }

            // Switch code view to Controller in: ChatAMVC -> ChatController, in OnUserChangedEvent
        }

        public void SetInfo(string Id, string Name, string Msg, bool Friend = false)
        {
            UserName.Text = Name;
            UserMessage1.Text = Msg;
            ClickMask.Content = Id;
            FriendRequestBtn.IsEnabled = !Friend;
            IsFriend = Friend;
        }

        public void SetOnlineStatus(bool online)
        {
            OnlineStatus.Visibility = online ? Visibility.Visible : Visibility.Hidden;
        }

        private void ClickMask_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            GetDisplayedProfile packet = new GetDisplayedProfile();
            packet.TargetID = ClickMask.Content.ToString();
            _ = ChatConnection.Instance.Send(packet);
        }

        private void FriendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            FriendRequest packet = new FriendRequest();
            packet.TargetID = ClickMask.Content.ToString();
            _ = ChatConnection.Instance.Send(packet);
        }

        public void IncomingMessage(AbstractMessage message, bool seeing)
        {
            if (message is AttachmentMessage)
            {
                UserMessage1.Text = "Attachment";
            }

            if (message is TextMessage)
            {
                UserMessage1.Text = (message as TextMessage).Message;
            }

            if (message is StickerMessage)
            {
                UserMessage1.Text = "Sticker";
            }

            if (!seeing)
            {
                IncomingMask.Visibility = Visibility.Visible;
                UserMessage1.FontWeight = FontWeights.Bold;
            }
        }
    }
}
