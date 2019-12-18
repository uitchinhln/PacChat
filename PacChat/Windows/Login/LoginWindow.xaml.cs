using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PacChat.Windows.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            dpDoB.DisplayDateEnd = new DateTime(DateTime.UtcNow.Year - 13, 12, 31);

            LoginView loginView = DataContext as LoginView;
            if (loginView.CloseAction == null)
            {
                loginView.CloseAction = new Action(() => this.Close());
            }
            if (loginView.MoveToTab == null)
            {
                loginView.MoveToTab = new Action<int>((index) =>
                {
                    MoveToTab(index);
                });
            }
            if (loginView.ClearRegisterForm == null)
            {
                loginView.ClearRegisterForm = new Action(() =>
                {
                    loginView.RegFirstName = String.Empty;
                    loginView.RegLastName = String.Empty;
                    loginView.RegUserName = String.Empty;
                    RegPassword.Password = String.Empty;
                    loginView.RegToUAgrement = false;
                });
            }
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

        public void CloseAllExcldueThis()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow || window is MediaPlayerWindow)
                {
                    window.Close();
                }
            }
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void TabClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            MoveToTab(index);
        }

        private void MoveToTab(int index)
        {
            GridCursor.Margin = new Thickness(66 + (56 * index), 23, 0, 0);

            if (trnsEr.SelectedIndex != index)
            {
                trnsEr.SelectedIndex = index;
            }
        }
    }
}
