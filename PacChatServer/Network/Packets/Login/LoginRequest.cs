using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.Login
{
    public class LoginRequest : IPacket
    {
        public string Username { get; set; }
        public string Passhash { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Username = ByteBufUtils.ReadUTF8(buffer);
            Passhash = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            LoginResult respone = new LoginResult();

            Guid id = ProfileCache.Instance.ParseEmailToGuid(Username);

            if (id == Guid.Empty)
            {
                respone.StatusCode = 404;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            ChatUserProfile profile = ProfileCache.Instance.GetUserProfile(id);
            Passhash = HashUtils.MD5(Passhash + id);

            if (profile.PassHashed != Passhash)
            {
                respone.StatusCode = 401;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            if (profile.Banned)
            {
                respone.StatusCode = 403;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            respone.StatusCode = 200;
            chatSession.Send(respone);
            chatSession.FinalizeLogin(profile);
        }
    }
}
