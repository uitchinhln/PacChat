using PacChatServer.IO.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestAPI.Handler
{
    public static class FileHandler
    {
        public static Dictionary<String, String> Upload(HttpRequestMessage Request, String SavePath)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();

            String fileName;
            Guid id;

            Directory.CreateDirectory(SavePath);
            var streamProvider = new MultipartFormDataStreamProvider(SavePath);

            Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(p =>
            {
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        continue;
                    }

                    if (new FileInfo(fileData.LocalFileName).Length > ServerSettings.MAX_SIZE_UPLOAD)
                    {
                        File.Delete(fileData.LocalFileName);
                        continue;
                    }

                    fileName = fileData.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    id = Guid.NewGuid();

                    AttachmentStore.Map(id, fileName);

                    result.Add(fileName, id.ToString());

                    File.Move(fileData.LocalFileName, Path.Combine(SavePath, id.ToString()));
                }
            }).Wait();

            return result;
        }
    }
}
