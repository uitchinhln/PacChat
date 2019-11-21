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
        public ChatUser Load(Guid id)
        {
            return Mongo.Instance.Get<ChatUser>(Mongo.UserCollectionName, (collection) => {
                var condition = Builders<ChatUser>.Filter.Eq(p => p.ID, id);
                var result = collection.Find(condition).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        }

        public bool AddNew(ChatUser value)
        {
            bool result = false;
            try
            {
                Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, (collection) =>
                {
                    collection.InsertOne(value);
                });
                result = true;
            } catch (MongoWriteException e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
            return result;
        }

        public bool Update(ChatUser value)
        {
            bool result = false;
            try
            {
                Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, (collection) => {
                    var condition = Builders<ChatUser>.Filter.Eq(p => p.ID, value.ID);
                    collection.ReplaceOneAsync(condition, value, new UpdateOptions() { IsUpsert = true });
                });
                result = true;
            } catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
            return result;
        }
    }
}
