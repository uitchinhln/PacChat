using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity.EntityProperty
{
    public class Relation
    {
        static Dictionary<Guid, Relation> Cache = new Dictionary<Guid, Relation>();

        Guid source;
        Guid user1;
        Guid user2;

        private Relation()
        {

        }

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
        public Type RelationType { get; set; }
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

        public DateTime LastUnblock { get; set; } = DateTime.MinValue;

        public void Save()
        {
            //save to db
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
            //load from db
            return new Relation();
        }
    }
}
