using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command
{
    public class ConsoleSender : ISender
    {
        static ConsoleSender instance;

        private ConsoleSender()
        {

        }

        public static ConsoleSender Instance
        {
            get
            {
                if (instance == null) instance = new ConsoleSender();
                return instance;
            }
        }
    }
}
