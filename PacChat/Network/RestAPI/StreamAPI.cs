using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PacChat.Network.RestAPI
{
    public static class StreamAPI
    {
        private static readonly String MediaStreamUrl = "http://{0}:1403/api/message/media/{1}/{2}/{3}";

        public static String GetMediaURL(String fileID, String conversationID)
        {            
            IPAddress address = ChatConnection.Instance.GetIPAddress();
            String token = ChatConnection.Instance.Session.SessionID;

            return String.Format(MediaStreamUrl, address, fileID, conversationID, token);
        }
    }
}
