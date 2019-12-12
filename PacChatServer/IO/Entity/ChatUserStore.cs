using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PacChatServer.Entity;
using PacChatServer.Entity.EntityProperty;
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
                var result = collection.Find(condition).Limit(1).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        }

        public bool UpdateRelations(ChatUser user)
        {
            lock (user)
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
        }

        public bool Save(ChatUser user)
        {
            lock (user)
            {
                bool result = false;
                try
                {
                    Mongo.Instance.Set<ChatUser>(Mongo.UserCollectionName, (collection) => {
                        var condition = Builders<ChatUser>.Filter.Eq(p => p.ID, user.ID);
                        collection.ReplaceOne(condition, user, new UpdateOptions() { IsUpsert = true });
                    });
                    result = true;
                }
                catch (Exception e)
                {
                    PacChatServer.GetServer().Logger.Error(e);
                }
                return result;
            }
        }

        public List<String> SearchUserIDByEmail(string input, ChatUser user)
        {
            List<String> result = new List<String>();

            Mongo.Instance.Get<SearchIdByEmail>(Mongo.UserCollectionName, collection =>
            {
                var matchEmail = Builders<SearchIdByEmail>.Filter.Regex(p => p.Email, input);
                var ignoreBlocked = Builders<SearchIdByEmail>.Filter.Where(p => !RelationIgnore(user, p.ID, Relation.Type.Block));

                var fields = Builders<SearchIdByEmail>.Projection
                     .Include(p => p.ID)
                     .Include(p => p.Email);

                var objs = collection.Find(matchEmail & ignoreBlocked)
                    .Project<SearchIdByEmail>(fields).Limit(31).ToList();

                foreach (SearchIdByEmail r in objs)
                {
                    if (r.ID == user.ID) continue;
                    result.Add(r.ID.ToString());
                }
                return null;
            });

            return result;
        }

        private bool RelationIgnore(ChatUser user, Guid targetID, Relation.Type relationType)
        {
            if (user == null || targetID == null) return false;

            if (user.Relationship.ContainsKey(targetID))
            {
                Relation relation = Relation.Get(user.Relationship[targetID], true);
                if (relation == null) return true;
                return relation.RelationType != relationType || relation.Source == user.ID;
            } else
            {
                return true;
            }
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
