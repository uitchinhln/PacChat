using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;
using PacChat.Client.ViewInterfaces;

namespace PacChat.Client
{
    public class ClientView : View<ClientApp>, IClientChat
    {
        #region Chat
        bool IClientChat.SendMessage() { return true; }
        #endregion
    }
}
