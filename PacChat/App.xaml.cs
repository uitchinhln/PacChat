using PacChat.Network;
using PacChat.Network.Protocol;
using PacChat.Windows;
using PacChat.Windows.Login;
using PacChat.Windows.SplashScreen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _ = ChatConnection.Instance;

            //SplashWindow splash = new SplashWindow();
            //splash.Show();

            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }
}
