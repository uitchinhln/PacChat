using Newtonsoft.Json;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.RestAPI
{
    public class FileAPI
    {
        public delegate void ResultHandler(Dictionary<String, String> result);
        public delegate void ErrorHandler(Exception error);

        private static readonly String AttachmentUploadUrl = "http://{0}:{1}/api/message/attachment/{2}";
        private static readonly String AttachmentDownloadUrl = "http://{0}:{1}/api/message/attachment/{2}/{3}";

        private static readonly String MediaUploadUrl = "http://{0}:{1}/api/message/media/{2}";

        private static readonly Random Rand = new Random();

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

                String address = ChatConnection.Instance.WebHost;
                int port = ChatConnection.Instance.WebPort;
                String url = String.Format(AttachmentUploadUrl, address, port, conversationID);

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
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler, [CallerMemberName] string caller = null)
        {
            try
            {
                Console.WriteLine(caller + " downloading attachment...");
                String address = ChatConnection.Instance.WebHost;
                int port = ChatConnection.Instance.WebPort;
                Uri uri = new Uri(String.Format(AttachmentDownloadUrl, address, port, fileID, conversationID));

                WebClient webClient = RestUtils.CreateWebClient();

                webClient.Headers.Add(ClientSession.HeaderToken, ChatConnection.Instance.Session.SessionID);

                Directory.CreateDirectory(TempUtil.DownloadPath);
                String temp = Path.Combine(TempUtil.DownloadPath, Guid.NewGuid().ToString());

                webClient.DownloadFileAsync(uri, temp);
                webClient.DownloadFileCompleted += (o, e) =>
                {
                    File.Move(temp, RepairSavePath(savePath));
                    if (onDownloadComplete != null)
                        onDownloadComplete(o, e);
                };
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;

                webClient.DownloadFileCompleted += (o, e) =>
                {
                    RestUtils.Remove(webClient);
                };
            } catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static async void UploadMedia(String conversationID, List<String> filePaths,
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

                String address = ChatConnection.Instance.WebHost;
                int port = ChatConnection.Instance.WebPort;
                String url = String.Format(MediaUploadUrl, address, port, conversationID);

                HttpResponseMessage response = await httpClient.PostAsync(url, form);
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();

                string sd = response.Content.ReadAsStringAsync().Result;

                //Dic<FileName, FileID>
                Dictionary<String, String> result = JsonConvert.DeserializeObject<Dictionary<String, String>>(sd);
                handler(result);
            }
            catch (Exception e)
            {
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static void DownloadMedia(String conversationID, String fileID, String savePath,
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler, [CallerMemberName] string caller = null)
        {
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);
            DownloadMedia(streamUrl, savePath, onProgressChange, onDownloadComplete, errorHandler, caller);
        }

        public static void DownloadMedia(String streamURL, String savePath,
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, ErrorHandler errorHandler, [CallerMemberName] string caller = null)
        {
            try
            {
                Console.WriteLine(caller + " downloading media...");
                Uri uri = new Uri(streamURL);

                WebClient webClient = RestUtils.CreateWebClient();

                Directory.CreateDirectory(TempUtil.DownloadPath);
                String temp = Path.Combine(TempUtil.DownloadPath, Guid.NewGuid().ToString());

                webClient.DownloadFileAsync(uri, temp);
                webClient.DownloadFileCompleted += (o, e) =>
                {                    
                    File.Move(temp, RepairSavePath(savePath));
                    if (onDownloadComplete != null)
                        onDownloadComplete(o, e);
                };
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;

                webClient.DownloadFileCompleted += (o, e) =>
                {
                    RestUtils.Remove(webClient);
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (errorHandler != null) errorHandler(e);
            }
        }

        public static String RepairSavePath(String savePath)
        {
            string fileNameOnly = Path.GetFileNameWithoutExtension(savePath);
            string extension = Path.GetExtension(savePath);
            string path = Path.GetDirectoryName(savePath);
            string newFullPath = savePath;
            int count = 1;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0} ({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }
    }
}
