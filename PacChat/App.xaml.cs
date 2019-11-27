﻿using PacChat.Windows.Login;
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

namespace PacChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //LoginWindow login = new LoginWindow();
            //login.Show();
            //MainWindow main = new MainWindow();
            TestWindows main = new TestWindows();
            main.Show();
            
        }
    }
}
