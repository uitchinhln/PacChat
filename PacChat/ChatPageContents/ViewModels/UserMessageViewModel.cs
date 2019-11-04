using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserMessageViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string IncomingMsg { get; set; }
    }
}
