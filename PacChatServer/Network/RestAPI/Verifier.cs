using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestAPI
{
    public static class Verifier
    {
        public static string HeaderToken { get; private set; } = "ChatVerifier";

        public static bool VerifyRequestToken(HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.TryGetValues(HeaderToken, out var values);

            if (values == null)
            {
                return false;
            }

            String token = values.FirstOrDefault();

            if (token == null)
            {
                return false;
            }

            return VerifyRequestToken(token);
        }

        public static bool VerifyRequestToken(String token)
        {
            try
            {
                Guid id = Guid.Parse(token);
                ChatSession session;
                if ((session = PacChatServer.GetServer().SessionRegistry.Get(id)) != null && session.IsActive()) {
                    return true;
                }
            } catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public static ChatSession SessionFromToken(HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.TryGetValues(HeaderToken, out var values);

            if (values == null)
            {
                return null;
            }

            String token = values.FirstOrDefault();

            if (token == null)
            {
                return null;
            }
            
            return SessionFromToken(token);
        } 

        public static ChatSession SessionFromToken(String token)
        {
            try
            {
                Guid id = Guid.Parse(token);
                ChatSession session;
                if ((session = PacChatServer.GetServer().SessionRegistry.Get(id)) != null && session.IsActive())
                {
                    return session;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        } 
    }
}
