using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserMessageDesignModel : UserMessageViewModel
    {
        public static UserMessageDesignModel Instance => new UserMessageDesignModel();

        public UserMessageDesignModel()
        {
            Name = "Liam";
            IncomingMsg = "Thank you so much for the awesome videos. I am having problem understanding how to update the chat list dynamically through run-time? I would appreciate the help.";
        }
    }
}
