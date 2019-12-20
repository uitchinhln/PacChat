using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entity.EntityProperty
{
    public class ChatTheme
    {
        #region Chat Background
        public String BackgroundId { get; set; } = "0";
        public int BackgroundBlur { get; set; } = 50;

        public int BackgroundColor { get; set; } = -1;

        public BackgroundType Use { get; set; } = BackgroundType.Color;
        #endregion

        public int IconColor { get; set; } = -13882324;
    }

    public enum BackgroundType
    {
        Color = 0,
        Image = 1
    }
}
