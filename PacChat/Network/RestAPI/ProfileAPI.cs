﻿using Newtonsoft.Json;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PacChat.Network.RestAPI
{
    public static class ProfileAPI
    {
        public delegate void ResultHandler();

        public delegate void GetAvatarResult(ImageSource source);
        public delegate void ErrorHandler(Exception error);

        private static readonly String UploadAvatarURL = "http://{0}:1403/api/profile/avatar";
        private static readonly String GetSelfAvatarURL = "http://{0}:1403/api/profile/avatar";
        private static readonly String GetUserAvatarURL = "http://{0}:1403/api/profile/avatar/{1}";

        public static async void AvatarUpload(String filePath, ResultHandler handler, ErrorHandler errorHandler)
        {
            try
            {
                if (!File.Exists(filePath)) return;

                Image image = Image.FromFile(filePath);
                int w = image.Width;
                int h = image.Height;

                if (w > h)
                {
                    if (h > 500)
                    {
                        w = 500 * w / h;
                        h = 500;
                    }
                } else
                {
                    if (w > 500)
                    {
                        h = 500 * h / w;
                        w = 500;
                    }
                }

                Image thumbnail = image.GetThumbnailImage(w, h, null, IntPtr.Zero);

                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);

                MultipartFormDataContent form = new MultipartFormDataContent();

                if (!File.Exists(filePath))
                    throw new EntryPointNotFoundException();
                Stream stream = new MemoryStream();
                thumbnail.Save(stream, ImageFormat.Png);
                form.Add(new StreamContent(stream));

                String address = ChatConnection.Instance.WebHost;
                String url = String.Format(UploadAvatarURL, address);

                HttpResponseMessage response = await httpClient.PostAsync(url, form);
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                handler();
            }
            catch (Exception e)
            {
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadSelfAvatar(GetAvatarResult resultHandler, ErrorHandler errorHandler)
        {
            try
            {
                String address = ChatConnection.Instance.WebHost;
                String url = String.Format(GetSelfAvatarURL, address);

                new Task(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
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
                                resultHandler(bitmap);
                            });
                        }
                    }
                }).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadUserAvatar(String userID, GetAvatarResult resultHandler, ErrorHandler errorHandler)
        {
            try
            {
                String address = ChatConnection.Instance.WebHost;
                String url = String.Format(GetUserAvatarURL, address, userID);

                new Task(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
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
                                resultHandler(bitmap);
                            });
                        }
                    }
                }).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }
    }
}
