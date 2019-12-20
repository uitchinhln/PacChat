using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PacChat.Utils
{
    public static class ColorUtils
    {
        public static Color IntToColor(int color)
        {
            byte[] bytes = BitConverter.GetBytes(color);
            return Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
        }

        public static int ColorToInt(Color color)
        {
            return BitConverter.ToInt32(new byte[] { color.B, color.G, color.R, color.A }, 0);
        }
    }
}
