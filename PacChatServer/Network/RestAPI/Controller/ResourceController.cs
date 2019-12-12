using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PacChatServer.Network.RestAPI.Controller
{
    public class ResourceController : ApiController
    {
        public static String ResourcePath { get; } = "Resource/v{0}";

        [HttpGet, Route("api/resource/v{version}/{basedFileName}")]
        public HttpResponseMessage GetConversationFile(string version, string basedFileName)
        {
            String fileName = HashUtils.Base64Decode(basedFileName);
            if (!File.Exists(Path.Combine(String.Format(ResourcePath, version), fileName)))
                throw new FileNotFoundException();

            String filePath = Path.Combine(String.Format(ResourcePath, version), fileName);

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            new CustomContentTypeProvider().TryGetContentType(fileName, out var contentType);

            if (contentType == null || contentType.Length < 1)
            {
                contentType = "application/octet-stream";
            }

            if (Request.Headers.Range != null)
            {
                HttpResponseMessage partialResponse = Request.CreateResponse(HttpStatusCode.PartialContent);
                partialResponse.Content = new ByteRangeStreamContent(stream, Request.Headers.Range, contentType);
                partialResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(fileName)
                };
                return partialResponse;
            }
            else
            {
                HttpResponseMessage fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                fullResponse.Content = new StreamContent(stream);
                fullResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                fullResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(fileName)
                };
                return fullResponse;
            }
        }
    }
}
