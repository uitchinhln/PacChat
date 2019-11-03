using PacChat.Network;
using PacChat.Network.Packets.Ping;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PacChat.Windows.SplashScreen
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            Load();
        }

        public async void Load()
        {
            ChatConnection connection = ChatConnection.Instance;
            if (!connection.IsConnected())
            {
                await connection.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1402));
            }
            Ping4Send ping = new Ping4Send();
            ping.Version = "1.0.0";
            if (connection.Session != null) connection.Session.Send(ping);
        }
    }
}
