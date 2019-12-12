using PacChat.Network.RestAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Utils
{
    public static class ResourceUtil
    {        
        public static readonly String ChatPageBG = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG.jpg");
        public static readonly String ChatPageBG1 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG1.jpg");
        public static readonly String ChatPageBG2 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG2.jpg");
        public static readonly String ChatPageBG3 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG3.jpg");
        public static readonly String ChatPageBG4 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG4.jpg");
        public static readonly String ChatPageBG5 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG5.jpg");
        public static readonly String ChatPageBG6 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG6.jpg");
        public static readonly String ChatPageBG7 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG7.jpg");
        public static readonly String ChatPageBG8 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG8.jpg");
        public static readonly String ChatPageBG9 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG9.jpg");
        public static readonly String ChatPageBG10 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG10.jpg");
        public static readonly String ChatPageBG11 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG11.jpg");
        public static readonly String ChatPageBG12 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG12.jpg");
        public static readonly String ChatPageBG13 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG13.jpg");
        public static readonly String ChatPageBG14 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG14.jpg");
        public static readonly String ChatPageBG15 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG15.jpg");
        public static readonly String ChatPageBG16 = Path.Combine(TempUtil.ResourcePath, "ChatPageBg/BG16.jpg");

        private static List<String> resources = new List<string>()
        {
            ChatPageBG, ChatPageBG1, ChatPageBG2, ChatPageBG3, ChatPageBG4, ChatPageBG5, ChatPageBG6,
            ChatPageBG7, ChatPageBG8, ChatPageBG9, ChatPageBG10, ChatPageBG11, ChatPageBG12, ChatPageBG13,
            ChatPageBG14, ChatPageBG15, ChatPageBG16
        };

        public static void PrepareResource()
        {
            foreach (String resource in resources)
            {
                if (File.Exists(resource)) continue;
                ResourceAPI.DownloadResource(resource, null, (sender, e) =>
                    {

                    }, (e) => Console.WriteLine(e));
            }
        }
    }
}
