using PacChatServer.Command.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Command
{
    public class CommandManager
    {
        static CommandManager instance;

        SortedDictionary<String, ICommandExecutor> registeredCommands = null;
        SortedDictionary<String, ICommandExecutor> defaultCommands = null;

        PacChatServer server = null;

        private CommandManager()
        {
            this.server = PacChatServer.GetServer();
            registeredCommands = new SortedDictionary<string, ICommandExecutor>();
            defaultCommands = new SortedDictionary<string, ICommandExecutor>();

            RegisterAllDefaultCommands();
        }

        private void RegisterAllDefaultCommands()
        {
            defaultCommands.Add(DefaultCommands.STOP, new StopCommand());
        }

        public void RegisterCommand(string commandLabel, ICommandExecutor executor)
        {
            try
            {
                if (commandLabel == null) return;
                commandLabel = commandLabel.Trim().ToUpper();

                if (commandLabel.Length == 0)
                {
                    Exception e = new Exception(String.Format("The command must have at least 1 character (excluding spaces)!"));
                    throw e;
                }
                if (executor == null)
                {
                    Exception e = new Exception(String.Format("Executor can't be null!!!"));
                    throw e;
                }
                if (registeredCommands.ContainsKey(commandLabel))
                {
                    Exception e = new Exception(String.Format("The command \"{0}\" already exists!!!", commandLabel.ToLower()));
                    throw e;
                }
                registeredCommands.Add(commandLabel, executor);
            } catch (Exception e)
            {
                server.Logger.Error(e);
            }
        }

        public void ExecuteCommand(ISender sender, String command)
        {
            try
            {
                if (command == null) return;
                command = command.Trim();
                if (command.Length < 1) return;

                String[] args = command.Split(' ');
                String commandLabel = args[0].ToUpper();

                if (!registeredCommands.ContainsKey(commandLabel) && !defaultCommands.ContainsKey(commandLabel))
                {
                    server.Logger.Error(String.Format("Command \"{0}\" is not exists!!!", commandLabel.ToLower()));
                    return;
                }

                if (registeredCommands.ContainsKey(commandLabel))
                {
                    registeredCommands[commandLabel].Execute(sender, commandLabel, args);
                    return;
                }

                if (defaultCommands.ContainsKey(commandLabel))
                {
                    defaultCommands[commandLabel].Execute(sender, commandLabel, args);
                    return;
                }
            } catch (Exception e)
            {
                server.Logger.Error(e);
                throw;
            }
        }

        public void Unregister(string commandLabel)
        {
            commandLabel = commandLabel.Trim().ToUpper();
            if (registeredCommands.ContainsKey(commandLabel))
            {
                registeredCommands.Remove(commandLabel);
            }
        }

        public void UnregisterAll()
        {
            registeredCommands.Clear();
        }

        public static CommandManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandManager();
                }
                return instance;
            }
        }
    }
}
