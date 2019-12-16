using NReco.VideoConverter;
using NReco.VideoInfo;
using PacChatServer.IO.Message;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestAPI.Handler
{
    public static class FileHandler
    {
        public static String[] VideoExtensions = { ".mp4", ".avi" };
        public static String[] ImageExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };

        public static Dictionary<String, String> Upload(HttpRequestMessage Request, String SavePath, bool createThumb = false)
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

                    //if (new FileInfo(fileData.LocalFileName).Length > ServerSettings.MAX_SIZE_UPLOAD)
                    //{
                    //    File.Delete(fileData.LocalFileName);
                    //    continue;
                    //}

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
                    if (createThumb)
                    {
                        if (VideoExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase))
                        {
                            var ffProbe = new FFProbe();
                            var videoInfo = ffProbe.GetMediaInfo(Path.Combine(SavePath, id.ToString()));
                            FFMpegConverter converter = new FFMpegConverter();
                            converter.GetVideoThumbnail(Path.Combine(SavePath, id.ToString()), 
                                Path.Combine(SavePath, id.ToString() + "_thumb.jpg"), 
                                (float) videoInfo.Duration.TotalSeconds / 2);
                        }

                        if (ImageExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase))
                        {
                            Image image = Image.FromFile(Path.Combine(SavePath, id.ToString()));

                            int w = image.Width, h = image.Height;
                            if (image.Height > 300)
                            {
                                h = 300;
                                w = image.Width * 300 / image.Height;
                            }
                            else if (image.Width > 500)
                            {
                                w = 500;
                                h = image.Height * 500 / image.Width;
                            }

                            image.GetThumbnailImage(w, h, null, IntPtr.Zero).Save(Path.Combine(SavePath, id.ToString() + "_thumb.jpg"));
                            image.Dispose();
                        }
                    }
                }
            }).Wait();

            return result;
        }
    }
}
