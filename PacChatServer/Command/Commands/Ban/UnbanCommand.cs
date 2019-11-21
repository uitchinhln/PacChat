using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command.Commands.Ban
{
    public class UnbanCommand : ICommandExecutor
    {
        public void Execute(ISender commandSender, string commandLabel, string[] args)
        {
            if (args.Length < 2) return;

            Guid id = ProfileCache.Instance.ParseEmailToGuid(args[1].ToLower());

            if (id.Equals(Guid.Empty))
            {
                PacChatServer.GetServer().Logger.Error("Email is not exists.");
                return;
            }

            ChatUser user = ChatUserManager.LoadUser(id);

            user.Banned = false;

            user.Save();
        }
    }
}
