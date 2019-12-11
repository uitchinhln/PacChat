using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.RestAPI
{
    public class StickerAPI
    {
        private static readonly String StickerCatesListUrl = "http://{0}:1403/api/message/sticker/category/list";

        public static string GetCategories(String token)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(ClientSession.HeaderToken, token);
                return client.DownloadString(String.Format(StickerCatesListUrl, ChatConnection.Instance.Host));
            }
        }

        public static void DownloadStickerPack()
        {

        }
    }
}
