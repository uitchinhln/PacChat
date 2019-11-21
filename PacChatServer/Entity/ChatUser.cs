using CNetwork;
using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.IO.Entity;
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
        [BsonIgnore]
        public HashSet<ChatSession> sessions { get; set; } = new HashSet<ChatSession>();

        [BsonIgnore]
        ChatUserStore saver = new ChatUserStore();

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
        
        [BsonElement("NearestStickers")]
        public HashSet<int> NearestStickers { get; private set; } = new HashSet<int>();        

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
            this.Banned = profile.Banned;
            this.LastLogoff = profile.LastLogoff;
        }

        public bool IsOnline()
        {
            return sessions.Count > 0;
        }

        public void Send(IPacket packet, params ChatSession[] ignoreSessions)
        {
            foreach(ChatSession session in sessions)
            {
                if (!ignoreSessions.Contains<ChatSession>(session))
                {
                    session.Send(packet);
                }
            }
        }

        public void Online()
        {
            
        }

        public void Offline()
        {
            this.LastLogoff = DateTime.Now;
            this.Save();
            PacChatServer.GetServer().Logger.Info(String.Format("User {0} has logged out!", this.Email));
        }

        public void Kick()
        {
            foreach(ChatSession session in sessions)
            {
                session.Disconnect();
            }
        }

        public override void Save()
        {
            saver.Update(this);
            ChatUserProfile profile = ProfileCache.Instance.GetUserProfile(this.ID);

            profile.Email = this.Email;
            profile.PassHashed = this.Password;
            profile.FirstName = this.FirstName;
            profile.LastName = this.LastName;
            profile.DoB = this.DateOfBirth;
            profile.Gender = this.Gender;
            profile.Banned = this.Banned;
            profile.LastLogoff = this.LastLogoff;
        }
    }
}
