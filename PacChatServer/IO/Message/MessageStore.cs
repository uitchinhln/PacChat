using MongoDB.Driver;
using PacChatServer.IO.Storage;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Message
{
    public class MessageStore : Store
    {
        public AbstractMessage Load(Guid id, Guid conversationID)
        {
            return Mongo.Instance.Get<AbstractMessage>(conversationID.ToString(), collection =>
            {
                var condition = Builders<AbstractMessage>.Filter.Eq(p => p.ID, id);
                var result = collection.Find(condition).Limit(1).ToList();
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            });
        } 

        public void Save(AbstractMessage message, Guid conversationID)
        {
            Mongo.Instance.Set<AbstractMessage>(conversationID.ToString(), collection =>
            {
                var condition = Builders<AbstractMessage>.Filter.Eq(p => p.ID, message.ID);
                collection.ReplaceOneAsync(condition, message, new UpdateOptions() { IsUpsert = true });
            });
        } 
    }
}
