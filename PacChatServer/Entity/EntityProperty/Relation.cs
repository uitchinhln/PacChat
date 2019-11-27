using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.IO.Entity.Property;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity.EntityProperty
{
    public class Relation
    {
        [BsonIgnore]
        static ConcurrentDictionary<Guid, Relation> Cache = new ConcurrentDictionary<Guid, Relation>();

        Guid source;
        Guid user1;
        Guid user2;

        [BsonId]
        public Guid ID { get; set; } = Guid.NewGuid();

        [BsonElement("User1")]
        public Guid User1 
        { 
            get => user1;
            set {
                if (value.Equals(User2))
                {
                    throw new Exception("User1 must be distinguished from User2");
                }
                user1 = value;
            }
        }

        [BsonElement("User2")]
        public Guid User2
        {
            get => user2;
            set
            {
                if (value.Equals(User2))
                {
                    throw new Exception("User2 must be distinguished from User1");
                }
                user2 = value;
            }
        }

        [BsonElement("Type")]
        public Type RelationType { get; set; } = Type.None;

        [BsonElement("Source")]
        public Guid Source
        {
            get => source;
            set
            {
                if (!value.Equals(User1) &&  !value.Equals(User2))
                {
                    throw new EntryPointNotFoundException("Source user must be equal to user1 or user2");
                }
                this.source = value;
            }
        }

        [BsonElement("LastUnblock")]
        public DateTime LastUnblock { get; set; } = DateTime.MinValue;

        public void Save()
        {
            new RelationStore().Save(this);
        }

        public enum Type
        {
            None = 0,
            Block = 1,
            Friend = 2,
            FriendRequest = 3
        }

        public static Relation Get(Guid id) {
            if (Cache.ContainsKey(id))
            {
                return Cache[id];
            }

            Relation relation = new RelationStore().Load(id);

            if (relation != null)
            {
                Cache.TryAdd(id, relation);
            }

            return relation;
        }
    }
}
