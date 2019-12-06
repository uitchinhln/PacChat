using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer
{
    public class ServerSettings
    {
        public static IPAddress SERVER_HOST { get; private set; } = IPAddress.Parse("127.0.0.1");
        public static int SERVER_PORT { get; private set; } = 1402;

        public static int FILESERVER_PORT { get; private set; } = 1403;

        public static String MYSQL_HOST { get; private set; } = "localhost";
        public static uint MYSQL_PORT { get; private set; } = 3306;
        public static String MYSQL_USER { get; private set; } = "root";
        public static String MYSQL_PASSWORD { get; private set; } = "!2#1@3PacChat008";
        public static String MYSQL_DATABASE { get; private set; } = "pacchat";
        
        public ServerSettings()
        {

        }

        public void LoadSettings()
        {

        }
    }
}
