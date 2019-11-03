using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.Ping
{
    public class PingRespone : IPacket
    {
        public bool ForceUpdate { get; set; }
        public bool Runnable { get; set; }
        public string CurrentVersion { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            this.Runnable = buffer.ReadBoolean();
            this.ForceUpdate = buffer.ReadBoolean();
            this.CurrentVersion = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine(Runnable);
            Console.WriteLine(ForceUpdate);
            Console.WriteLine(CurrentVersion);
        }
    }
}
