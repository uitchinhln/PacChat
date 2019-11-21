using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MVC
{
    public class AppManager
    {
        public static bool isLostConnection { get; private set; }

        public static List<App> apps { get; private set; } = new List<App>();

        /// <summary>
        /// Find application 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static App GetAppOfType<T>() where T : App
        {
            if (isLostConnection)
                throw new Exception("Lost connection to server.");

            foreach (App app in apps)
            {
                if (app is T)
                    return app;
            }

            return null;
        }

        public static void Register(App app)
        {
            apps.Add(app);
        }

        public static void Unregister(App app)
        {
            apps.Remove(app);
        }

        public static void Clear()
        {
            apps.Clear();
        }


        // This will be called when connection to server is lost
        public static void OnDisconnection(bool lostConnection = false)
        {
            isLostConnection = lostConnection;
        }
    }
}
