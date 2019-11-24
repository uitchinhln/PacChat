using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MessageCore.Message
{
    public abstract class AbstractMessage : IMessage
    {
        public Guid ID { get; set; }

        //ID of sender, sender is user
        public Guid SenderID { get; set; }

        //List of user seen this message
        public HashSet<Guid> SeenBy { get; private set; } = new HashSet<Guid>();

        //if Revoked = true, the message will be hidden to all users.
        public bool Revoked { get; set; } = false;

        //IDs of users who hide this message away from their conversation, the message still show to the others
        public HashSet<Guid> HideBy { get; private set; } = new HashSet<Guid>();

        //IDs of user who reacted and react id;
        public Dictionary<Guid, int> Reacts { get; private set; } = new Dictionary<Guid, int>();

        public void Reply()
        {
            throw new NotImplementedException();
        }

        public void SendTo(Guid receiver)
        {
            throw new NotImplementedException();
        }
    }
}
