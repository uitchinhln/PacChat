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

        public bool UpdateRelations(ChatUser user)
        {
            bool result = false;
            try
            {
                Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, (collection) => {
                    var condition = Builders<ChatUser>.Filter.Eq(p => p.ID, user.ID);
                    var update = Builders<ChatUser>.Update.Set(p => p.Relationship, user.Relationship);
                    collection.UpdateOneAsync(condition, update, new UpdateOptions() { IsUpsert = true });
                });
                result = true;
            }
            catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
            return result;
        }

        public bool Update(ChatUser user)
        {
            bool result = false;
            try
            {
                Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, (collection) => {
                    var condition = Builders<ChatUser>.Filter.Eq(p => p.ID, user.ID);
                    collection.ReplaceOneAsync(condition, user, new UpdateOptions() { IsUpsert = true });
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
