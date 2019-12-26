using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.IO.Entity;
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

            Relation rela;
            foreach (KeyValuePair<Guid, Guid> pair in chatSession.Owner.Relationship)
            {
                if ((rela = Relation.Get(pair.Value)) != null && rela.RelationType == Relation.Type.Friend)
                {
                    bool isOnline = ChatUserManager.OnlineUsers.ContainsKey(pair.Key);
                    if (isOnline)
                        response.Friends.Insert(0, pair.Key.ToString());
                    else
                        response.Friends.Add(pair.Key.ToString());
                }
            }

            chatSession.Send(response);
        }
    }
}
