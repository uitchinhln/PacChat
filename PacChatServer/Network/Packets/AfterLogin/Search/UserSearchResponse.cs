using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Search
{
    public class UserSearchResponse : IPacket
    {
        public List<SearchResult> Results = new List<SearchResult>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            foreach (var item in Results)
            {
                ByteBufUtils.WriteUTF8(byteBuf, item.ID);
                ByteBufUtils.WriteUTF8(byteBuf, item.FirstName);
                ByteBufUtils.WriteUTF8(byteBuf, item.LastName);
                ByteBufUtils.WriteUTF8(byteBuf, item.LastMessage);
                ByteBufUtils.WriteUTF8(byteBuf, item.ConversationID);
                byteBuf.WriteInt(item.PreviewCode);
                byteBuf.WriteBoolean(item.IsOnline);
                byteBuf.WriteLong(item.LastLogout.Ticks);
                byteBuf.WriteInt(item.Relationship);
            }

            ByteBufUtils.WriteUTF8(byteBuf, "~");

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }

    public class SearchResult {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastMessage { get; set; } = "";
        public string ConversationID { get; set; }
        public int PreviewCode { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLogout { get; set; }
        public int Relationship { get; set; }
    }
}
