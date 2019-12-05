using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class ShortProfileResponse : IPacket
    {
        public String ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String LastMess { get; set; } = "";
        public String ConversationID { get; set; }
        public int PreviewCode { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLogout { get; set; }
        public int RelationshipType { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ID);
            ByteBufUtils.WriteUTF8(byteBuf, FirstName);
            ByteBufUtils.WriteUTF8(byteBuf, LastName);
            ByteBufUtils.WriteUTF8(byteBuf, LastMess);
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            byteBuf.WriteInt(PreviewCode);
            byteBuf.WriteBoolean(IsOnline);
            byteBuf.WriteLong(LastLogout.Ticks);
            byteBuf.WriteInt(RelationshipType);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
