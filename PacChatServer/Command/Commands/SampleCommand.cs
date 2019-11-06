using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command.Commands
{
    public class SampleCommand : ICommandExecutor
    {
        public void Execute(string commandLabel, string[] args)
        {
            int f = Convert.ToInt32(args[1]);
            int s = Convert.ToInt32(args[2]);
            PacChatServer.GetServer().Logger.Info(String.Format("{0} + {1} = {2}", f, s, s+f));
        }
    }
}
