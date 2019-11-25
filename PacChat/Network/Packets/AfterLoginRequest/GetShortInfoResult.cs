using CNetwork;
using CNetwork.Utils;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.ChatAMVC;
using PacChat.ChatPageContents.ViewModels;
using PacChat.MVC;
using PacChat.Windows.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest
{
    public class GetShortInfoResult : IPacket
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastMessage { get; set; }
        public int IsOnline { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            LastMessage = ByteBufUtils.ReadUTF8(buffer);
            IsOnline = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            
        }
    }
}
