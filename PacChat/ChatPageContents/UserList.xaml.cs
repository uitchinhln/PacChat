using PacChat.ChatPageContents.ViewModels;
using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest;
using PacChat.Network.Packets.AfterLoginRequest.Search;
using PacChat.Utils;
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

namespace PacChat.ChatPageContents
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        Color lostFocusColor = Color.FromRgb(100, 90, 150);
        public static UserList Instance { get; set; }
        public object GetFriendID { get; private set; }

        public UserList()
        {
            InitializeComponent();
            selectTab(ContactsTab);
            Trans.SelectedIndex = 1;
            Instance = this;
        }

        private void selectTab(Button button)
        {
            RecentTab.Background = ContactsTab.Background = new SolidColorBrush(lostFocusColor);
            RecentTab.Foreground = ContactsTab.Foreground = new SolidColorBrush(Color.FromArgb(75, 255, 255, 255));
            button.Background = Brushes.Transparent;
            button.Foreground = Brushes.White;
        }

        private void RecentTab_Click(object sender, RoutedEventArgs e)
        {
            Trans.SelectedIndex = 0;
            selectTab(RecentTab);
        }

        private void ContactsTab_Click(object sender, RoutedEventArgs e)
        {
            Trans.SelectedIndex = 1;
            selectTab(ContactsTab);
        }

        public void ListViewUpdate()
        {
            ListView.Children.Clear();
            foreach (var user in UserListDesignModel.Instance.Contacts)
            {
                UserMessage userControl = new UserMessage();
                userControl.SetInfo(user.Id, user.Name, user.IncomingMsg);
                ListView.Children.Add(userControl);
            }
        }

        public void ClearListView()
        {
            ListView.Children.Clear();

            var app = MainWindow.chatApplication;
            app.model.UserControls.Clear();
        }

        public void AddUserToListView(UserMessageViewModel user)
        {
            UserMessage userControl = new UserMessage();
            userControl.SetInfo(user.Id, user.Name, user.IncomingMsg);
            userControl.SetOnlineStatus(user.IsOnline);
            var app = MainWindow.chatApplication;
            app.model.UserControls.Add(user.Id, userControl);
            ListView.Children.Add(userControl);
        }

        private void UserSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserSearchBox.Text.Length == 0)
            {
                Packets.SendPacket<GetFriendIDs>();
                return;
            }

            SearchUser packet = new SearchUser();
            packet.Email = UserSearchBox.Text;
            _ = ChatConnection.Instance.Send(packet);
        }
    }
}
