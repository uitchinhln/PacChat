using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PacChatServer.IO.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Message
{
    public class AttachmentStore : Store
    {
        
        public static void Map(Guid hash, String fileName)
        {
            FileMap map = new FileMap(hash, fileName);
            Mongo.Instance.Set<FileMap>(Mongo.FileMapName, collection =>
            {
                collection.InsertOneAsync(map);
            });
        }

        public static String Parse(Guid hash)
        {
            FileMap map = Mongo.Instance.Get<FileMap>(Mongo.FileMapName, collection =>
            {
                var condition = Builders<FileMap>.Filter.Eq(p => p.Hash, hash);
                var result = collection.Find(condition).Limit(1).ToList();
                return result.FirstOrDefault();
            });

            if (map == null) return null;

            return map.FileName;
            
        }


    }

    public class FileMap
    {
        [BsonId]
        public Guid Hash { get; set; }
        [BsonElement("Name")]
        public string FileName { get; set; }

        public FileMap()
        {

        }

        public FileMap(Guid hash, String fileName)
        {
            this.Hash = hash;
            this.FileName = fileName;
        }
    }
}
