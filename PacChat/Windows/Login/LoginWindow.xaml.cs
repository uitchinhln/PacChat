﻿using System;
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

            dpDoB.DisplayDateEnd = new DateTime(DateTime.Now.Year - 13, 12, 31);

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
            this.Close();
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

        private void Banner_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (!(e.Uri.ToString().Equals("https://cs15083a2f24621x4ad7x9d4.z23.web.core.windows.net/", StringComparison.InvariantCultureIgnoreCase)))
            {               
                System.Diagnostics.Process.Start(e.Uri.ToString());
                e.Cancel = true;
            }
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }
    }
}
