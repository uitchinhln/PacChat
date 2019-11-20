using MongoDB.Driver;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.IO.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Entity
{
    public class UserProfileStore : Store
    {
        string email;
        Guid id;

        public ChatUserProfile LoadByEmail(string email)
        {
            this.email = email.ToLower();
            return Mongo.Instance.Get<ChatUserProfile>(Mongo.UserCollectionName, QueryByEmail);
        }

        public ChatUserProfile LoadById(string id)
        {
            return LoadById(Guid.Parse(id));
        }

        public ChatUserProfile LoadById(Guid id)
        {
            this.id = id;
            return Mongo.Instance.Get<ChatUserProfile>(Mongo.UserCollectionName, QueryById);
        }

        private ChatUserProfile QueryByEmail(IMongoCollection<ChatUserProfile> collection)
        {
            var condition = Builders<ChatUserProfile>.Filter.Eq(p => p.Email, email);
            var result = collection.Find(condition).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        private ChatUserProfile QueryById(IMongoCollection<ChatUserProfile> collection)
        {
            var condition = Builders<ChatUserProfile>.Filter.Eq(p => p.ID, id);
            var result = collection.Find(condition).ToList();
            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }
    }
}
