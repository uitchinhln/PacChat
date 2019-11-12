using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.Register
{
    public class RegisterData : IPacket
    {
        public string Email { get; set; }
        public string PassHashed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public Gender Gender { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, Email);
            ByteBufUtils.WriteUTF8(byteBuf, PassHashed);
            ByteBufUtils.WriteUTF8(byteBuf, FirstName);
            ByteBufUtils.WriteUTF8(byteBuf, LastName);
            byteBuf.WriteLong(DoB.Ticks);
            byteBuf.WriteInt((int) Gender);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
