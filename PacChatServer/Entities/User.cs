using PacChatServer.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entities
{
    public class User
    {
        public List<ChatSession> sessions = new List<ChatSession>();

        public int ID { get; }
        public string Email { get; set; }
        public string PassHashed { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public Gender Gender { get; set; }
        SortedList FriendIDs { get; set; }
        SortedList BlockedIDs { get; set; }
    }
}
