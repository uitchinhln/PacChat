using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Message
{
    public class VideoMessage : AbstractMessage
    {
        public override int GetPreviewCode()
        {
            return 5;
        }
    }
}
