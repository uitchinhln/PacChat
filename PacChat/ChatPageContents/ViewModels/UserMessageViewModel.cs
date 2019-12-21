using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserMessageViewModel : BaseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        private string _incomingMsg = String.Empty;
        public string IncomingMsg
        {
            get => _incomingMsg;
            set 
            {
                _incomingMsg = value.Replace(Environment.NewLine, " ").Replace("\n", " ");
            }
        }
        public bool IsOnline { get; set; }
    }
}
