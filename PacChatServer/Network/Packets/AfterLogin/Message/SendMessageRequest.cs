using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class SendMessageRequest : IPacket
    {
        public string ReceiverID { get; set; }
        public TextMessage Message { get; set; } = new TextMessage();

        public void Decode(IByteBuffer buffer)
        {
            ReceiverID = ByteBufUtils.ReadUTF8(buffer);
            Message.Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            ChatUser user;

            if (ChatUserManager.OnlineUsers.TryGetValue(Guid.Parse(ReceiverID), out user))
            {
                SendMessageResponse packet = new SendMessageResponse();
                packet.Message = Message;
                packet.SenderID = chatSession.Owner.ID.ToString();
                user.Send(packet);
                // Console.WriteLine("Sent to " + ReceiverID);
            }
        }
    }
}
