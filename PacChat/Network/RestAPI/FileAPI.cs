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
    public class FileAPI
    {
        public delegate void ResultHandler(Dictionary<String, String> result);
        public delegate void ErrorHandler(Exception error);

        private static readonly String AttachmentUploadUrl = "http://{0}:1403/api/message/attachment/{1}";
        private static readonly String AttachmentDownloadUrl = "http://{0}:1403/api/message/attachment/{1}/{2}";

        public static async void UploadAttachment(String conversationID, List<String> filePaths, 
            ResultHandler handler, ErrorHandler errorHandler)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);

                MultipartFormDataContent form = new MultipartFormDataContent();
                foreach (String filePath in filePaths)
                {
                    if (!File.Exists(filePath)) continue;
                    FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    form.Add(new StreamContent(stream), "file", Path.GetFileName(filePath));
                }

                String address = ChatConnection.Instance.Host;
                String url = String.Format(AttachmentUploadUrl, address, conversationID);

                Console.WriteLine(url);

                HttpResponseMessage response = await httpClient.PostAsync(url, form);
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();

                string sd = response.Content.ReadAsStringAsync().Result;

                //Dic<FileName, FileID>
                Dictionary<String, String> result = JsonConvert.DeserializeObject<Dictionary<String, String>>(sd);
                handler(result);
            } catch (Exception e)
            {
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadAttachment(String conversationID, String fileID, String savePath, 
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler)
        {
            try
            {
                String address = ChatConnection.Instance.Host;
                Uri uri = new Uri(String.Format(AttachmentDownloadUrl, address, fileID, conversationID));

                WebClient webClient = new WebClient();

                webClient.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);
                webClient.DownloadFileAsync(uri, savePath);
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;
                if (onDownloadComplete != null)
                    webClient.DownloadFileCompleted += onDownloadComplete;           
            } catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadMedia(String conversationID, String fileID, String savePath,
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler)
        {
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);
            DownloadMedia(streamUrl, savePath, onProgressChange, onDownloadComplete, errorHandler);
        }

        public static void DownloadMedia(String streamURL, String savePath,
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler)
        {
            try
            {
                String address = ChatConnection.Instance.Host;
                String token = ChatConnection.Instance.Session.SessionID;
                Uri uri = new Uri(streamURL);

                WebClient webClient = new WebClient();

                webClient.DownloadFileAsync(uri, savePath);
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
