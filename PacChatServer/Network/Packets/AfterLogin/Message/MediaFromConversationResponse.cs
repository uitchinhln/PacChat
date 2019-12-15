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
    public class MediaFromConversationResponse : IPacket
    {
        public List<string> FileIDs { get; set; } = new List<string>();
        public List<string> FileNames { get; set; } = new List<string>();
        public List<int> Positions { get; set; } = new List<int>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            for (int i = 0; i < FileIDs.Count; ++i)
            {
                ByteBufUtils.WriteUTF8(byteBuf, FileIDs[i]);
                ByteBufUtils.WriteUTF8(byteBuf, FileNames[i]);
                byteBuf.WriteInt(Positions[i]);
            }

            ByteBufUtils.WriteUTF8(byteBuf, "~");

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
