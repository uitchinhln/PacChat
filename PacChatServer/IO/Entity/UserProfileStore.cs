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
        public ChatUserProfile LoadByEmail(string email)
        {
            return Mongo.Instance.Get<ChatUserProfile>(Mongo.UserCollectionName, collection => {
                var condition = Builders<ChatUserProfile>.Filter.Eq(p => p.Email, email.ToLower());
                var fields = Builders<ChatUserProfile>.Projection
                    .Include(p => p.ID)
                    .Include(p => p.Email)
                    .Include(p => p.PassHashed)
                    .Include(p => p.FirstName)
                    .Include(p => p.LastName)
                    .Include(p => p.DoB)
                    .Include(p => p.Gender)
                    .Include(p => p.LastLogoff)
                    .Include(p => p.Banned);

                var result = collection.Find(condition).Project<ChatUserProfile>(fields).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        }

        public ChatUserProfile LoadById(string id)
        {
            return LoadById(Guid.Parse(id));
        }

        public ChatUserProfile LoadById(Guid id)
        {
            return Mongo.Instance.Get<ChatUserProfile>(Mongo.UserCollectionName, collection => {
                var condition = Builders<ChatUserProfile>.Filter.Eq(p => p.ID, id);
                var fields = Builders<ChatUserProfile>.Projection
                    .Include(p => p.ID)
                    .Include(p => p.Email)
                    .Include(p => p.PassHashed)
                    .Include(p => p.FirstName)
                    .Include(p => p.LastName)
                    .Include(p => p.DoB)
                    .Include(p => p.Gender)
                    .Include(p => p.LastLogoff)
                    .Include(p => p.Banned);

                var result = collection.Find(condition).Project<ChatUserProfile>(fields).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        }
    }
}
