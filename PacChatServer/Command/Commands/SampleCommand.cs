﻿using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.IO.Entity;
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
            if (args.Length < 3) return;

            string email = args[2];

            if (args[1] == "get")
            {
                Guid id = ProfileCache.Instance.ParseEmailToGuid(email);
                ChatUserProfile profile = ProfileCache.Instance.GetUserProfile(id);
                if (profile == null)
                {
                    Console.WriteLine("NULL");
                }
                else
                {
                    Console.WriteLine(profile.ID);
                }
            }

            if (args[1] == "add" && args.Length >= 4)
            {
                Random r = new Random();

                if (ProfileCache.Instance.ParseEmailToGuid(email) != Guid.Empty)
                {
                    PacChatServer.GetServer().Logger.Error("Create fail: email early existed!!!");
                    return;
                }

                Guid id = Guid.NewGuid();

                ChatUser user = new ChatUser()
                {
                    ID = id,
                    Email = email,
                    Password = HashUtils.MD5(HashUtils.MD5(args[3]) + id),
                    FirstName = "Admin",
                    LastName = "Admin's lastname",
                    DateOfBirth = new DateTime(1998, r.Next(11) + 1, r.Next(29) + 1),
                    Gender = Entity.EntityProperty.Gender.Male
                };

                bool added = new ChatUserStore().AddNew(user);
                if (added)
                {                
                    PacChatServer.GetServer().Logger.Info(String.Format("Account {0} has registered successfully. ID = {1}", user.Email, user.ID));
                }
                else
                {
                    PacChatServer.GetServer().Logger.Error("Create fail: Database error!!!");
                }
            }

            if (args[1] == "search" && args.Length >=3)
            {
                List<String> ids = new ChatUserStore().SearchUserIDByEmail(args[2]);
                foreach (string s in ids)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
