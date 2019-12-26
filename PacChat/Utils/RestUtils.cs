using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Utils
{
    public static class RestUtils
    {
        private static List<WebClient> webClients = new List<WebClient>();

        public static WebClient CreateWebClient()
        {
            WebClient client = new WebClient();
            webClients.Add(client);
            return client;
        }

        public static void CancelAllTask()
        {
            foreach (var client in webClients)
            {
                if (client == null) continue;
                client.CancelAsync();
                client.Dispose();
            }
        }

        public static void Remove(WebClient client)
        {
            webClients.Remove(client);
            client.CancelAsync();
            client.Dispose();
        }
    }
}
