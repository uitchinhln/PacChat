using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class ModifyPasswordRequest : IPacket
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            OldPassword = ByteBufUtils.ReadUTF8(buffer);
            NewPassword = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            PacChatServer.GetServer().Logger.Debug(chatSession.Owner.Password);
            PacChatServer.GetServer().Logger.Debug(HashUtils.MD5(OldPassword + chatSession.Owner.ID)); 
            PacChatServer.GetServer().Logger.Debug(HashUtils.MD5(OldPassword)); 

            bool result = false;
            if (HashUtils.MD5(OldPassword + chatSession.Owner.ID).Equals(chatSession.Owner.Password))
            {
                chatSession.Owner.Password = HashUtils.MD5(NewPassword + chatSession.Owner.ID);
                chatSession.Owner.Save();
                result = true;
            }

            ModifyPasswordResponse packet = new ModifyPasswordResponse();
            packet.Result = result;
            chatSession.Send(packet);
        }
    }
}
