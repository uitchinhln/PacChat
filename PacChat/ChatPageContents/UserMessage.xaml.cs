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
using PacChat.Utils;

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
            app.model.previousSelectedUser = app.model.currentSelectedUser;
            app.model.currentSelectedUser = ClickMask.Content.ToString();

            // Switch code view to Controller in: ChatAMVC -> ChatController, in OnUserChangedEvent
        }

        public void SetInfo(string Id, string Name, string Msg)
        {
            UserName.Text = Name;
            UserMessage1.Text = Msg;
            ClickMask.Content = Id;
        }
    }
}
