using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest.Notification;
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

namespace PacChat.Resources.CustomControls.Notifications
{
    /// <summary>
    /// Interaction logic for FriendRequestNoti.xaml
    /// </summary>
    public partial class FriendRequestNoti : UserControl
    {
        public FriendRequestNoti()
        {
            InitializeComponent();
        }

        public void SetInfo(string id, string name)
        {
            ClickMask.Content = id;
            FriendName.Text = name;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            ResponseFriendRequest packet = new ResponseFriendRequest();
            packet.TargetID = ClickMask.Content.ToString();
            packet.Accepted = true;
            _ = ChatConnection.Instance.Send(packet);
            NotificationPage.Instance.RemoveNotiTag(this);
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            ResponseFriendRequest packet = new ResponseFriendRequest();
            packet.TargetID = ClickMask.Content.ToString();
            packet.Accepted = false;
            _ = ChatConnection.Instance.Send(packet);
            NotificationPage.Instance.RemoveNotiTag(this);
        }

        private void ClickMask_Click(object sender, RoutedEventArgs e)
        {
            // Display profile
        }
    }
}
