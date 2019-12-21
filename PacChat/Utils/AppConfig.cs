using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Utils
{
    public class AppConfig
    {
        [JsonIgnore]
        private String encryptPassword = "Chinh@@Long@@Bach";
        public Dictionary<String, String> SavedUsername = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private AppConfig()
        {
            if (!File.Exists(TempUtil.ConfigFilePath))
            {
                Save();
            }
            
            //Open
        }

        public void Save()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FileStream stream = new FileStream(TempUtil.ConfigFilePath, FileMode.Create, FileAccess.ReadWrite);

                String raw = JsonConvert.SerializeObject(this);

                String encrypted = HashUtils.AESEncrypt(raw, encryptPassword);

            });
        }
        
        public static void StartService()
        {
            if (Instance == null)
            {
                Instance = new AppConfig();
            }
        }

        public static AppConfig Instance { get; private set; }
    }
}
