using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.IO.Entity;

namespace PacChatServer.MessageCore.Message
{
    public class SystemAddNewUserMessage : AbstractMessage
    {
        public Guid AdderID { get; set; }
        public Guid AddedID { get; set; }

        public override void DecodeFromBuffer(IByteBuffer buffer)
        {
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            string notification = new ChatUserStore().Load(AdderID).FirstName +
                " invited " + new ChatUserStore().Load(AddedID).FirstName +
                " to group";
            ByteBufUtils.WriteUTF8(buffer, notification);
            return buffer;
        }

        public override int GetPreviewCode()
        {
            return 6;
        }
    }
}
