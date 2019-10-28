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
using System.Windows.Shapes;

namespace PacChat.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginModel loginModel;
        LoginView loginView;
        LoginController loginController;
        LoginApp loginApp;

        public LoginWindow()
        {
            InitializeComponent();
            loginModel = new LoginModel();
            loginView = new LoginView();
            loginController = new LoginController();

            loginApp = new LoginApp();
            loginApp.InitializeMVC(loginModel, loginView, loginController);
        }

        private void FormDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                } catch (Exception) { }
            }
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TabClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(66 + (56 * index), 23, 0, 0);

            if (trnsEr.SelectedIndex != index)
            {
                trnsEr.SelectedIndex = index;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginView.Login();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            loginView.Register();
        }
    }
}
