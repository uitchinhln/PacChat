using PacChatServer.IO.Message;
using PacChatServer.Network.RestAPI.Handler;
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
    public class MediaController : ApiController
    {
        public static String SavePath { get; } = "Uploaded/Media/{0}";

        [HttpPost, Route("api/message/media/{conversationID}")]
        public Dictionary<String, String> ConversationMediaUpload(string conversationID)
        {
            ChatSession session = Verifier.SessionFromToken(Request);
            if (session == null)
                throw new UnauthorizedAccessException();

            Guid conversationGid = Guid.Parse(conversationID);
            if (!session.Owner.ConversationID.Contains(conversationGid))
                throw new UnauthorizedAccessException();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            Dictionary<String, String> result = FileHandler.Upload(Request, String.Format(SavePath, conversationID), true);

            return result;
        }

        [HttpGet, Route("api/message/media/{fileID}/{conversationID}/{token}")]
        public HttpResponseMessage GetConversationMedia(string conversationID, string fileID, string token)
        {
            ChatSession session = Verifier.SessionFromToken(token);
            if (session == null)
                throw new UnauthorizedAccessException();

            Guid conversationGid = Guid.Parse(conversationID);
            if (!session.Owner.ConversationID.Contains(conversationGid))
                throw new UnauthorizedAccessException();

            if (!File.Exists(Path.Combine(String.Format(SavePath, conversationID), fileID)))
                throw new FileNotFoundException();

            String filePath = Path.Combine(String.Format(SavePath, conversationID), fileID);
            String fileName = AttachmentStore.Parse(Guid.Parse(fileID));

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
                return partialResponse;
            }
            else
            {
                HttpResponseMessage fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                fullResponse.Content = new StreamContent(stream);
                fullResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                return fullResponse;
            }
        }
    }
}
