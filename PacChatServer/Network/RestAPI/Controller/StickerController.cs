using Newtonsoft.Json;
using PacChatServer.MessageCore.Sticker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PacChatServer.Network.RestAPI.Controller
{
    public class StickerController : ApiController
    {
        [HttpGet, Route("api/message/sticker/category/stickers/{id:int}")]
        public ICollection<Sticker> Category(int id)
        {
            if (!Verifier.VerifyRequestToken(Request))
                throw new UnauthorizedAccessException();

            if (Sticker.LoadedCategories.ContainsKey(id))
            {
                return Sticker.LoadedCategories[id].Stickers;
            }

            throw new EntryPointNotFoundException();
        }

        [HttpGet, Route("api/message/sticker/category/list")]
        public ICollection<StickerCategory> CategoryList()
        {
            if (!Verifier.VerifyRequestToken(Request))
                throw new UnauthorizedAccessException();

            return Sticker.LoadedCategories.Values;
        }
    }
}
