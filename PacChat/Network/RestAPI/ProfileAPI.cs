using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.RestAPI
{
    public static class ProfileAPI
    {
        public delegate void ResultHandler();
        public delegate void ErrorHandler(Exception error);

        private static readonly String UploadAvatarURL = "http://{0}:1403/api/profile/avatar";
        private static readonly String GetSelfAvatarURL = "http://{0}:1403/api/profile/avatar";
        private static readonly String GetUserAvatarURL = "http://{0}:1403/api/profile/avatar/{1}";
        private static readonly String AvatarSavePath = "";

        public static async void AvatarUpload(String filePath, ResultHandler handler, ErrorHandler errorHandler)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);

                MultipartFormDataContent form = new MultipartFormDataContent();

                if (!File.Exists(filePath))
                    throw new EntryPointNotFoundException();
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                form.Add(new StreamContent(stream));

                IPAddress address = ChatConnection.Instance.GetIPAddress();
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

        public static void DownloadSelfAvatar(DownloadProgressChangedEventHandler onProgressChange, 
            AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler)
        {
            try
            {
                IPAddress address = ChatConnection.Instance.GetIPAddress();
                Uri uri = new Uri(String.Format(GetSelfAvatarURL, address));

                WebClient webClient = new WebClient();

                webClient.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                webClient.DownloadFileAsync(uri, Path.Combine(AvatarSavePath, "MyAvatar"));
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;
                if (onDownloadComplete != null)
                    webClient.DownloadFileCompleted += onDownloadComplete;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadUserAvatar(String userID, DownloadProgressChangedEventHandler onProgressChange, 
            AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler)
        {
            try
            {
                IPAddress address = ChatConnection.Instance.GetIPAddress();
                Uri uri = new Uri(String.Format(GetUserAvatarURL, address, userID));

                WebClient webClient = new WebClient();

                webClient.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                webClient.DownloadFileAsync(uri, Path.Combine(AvatarSavePath, userID));
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;
                if (onDownloadComplete != null)
                    webClient.DownloadFileCompleted += onDownloadComplete;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }
    }
}
