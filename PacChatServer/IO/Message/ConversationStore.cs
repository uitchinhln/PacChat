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
        public AbstractConversation Load(Guid id)
        {
            AbstractConversation conversation = null;

            try
            {
                conversation = Mongo.Instance.Get<AbstractConversation>(id.ToString(), (collection) =>
                {
                    var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, id);
                    var result = collection.Find(condition).ToList();
                    if (result.Count > 0)
                    {
                        return result[0];
                    }
                    return null;
                });
            } catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }

            return conversation;
        }

        public void UpdateMessagesList(AbstractConversation conversation)
        {
            Mongo.Instance.Set<AbstractConversation>(conversation.ID.ToString(), collection =>
            {
                var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, conversation.ID);
                var update = Builders<AbstractConversation>.Update.Set(p => p.MessagesID, conversation.MessagesID);
                collection.UpdateOneAsync(condition, update, new UpdateOptions() { IsUpsert = true });
            });
        }

        public void Save(AbstractConversation conversation)
        {
            Mongo.Instance.Set<AbstractConversation>(conversation.ID.ToString(), (collection) =>
            {
                var condition = Builders<AbstractConversation>.Filter.Eq(p => p.ID, conversation.ID);
                collection.ReplaceOneAsync(condition, conversation, new UpdateOptions() { IsUpsert = true });
            });
        }

        public void AddNew(AbstractConversation conversation)
        {
            Mongo.Instance.Set<AbstractConversation>(conversation.ID.ToString(), (collection) =>
            {
                collection.InsertOne(conversation);
            });
        }
    }
}
