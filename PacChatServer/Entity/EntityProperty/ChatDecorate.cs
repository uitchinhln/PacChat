using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity.EntityProperty
{
    public class ChatDecorate
    {
        #region Chat Background
        public String BackgroundId { get; set; } = "BG";
        public int BackgroundBlur { get; set; } = 50;

        public int BackgroundColor { get; set; } = 16777215;

        public BackgroundType Use { get; set; } = BackgroundType.Color;
        #endregion

        public int IconColor { get; set; } = 2894892;
    }

    public enum BackgroundType
    {
        Color = 0,
        Image = 1
    }
}
