using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class ConversationFrIDResponse : IPacket
    {
        public int StatusCode { get; set; }
        public long LastActive { get; set; }
        public List<string> Members { get; set; } = new List<string>();
        public List<string> MessagesID { get; set; } = new List<string>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            byteBuf.WriteLong(LastActive);

            foreach (var member in Members)
                ByteBufUtils.WriteUTF8(byteBuf, member);
            ByteBufUtils.WriteUTF8(byteBuf, "~");

            foreach (var msg in MessagesID)
                ByteBufUtils.WriteUTF8(byteBuf, msg);
            ByteBufUtils.WriteUTF8(byteBuf, "~");

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
