using Newtonsoft.Json;
using PacChat.Cache.Core;
using PacChat.MessageCore.Sticker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

        private static LFUCache<String, BitmapImage> cachedImage = new LFUCache<string, BitmapImage>(100, 10);

        public delegate void ResultHandler(BitmapImage result);

        public static List<StickerCategory> GetCategories()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                String r = client.DownloadString(String.Format(StickerCatesListUrl, ChatConnection.Instance.WebHost));
                return JsonConvert.DeserializeObject<List<StickerCategory>>(r);
            }
        }

        public static List<Sticker> GetStickerCategory(int id) 
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                String r = client.DownloadString(String.Format(StickerCatesUrl, ChatConnection.Instance.WebHost, id));
                return JsonConvert.DeserializeObject<List<Sticker>>(r);
            }
        }

        public static void DownloadImage(String url, ResultHandler resultHandler, [CallerMemberName] string caller = null)
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

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(data);
                        bitmap.EndInit();

                        if (resultHandler != null)
                        {
                            bitmap.Freeze();
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                cachedImage.AddReplace(urll, bitmap);
                                resultHandler(bitmap);
                            });
                        }
                    }
                }).Start();
            } catch (Exception e)
            {
            }
        }

        public static void DownloadImageSynchronously(String url, ResultHandler resultHandler, [CallerMemberName] string caller = null)
        {
            String urll = url.ToLower().Trim();
            try
            {
                if (cachedImage.Contains(urll))
                {
                    resultHandler(cachedImage.Get(urll));
                    return;
                }
                using (WebClient client = new WebClient())
                {
                    byte[] data = client.DownloadData(url);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(data);
                    bitmap.EndInit();

                    if (resultHandler != null)
                    {
                        cachedImage.AddReplace(urll, bitmap);
                        resultHandler(bitmap);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
