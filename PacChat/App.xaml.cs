using PacChat.Windows.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using PacChat.Resources.CustomControls;
using PacChat.Utils;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ResourceUtil.PrepareResource();
            LoginWindow main = new LoginWindow();
            //MediaPlayerWindow main = new MediaPlayerWindow();
            //MainWindow main = new MainWindow();
            //TestWindows main = new TestWindows();
            main.Show();
           
        }
    }
}
