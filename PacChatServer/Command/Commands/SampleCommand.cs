using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command.Commands
{
    public class SampleCommand : ICommandExecutor
    {
        public void Execute(ISender commandSender, string commandLabel, string[] args)
        {
            //if (args[1] == "get")
            //{
            //    User user = MySQLSto.Instance.GetUser(args[2]);
            //    if (user == null)
            //    {
            //        Console.WriteLine("NULL");
            //    } else
            //    {
            //        Console.WriteLine(user.ID);
            //    }
            //}

            //if (args[1] == "add")
            //{
            //    Random r = new Random();
            //    User user = new User(-1);
            //    user.Email = args[2];
            //    user.PassHashed = HashUtils.MD5(HashUtils.MD5(args[3]));
            //    user.FirstName = "Chính";
            //    user.LastName = "Lê Ngọc";
            //    user.DoB = new DateTime(1998, r.Next(11)+1, r.Next(29)+1);
            //    user.Gender = Entities.Properties.Gender.Male;

            //    Console.WriteLine(MySQLSto.Instance.AddNewUser(user).ID);
            //}
        }
    }
}
