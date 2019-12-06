using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using Newtonsoft.Json;
using PacChatServer.MessageCore.Sticker;

namespace PacChatServer.Network.RestfulServer.Alias.AliasHandler
{
    public class StickerCategoryPage : IAliasExecutor
    {
        public void Execute(IDictionary<string, List<string>> parameters, IFullHttpRequest request, IFullHttpResponse response)
        {
            int categoryID = Convert.ToInt32(parameters["id"][0]);
            string result = JsonConvert.SerializeObject(Sticker.LoadedCategories[categoryID]);

            response.Content.WriteString(result, Encoding.UTF8);
        }
    }
}
