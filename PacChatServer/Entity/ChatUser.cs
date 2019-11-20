using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity
{
    public class ChatUser : AbstractEntity
    {
        List<ChatSession> sessions = new List<ChatSession>();

        [BsonElement("FirstName")]
        public String FirstName { get; set; }

        [BsonElement("LastName")]
        public String LastName { get; set; }

        [BsonElement("Town")]
        public String Town { get; set; } = "Default";

        [BsonElement("Email")]
        public String Email { get; set; }

        [BsonElement("Password")]
        public String Password { get; set; }

        [BsonElement("DoB")]
        public DateTime DateOfBirth { get; set; }

        [BsonElement("Gender")]
        public Gender Gender { get; set; }

        //LastLogon is the time that last session logged in
        [BsonElement("LastLogon")]
        public DateTime LastLogon { get; set; } = DateTime.Now;

        //LastLogoff is the time that last session logged out
        [BsonElement("LastLogoff")]
        public DateTime LastLogoff { get; set; } = DateTime.Now;

        //Key is id of user who have a reationship with this user, Value is id of relationship
        [BsonElement("Relationship")]
        public Dictionary<Guid, Guid> Relationship { get; private set; } = new Dictionary<Guid, Guid>();

        [BsonElement("Conversations")]
        public Dictionary<Guid, long> Conversations { get; private set; } = new Dictionary<Guid, long>();

        //True if this user has been banned
        [BsonElement("Banned")]
        public bool Banned { get; set; } = false;

        public ChatUser()
        {

        }

        public ChatUser(ChatUserProfile profile)
        {
            this.ID = profile.ID;
            this.Email = profile.Email;
            this.Password = profile.PassHashed;
            this.FirstName = profile.FirstName;
            this.LastName = profile.LastName;
            this.DateOfBirth = profile.DoB;
            this.Gender = profile.Gender;
        }

        public List<ChatSession> GetSessions()
        {
            return sessions;
        }

        public bool IsOnline()
        {
            return sessions.Count > 0;
        }

        public override void Save()
        {
            //Save to db
        }
    }
}
