using MongoDB.Bson.Serialization.Attributes;
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

        public bool Save(ChatUser user)
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

        public List<String> SearchUserIDByEmail(string input)
        {
            List<String> result = new List<String>();

            Mongo.Instance.Get<SearchIdByEmail>(Mongo.UserCollectionName, collection =>
            {
                var condition = Builders<SearchIdByEmail>.Filter.Regex(p => p.Email, input); 

                var fields = Builders<SearchIdByEmail>.Projection
                     .Include(p => p.ID)
                     .Include(p => p.Email);

                var objs = collection.Find(condition).Project<SearchIdByEmail>(fields).ToList();

                foreach (SearchIdByEmail r in objs)
                {
                    result.Add(r.ID.ToString());
                }
                return null;
            });

            return result;
        }
    }

    public class SearchIdByEmail
    {
        [BsonId]
        public Guid ID { get; set; }
        [BsonElement("Email")]
        public String Email { get; set; }
    }
}
