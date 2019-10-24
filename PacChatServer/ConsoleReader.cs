using CNetwork.Sessions;
using PacChatServer.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacChatServer
{
    public class ConsoleReader
    {
        public ConsoleReader()
        {
            Thread thread = new Thread(() =>
            {
                String input;
                while (true)
                {
                    input = Console.ReadLine();


                }                
            });
            thread.Start();
        }
    }
}
