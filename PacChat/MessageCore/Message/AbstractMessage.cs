using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MessageCore.Message
{
    public abstract class AbstractMessage : IMessage
    {
        public string ID { get; set; }

        // ID of sender, sender is user
        public string SenderID { get; set; }

        // ID of receiver, modify this in SendTo method
        public string ReceiverID { get; set; }

        // List of user seen this message
        public HashSet<string> SeenBy { get; private set; } = new HashSet<string>();

        // if Revoked = true, the message will be hidden to all users.
        public bool Revoked { get; set; } = false;

        // IDs of users who hide this message away from their conversation, the message still show to the others
        public HashSet<string> HideBy { get; private set; } = new HashSet<string>();

        // IDs of user who reacted and react id;
        public Dictionary<string, int> Reacts { get; private set; } = new Dictionary<string, int>();

    }
}
