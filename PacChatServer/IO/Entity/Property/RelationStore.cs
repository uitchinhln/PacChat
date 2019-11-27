using MongoDB.Driver;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.IO.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Entity.Property
{
    public class RelationStore : Store
    {
        
        public Relation Load(Guid id)
        {
            return Mongo.Instance.Get<Relation>(Mongo.RelationCollectionName, collection =>
            {
                var condition = Builders<Relation>.Filter.Eq(p => p.ID, id);
                var result = collection.Find(condition).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        }

        public void AddNew(Relation value)
        {
            Mongo.Instance.Set<Relation>(Mongo.UserCollectionName, (collection) =>
            {
                collection.InsertOne(value);
            });
        }

        public void Save(Relation relation)
        {
            Mongo.Instance.Set<Relation>(Mongo.RelationCollectionName, collection =>
            {
                var condition = Builders<Relation>.Filter.Eq(p => p.ID, relation.ID);
                collection.ReplaceOneAsync(condition, relation, new UpdateOptions() { IsUpsert = true });
            });
        }
    }
}
