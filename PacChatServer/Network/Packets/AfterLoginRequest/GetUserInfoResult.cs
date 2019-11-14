using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetUserInfoResult : IPacket
    {
        public User user;

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            byteBuf.WriteString(user.Email, Encoding.Unicode);
            byteBuf.WriteString(user.FirstName, Encoding.Unicode);
            byteBuf.WriteString(user.LastName, Encoding.Unicode);
            byteBuf.WriteString(user.PassHashed, Encoding.Unicode);
            byteBuf.WriteInt((int)user.Gender);
            byteBuf.WriteString(user.DoB.ToString(), Encoding.Unicode);
            // Relation for post addition

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
