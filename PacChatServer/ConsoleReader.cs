using CNetwork.Sessions;
using PacChatServer.Command;
using PacChatServer.Network.Packets;
using PacChatServer.Utils.ThreadUtils;
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
        static Thread readerThread = null;
        static bool IsStop = false;

        public ConsoleReader()
        {
            readerThread = new Thread(() =>
            {
                String input;
                while (!IsStop)
                {
                    try
                    {
                        input = Console.ReadLine();
                        CommandManager.Instance.ExecuteCommand(input);
                    } catch (Exception e)
                    {
                        PacChatServer.GetServer().Logger.Error(e);
                    }
                }
            });
            readerThread.Start();
        }

        public static void Stop()
        {
            if (readerThread != null && readerThread.ThreadState != ThreadState.Stopped)
            {
                IsStop = true;
            }
        }
    }
}
