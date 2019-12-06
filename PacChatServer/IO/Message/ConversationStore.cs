using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PacChatServer.IO.Storage;
using PacChatServer.MessageCore.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Message
{
    public class ConversationStore : Store
    {
        private object _lock = new object();

        public AbstractConversation Load(Guid id)
        {
            AbstractConversation conversation = null;

            try
            {
                conversation = Mongo.Instance.Get<AbstractConversation>(id.ToString(), (collection) =>
                {
                    var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, id);
                    var result = collection.Find(condition).Limit(1).ToList();
                    if (result.Count > 0)
                    {
                        return result[0];
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }

            return conversation;
        }

        public void UpdateMessagesList(AbstractConversation conversation)
        {
            lock (_lock)
            {
                Mongo.Instance.Set<AbstractConversation>(conversation.ID.ToString(), collection =>
                {
                    var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, conversation.ID);
                    var update = Builders<AbstractConversation>.Update.Set(p => p.MessagesID, conversation.MessagesID);
                    collection.UpdateOneAsync(condition, update, new UpdateOptions() { IsUpsert = true });
                });
            }
        }

        public void Save(AbstractConversation conversation)
        {
            lock (_lock)
            {
                Mongo.Instance.Set<AbstractConversation>(conversation.ID.ToString(), (collection) =>
                {
                    var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, conversation.ID);
                    collection.ReplaceOneAsync(condition, conversation, new UpdateOptions() { IsUpsert = true });
                });
            }
        }

        public long GetLastActive(Guid conversationID)
        {
            LastActiveTemp conversation = null;

            try
            {
                conversation = Mongo.Instance.Get<LastActiveTemp>(conversationID.ToString(), (collection) =>
                {
                    var condition = Builders<LastActiveTemp>.Filter.Eq(p => p.ID, conversationID);
                    var field = Builders<LastActiveTemp>.Projection.Include(p => p.LastActive);
                    var result = collection.Find(condition).Project<LastActiveTemp>(field).ToList();

                    if (result.Count > 0)
                    {
                        return result[0];
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }

            return conversation != null ? conversation.LastActive : 0;
        }
    }

    public class LastActiveTemp
    {
        [BsonId]
        public Guid ID { get; set; }

        [BsonElement("LastActive")]
        public long LastActive { get; set; }
    }
}
