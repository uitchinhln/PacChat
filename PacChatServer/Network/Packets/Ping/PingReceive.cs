using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.Ping
{
    public class PingReceive : IPacket
    {
        public string Version { get; set; }


        public void Decode(IByteBuffer buffer)
        {
            this.Version = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            PingRespone respone = new PingRespone();
            respone.CurrentVersion = "1.0.0";
            if (Version != "1.0.0")
            {
                respone.Runnable = false;
                respone.ForceUpdate = true;
            } else
            {
                respone.Runnable = true;
                respone.ForceUpdate = false;
            }
            session.Send(respone);
            session.Disconnect();
        }
    }
}
