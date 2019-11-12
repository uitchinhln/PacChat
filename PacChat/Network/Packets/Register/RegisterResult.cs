using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.Register
{
    public class RegisterResult : IPacket
    {
        public int StatusCode { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            StatusCode = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine(StatusCode);
        }
    }
}
