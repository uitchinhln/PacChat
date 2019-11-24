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
    public class Sticker
    {
        public static ConcurrentDictionary<int, Sticker> LoadedStickers { get; private set; } = new ConcurrentDictionary<int, Sticker>();
        public static ConcurrentDictionary<int, StickerCategory> LoadedCategories { get; set; } = new ConcurrentDictionary<int, StickerCategory>();

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

        public static void StartService()
        {
            Task.Run(() =>
            {
                StickerLoader loader = new StickerLoader();

                List<StickerCategory> raw = loader.LoadCategory();
                foreach (StickerCategory cate in raw)
                {
                    LoadedCategories.TryAdd(cate.ID, cate);
                }

                List<List<Sticker>> stickers = loader.LoadSticker();
                int total = 0;
                foreach (List<Sticker> cate in stickers)
                {
                    foreach (Sticker sticker in cate)
                    {
                        LoadedStickers.TryAdd(sticker.ID, sticker);

                        if (LoadedCategories.ContainsKey(sticker.CategoryID))
                        {
                            LoadedCategories[sticker.CategoryID].Stickers.Add(sticker);
                        }

                        total++;
                    }
                }

                PacChatServer.GetServer().Logger.Info(String.Format("Loaded {0} categories, {1} stickers", LoadedCategories.Count, total));
            });
        }
    }
}
