using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity
{
    public abstract class AbstractEntity : IEntity
    {
        [BsonId]
        public Guid ID { get; set; }

        public abstract void Save();
    }
}
