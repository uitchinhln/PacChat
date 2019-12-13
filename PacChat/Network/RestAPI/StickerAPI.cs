using Newtonsoft.Json;
using PacChat.Cache.Core;
using PacChat.MessageCore.Sticker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PacChat.Network.RestAPI
{
    public class StickerAPI
    {
        private static readonly String StickerCatesListUrl = "http://{0}:1403/api/message/sticker/category/list";
        private static readonly String StickerCatesUrl = "http://{0}:1403/api/message/sticker/category/stickers/{1}";

        private static LRUCache<String, BitmapImage> cachedImage = new LRUCache<string, BitmapImage>(100, 10);

        public delegate void ResultHandler(BitmapImage result);

        public static List<StickerCategory> GetCategories()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                String r = client.DownloadString(String.Format(StickerCatesListUrl, ChatConnection.Instance.Host));
                return JsonConvert.DeserializeObject<List<StickerCategory>>(r);
            }
        }

        public static List<Sticker> GetStickerCategory(int id) 
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                String r = client.DownloadString(String.Format(StickerCatesUrl, ChatConnection.Instance.Host, id));
                return JsonConvert.DeserializeObject<List<Sticker>>(r);
            }
        }

        public static void DownloadImage(String url, ResultHandler resultHandler)
        {
            String urll = url.ToLower().Trim();
            try
            {
                if (cachedImage.Contains(urll))
                {
                    resultHandler(cachedImage.Get(urll));
                    return;
                }
                new Task(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        byte[] data = client.DownloadData(url);
                        if (resultHandler != null)
                            Application.Current.Dispatcher.Invoke(() => 
                            {
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.StreamSource = new MemoryStream(data);
                                bitmap.EndInit();
                                cachedImage.AddReplace(urll, bitmap);
                                resultHandler(bitmap);
                            });
                    }
                }).Start();
            } catch (Exception e)
            {
            }
        }
    }
}
