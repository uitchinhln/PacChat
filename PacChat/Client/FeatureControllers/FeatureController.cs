using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;

namespace PacChat.Client.FeatureControllers
{
    public class FeatureController : Controller<ClientApp>
    {
        public Chat chat { get; private set; }
        public AccountSetting accountSetting { get; private set; }
        public AccountVerifier accountVerifier { get; private set; }

        public FeatureController()
        {
            chat = new Chat();
            accountSetting = new AccountSetting();
            accountVerifier = new AccountVerifier();
        }
    }
}
