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

namespace PacChatServer.Network.Packets
{
    public class ReconnectResquest : IPacket
    {
        public String UserID { get; set; }
        public String Hash { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Hash = ByteBufUtils.ReadUTF8(buffer);
            UserID = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            ReconnectResponse respone = new ReconnectResponse();
            respone.Token = "~";

            Guid id = Guid.Parse(UserID);

            if (id == Guid.Empty)
            {
                respone.StatusCode = ResponeCode.NotFound;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            ChatUserProfile profile = ProfileCache.Instance.GetUserProfile(id);
            Hash = HashUtils.MD5(Hash + id);

            if (profile.PassHashed != Hash)
            {
                respone.StatusCode = ResponeCode.Unauthorized;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            if (profile.Banned)
            {
                respone.StatusCode = ResponeCode.Forbidden;
                chatSession.Send(respone);
                chatSession.Disconnect();
                return;
            }

            respone.StatusCode = ResponeCode.OK;
            respone.Token = chatSession.SessionID.ToString();
            chatSession.Send(respone);
            chatSession.FinalizeLogin(profile);
        }
    }
}
