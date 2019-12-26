using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace PacChatServer.Network.RestAPI
{
    public class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        public CustomContentTypeProvider()
        {
            try
            {
                Mappings.Add(".json", "application/json");
            }
            catch (Exception)
            { }
        }
    }
}
