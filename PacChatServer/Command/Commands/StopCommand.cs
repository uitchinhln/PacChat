using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command.Commands
{
    public class StopCommand : ICommandExecutor
    {
        public void Execute(string commandLabel, string[] args)
        {
            PacChatServer.GetServer().Logger.Info("Stopping...");
            ConsoleReader.Stop();
            Environment.Exit(0);
        }
    }
}
