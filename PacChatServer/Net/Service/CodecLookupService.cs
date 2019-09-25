using PacChatServer.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Net.Service
{    
    class CodecLookupService
    {
        //using opcode to get codec (inbound)
        //using message to get opcode and codec (outbound)
        
        public CodecLookupService()
        {

        }

        public void bind(int opcode, Message message, Codec<Message> codec)
        {
            
        }
    }
}
