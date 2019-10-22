using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;

namespace PacChat.Client
{
    public class ClientModel : Model<ClientApp>
    {
        // Online user list received from Server (hash_id)
        public List<int> onlineUser { get; set; }

        // A socket at client is needed to receive and accept the connection
    }
}
