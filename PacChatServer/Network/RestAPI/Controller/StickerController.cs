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
        [HttpGet, Route("api/message/sticker/category/sticker_detail/{id:int}")]
        public string Category(int id)
        {

            return "Chính " + id;
        }

        [HttpGet, Route("api/message/sticker/category/list")]
        public string CategoryList()
        {
            

            return "100000";
        }
    }
}
