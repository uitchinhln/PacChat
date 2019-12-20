using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity.EntityProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class SetChatInterfaceRequest : IPacket
    {
        public ChatDecorate Decore { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Decore = new ChatDecorate();
            Decore.BackgroundId = ByteBufUtils.ReadUTF8(buffer);
            Decore.BackgroundBlur = buffer.ReadInt();

            Decore.BackgroundColor = buffer.ReadInt();
            Decore.Use = (BackgroundType)buffer.ReadInt();

            Decore.IconColor = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            

        }
    }
}
