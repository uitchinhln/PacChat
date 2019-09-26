using Force.DeepCloner;
using log4net;
using PacChatServer.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Net.Service
{    
    class CodecFactory
    {
        private ILog Logger = log4net.LogManager.GetLogger(typeof(CodecFactory));

        //using opcode to get codec (inbound)
        Dictionary<int, Codec<Message>> registeredOpcode = new Dictionary<int, Codec<Message>>();
        Dictionary<int, Handler<Message>> registeredHandler = new Dictionary<int, Handler<Message>>();
        Dictionary<Type, CodecRegistration> registeredMessage = new Dictionary<Type, CodecRegistration>();

        //using message to get opcode and codec (outbound)
        
        public CodecFactory()
        {
            
        }

        public void Bind(int opcode, Type message, Codec<Message> codec)
        {
            //CodecRegistration codecRegistration = registeredMessage.

            if (message == null) throw new NullReferenceException("Message is null");
            if (codec == null) throw new NullReferenceException("Codec is null");
            if (!(message is Message)) throw new InvalidOperationException("'message' must be derived form class Message");

            

            try
            {
                codec.DeepClone();
            } catch (Exception e)
            {
                Logger.Error(e);
            }


        }


    }
}
