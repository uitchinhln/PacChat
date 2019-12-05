using PacChatServer.Network.RestfulServer.Alias.AliasHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestfulServer.Alias
{
    public class AliasInitialize
    {
        public void Initialize(AliasManager alias)
        {
            alias.RegisterAlias("/api/sticker/category/", new StickerCategoryPage());
        }
    }
}
