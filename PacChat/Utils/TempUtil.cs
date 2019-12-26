using PacChat.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Utils
{
    public static class TempUtil
    {
        private static readonly String currentVersion = ConfigurationManager.AppSettings["AppVersion"];
        public static readonly String ConfigFilePath = Path.Combine(Path.GetTempPath(), "PacChat/Config.pac");
        public static readonly String DownloadPath = Path.Combine(Path.GetTempPath(), "PacChat/Download/");
        public static readonly String RenderTempPath = Path.Combine(Path.GetTempPath(), "PacChat/Rendering/");
        public static readonly String ResourcePath = Path.Combine(Path.GetTempPath(), "PacChat/Resource/v" + currentVersion + "/");
    }
}
