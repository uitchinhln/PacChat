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

        public UserList()
        {
            InitializeComponent();
            selectTab(ContactsTab);
            Trans.SelectedIndex = 1;
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
    }
}
