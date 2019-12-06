using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command
{
    public interface ICommandExecutor
    {
        void Execute(ISender commandSender, string commandLabel, string[] args);
    }
}
