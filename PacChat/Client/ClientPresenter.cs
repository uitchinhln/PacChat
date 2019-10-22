using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;
using PacChat.Client.FeatureControllers;

namespace PacChat.Client
{
    public class ClientPresenter : Controller<ClientApp>
    {
        public FeatureController featureControllers { get; private set; }

        public ClientPresenter()
        {
            featureControllers = new FeatureController();
        }
    }
}
