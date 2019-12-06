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
    /// Interaction logic for GroupChatPage.xaml
    /// </summary>
    public partial class GroupChatPage : UserControl
    {
        public GroupChatPage()
        {
            InitializeComponent();
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
        }
    }
}
