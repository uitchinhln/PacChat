using MongoDB.Bson;
using MongoDB.Driver;
using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Storage
{
    public class Mongo
    {
        private IMongoDatabase db;
        private MongoClient client;

        private int TryLimit = 10;
        private int TryCount = 0;

        public delegate T Query<T>(IMongoCollection<T> collection);
        public delegate void ExecuteNonQuery<T>(IMongoCollection<T> collection);

        public static readonly string DatabaseName = "PacChat";
        public static readonly string UserCollectionName = "Users";

        private Mongo()
        {
            Init();
        }

        private void Init()
        {
            client = new MongoClient();
            db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<ChatUser>(UserCollectionName);
            collection.Indexes.CreateOneAsync(new CreateIndexModel<ChatUser>(Builders<ChatUser>.IndexKeys.Ascending(p => p.Email))).Wait();
        }

        public T Get<T>(string collectionName, Query<T> executable)
        {
            T result = default(T);

            try
            {
                var collection = db.GetCollection<T>(collectionName);
                result = executable(collection);
            } catch (MongoConnectionClosedException e)
            {
                if (TryLimit < TryCount) return result;
                TryCount++;
                PacChatServer.GetServer().Logger.Error(e);
                client = new MongoClient();
                db = client.GetDatabase(DatabaseName);

                result = Get<T>(collectionName, executable);
            }
            TryCount = 0;

            return result;
        }

        public void Set<T>(string collectionName, ExecuteNonQuery<T> executable)
        {
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                executable(collection);
            }
            catch (MongoConnectionClosedException e)
            {
                if (TryLimit < TryCount) return;
                TryCount++;
                PacChatServer.GetServer().Logger.Error(e);
                client = new MongoClient();
                db = client.GetDatabase(DatabaseName);

                Set<T>(collectionName, executable);
            }
            TryCount = 0;
        }

        public static void StartService(bool forceRestart = false)
        {
            if (Instance == null || forceRestart)
            {
                Instance = new Mongo();
            }
        }

        public static Mongo Instance { get; private set; }
    }
}
