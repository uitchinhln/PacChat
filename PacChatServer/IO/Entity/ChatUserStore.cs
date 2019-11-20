using MongoDB.Driver;
using PacChatServer.Entity;
using PacChatServer.IO.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Entity
{
    public class ChatUserStore : Store
    {
        ChatUser value; 

        public ChatUser Load(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save(ChatUser value)
        {
            this.value = value;
            bool result = true;
            try
            {
                Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, Save);
            } catch (MongoWriteException e)
            {
                PacChatServer.GetServer().Logger.Error(e);
                result = false;
            }
            return result;
        }

        private void Save(IMongoCollection<ChatUser> collection)
        {
            collection.InsertOne(value);
        }

        public ChatUser Query(IMongoCollection<ChatUser> collection)
        {
            throw new NotImplementedException();
        }
    }
}
