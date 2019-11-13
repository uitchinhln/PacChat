using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.ChatPageContents.ViewModels
{
    public class UserListViewModel: BaseViewModel
    {
        public List<UserMessageViewModel> RecentUsers { get; set; }
        public List<UserMessageViewModel> Contacts { get; set; }
    }
}
