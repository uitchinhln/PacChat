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
    /// Interaction logic for ConversationControl.xaml
    /// </summary>
    public partial class ConversationControl : UserControl
    {
        public long LastActive { get; set; }
        public ConversationControl()
        {
            InitializeComponent();
        }

        public void SetInfo(string conversationID, string conversationName, long lastActive)
        {
            ClickMask.Content = conversationID;
            ConvName.Text = conversationName;
            OnlineStatus.Visibility = lastActive == 0 ? Visibility.Visible : Visibility.Hidden;
            LastActive = lastActive;
        }

        private void ClickMask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
