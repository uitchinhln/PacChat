using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entity.EntityProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class FriendsListRequest : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
            
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            FriendsListResponse response = new FriendsListResponse();

            foreach(KeyValuePair<Guid, Guid> pair in chatSession.Owner.Relationship)
            {
                if (Relation.Get(pair.Value).RelationType == Relation.Type.Friend)
                {
                    response.Friends.Add(pair.Key.ToString());
                }
            }

            chatSession.Send(response);
        }
    }
}
