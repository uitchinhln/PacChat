using PacChat.Resources.CustomControls.Notifications;
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

namespace PacChat
{
    /// <summary>
    /// Interaction logic for NotificationPage.xaml
    /// </summary>
    public partial class NotificationPage : UserControl
    {
        public static NotificationPage Instance { get; private set; }

        public NotificationPage()
        {
            InitializeComponent();
            Instance = this;
        }

        public void AddNotiTag(UserControl userControl)
        {
            NotiListView.Children.Add(userControl);
        }

        public void AddFriendRequestNoti(string id, string name)
        {
            Console.WriteLine("Add: " + name);
            FriendRequestNoti friendRequestNoti = new FriendRequestNoti();
            friendRequestNoti.SetInfo(id, name);
            NotiListView.Children.Add(friendRequestNoti);
            MainWindow.Instance.SetNotificationDotState(true);
        }

        public void ClearNoti()
        {
            NotiListView.Children.Clear();
        }
    }
}
