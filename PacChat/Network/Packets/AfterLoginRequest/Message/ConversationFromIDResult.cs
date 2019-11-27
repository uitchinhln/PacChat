using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore;
using CNetwork.Utils;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class ConversationFromIDResult : IPacket
    {
        public long LastActive { get; set; }
        public HashSet<string> Members { get; set; } = new HashSet<string>();
        public List<string> MessagesID { get; set; } = new List<string>();

        public void Decode(IByteBuffer buffer)
        {
            LastActive = buffer.ReadLong();

            // Get the number of members in this conversation
            string temp = ByteBufUtils.ReadUTF8(buffer);
            while (temp != "~")
            {
                Members.Add(temp);
                temp = ByteBufUtils.ReadUTF8(buffer);
            }

            // Get the most recent messages (ID) of this conversation
            temp = ByteBufUtils.ReadUTF8(buffer);
            while (temp != "~")
            {
                MessagesID.Add(temp);
                temp = ByteBufUtils.ReadUTF8(buffer);
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // Load message to UI
        }
    }
}
