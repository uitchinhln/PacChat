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
    /// Interaction logic for UserMessage.xaml
    /// </summary>
    public partial class UserMessage : UserControl
    {
        public UserMessage()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            LoadDataToModel();
            MainWindow.chatApplication.controller.OnUserChanged();
        }

        // This method load data to model and controller get it from model and update view
        private void LoadDataToModel()
        {
            var app = MainWindow.chatApplication;
            app.model.Title = UserName.Text;

            // Load user messages from database and update model
            app.model.CurrentUserMessages = new List<string>(); // Clear, then re-load to this list

            // Switch code view to Controller in: ChatAMVC -> ChatController, in OnUserChangedEvent
        }
    }
}
