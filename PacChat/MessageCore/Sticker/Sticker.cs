using Newtonsoft.Json;
using PacChat.Network.RestAPI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MessageCore.Sticker
{
    public class Sticker
    {
        public static ConcurrentDictionary<int, Sticker> LoadedStickers { get; private set; } = new ConcurrentDictionary<int, Sticker>();
        public static ConcurrentDictionary<int, StickerCategory> LoadedCategories { get; set; } = new ConcurrentDictionary<int, StickerCategory>();

        public delegate void LoadedHandler();

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("cateId")]
        public int CategoryID { get; set; }

        [JsonProperty("uri")]
        public string URI { get; set; }

        [JsonProperty("stickerUrl")]
        public string StickerURL { get; set; }

        [JsonProperty("stickerSpriteUrl")]
        public string SpriteURL { get; set; }

        [JsonProperty("totalFrames")]
        public int TotalFrames { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        public static void Load(LoadedHandler loadedHandler)
        {
            new Task(() =>
            {
                List<StickerCategory> categories = StickerAPI.GetCategories();
                foreach (StickerCategory cate in categories)
                {
                    LoadedCategories.TryAdd(cate.ID, cate);
                    List<Sticker> stickers = StickerAPI.GetStickerCategory(cate.ID);
                    foreach (Sticker sticker in stickers)
                    {
                        LoadedStickers.TryAdd(sticker.ID, sticker);
                        cate.Stickers.Add(sticker);
                    }
                }
                if (loadedHandler != null)
                    loadedHandler();
            }).Start();
        }
    }
}
