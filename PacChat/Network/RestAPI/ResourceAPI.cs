using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.RestAPI
{
    public static class ResourceAPI
    {
        private static readonly String currentVersion = ConfigurationManager.AppSettings["AppVersion"];

        public static readonly String ResourceDownloadUrl = "http://{0}:{1}/api/resource/v{2}/{3}";

        public delegate void ErrorHandler(Exception error);

        public static void DownloadResource(String savePath,
            DownloadProgressChangedEventHandler onProgressChange, AsyncCompletedEventHandler onDownloadComplete, 
            ErrorHandler errorHandler)
        {
            try
            {
                String fileNameParam = HashUtils.Base64Encode(savePath.Replace(TempUtil.ResourcePath, ""));

                String address = ChatConnection.Instance.WebHost;
                int port = ChatConnection.Instance.WebPort;
                Uri uri = new Uri(String.Format(ResourceDownloadUrl, address, port, currentVersion, fileNameParam));

                WebClient webClient = RestUtils.CreateWebClient();

                Directory.CreateDirectory(savePath.Replace(Path.GetFileName(savePath), ""));

                webClient.DownloadFileAsync(uri, savePath);
                if (onProgressChange != null)
                    webClient.DownloadProgressChanged += onProgressChange;
                if (onDownloadComplete != null)
                    webClient.DownloadFileCompleted += onDownloadComplete;
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
    }
}
