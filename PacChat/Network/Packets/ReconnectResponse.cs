using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets
{
    public class ReconnectResponse : IPacket
    {
        public int StatusCode { get; set; }
        public string Token { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            StatusCode = buffer.ReadInt();
            Token = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            if (StatusCode == 200)
            {
                (session as ClientSession).LoggedIn(Token);
                AppManager.OnReconnected();
            }
            ChatConnection.Instance.OnResponse(StatusCode);
        }
    }
}
