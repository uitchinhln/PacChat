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
    public class ConsoleManager
    {
        static Thread consoleReader = null;
        static bool IsStop = false;

        public ConsoleManager()
        {
            consoleReader = new Thread(() =>
            {
                String input;
                while (!IsStop)
                {
                    try
                    {
                        input = Console.ReadLine();

                        if (input == null || input.Trim().Length == 0) continue;

                        CommandManager.Instance.ExecuteCommand(ConsoleSender.Instance, input);
                    } catch (Exception e)
                    {
                        PacChatServer.GetServer().Logger.Error(e);
                    }
                }
            });
            consoleReader.Start();
        }

        public static void Stop()
        {
            IsStop = true;
        }
    }
}
