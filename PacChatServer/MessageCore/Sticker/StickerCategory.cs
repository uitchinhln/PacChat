using Newtonsoft.Json;
using PacChatServer.IO.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Sticker
{
    public class StickerCategory
    {
        [JsonIgnore]
        public List<Sticker> Stickers { get; set; } = new List<Sticker>();

        private string thumbImg = "";

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("totalImage")]
        public int TotalImage { get; set; }

        [JsonProperty("thumbUrl")]
        public string ThumbURL { get; set; }

        [JsonProperty("iconUrl")]
        public string IconURL { get; set; }

        [JsonProperty("iconPreview")]
        public string IconPreview { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("group")]
        public int Group { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("thumbImg")]
        public string ThumbImg
        {
            get => thumbImg;
            set {
                string[] args = value.Split(new string[] { "https" }, StringSplitOptions.None);
                thumbImg = "https" + args[args.Length-1];
            }
        }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("sourceUrl")]
        public string SourceURL { get; set; }

        [JsonProperty("permission")]
        public int Permisson { get; set; }

        [JsonProperty("expireTime")]
        public long ExpireTime { get; set; }

        [JsonProperty("is_hidden")]
        public int IsHidden { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
