using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static String WEBSERVER_HOST { get; private set; } = "http://127.0.0.1";
        public static int WEBSERVER_PORT { get; private set; } = 1403;
        public static long MAX_SIZE_UPLOAD { get; private set; } = 1024 * 1024 * 25;

        public static String MYSQL_HOST { get; private set; } = "localhost";
        public static uint MYSQL_PORT { get; private set; } = 3306;
        public static String MYSQL_USER { get; private set; } = "root";
        public static String MYSQL_PASSWORD { get; private set; } = "!2#1@3PacChat008";
        public static String MYSQL_DATABASE { get; private set; } = "pacchat";
        
        public ServerSettings()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            SERVER_HOST = IPAddress.Parse(ConfigurationManager.AppSettings["ServerAddress"]);
            SERVER_PORT = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);

            WEBSERVER_HOST = ConfigurationManager.AppSettings["WebAddress"];
            WEBSERVER_PORT = Convert.ToInt32(ConfigurationManager.AppSettings["WebPort"]);
        }
    }
}
