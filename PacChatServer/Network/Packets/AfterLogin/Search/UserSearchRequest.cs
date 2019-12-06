using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.IO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Search
{
    public class UserSearchRequest : IPacket
    {
        public String Email { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Email = ByteBufUtils.ReadUTF8(buffer).ToLower();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            UserSearchResponse response = new UserSearchResponse();
            response.UserIDs = new ChatUserStore().SearchUserIDByEmail(Email);
            session.Send(response);
        }
    }
}
