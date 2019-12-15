using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.Resources.CustomControls;
using System;
using System.Collections.Generic;
using PacChat.MessageCore.Sticker;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Sticker
{
    public class GetBoughtStickerPacksResponse : IPacket
    {
        public List<int> BoughtStickerPacks { get; set; } = new List<int>();

        public void Decode(IByteBuffer buffer)
        {
            int id = buffer.ReadInt();

            while (id > 0)
            {
                BoughtStickerPacks.Add(id);
                id = buffer.ReadInt();
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            foreach (var Cate in MessageCore.Sticker.Sticker.LoadedCategories )
            {
                //Cate da duoc mua
                if (BoughtStickerPacks.Contains(Cate.Key))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PacChat.MessageCore.Sticker.Sticker.LoadedCategories.TryGetValue(Cate.Key, out var stickerCate);
                        ChatPage.Instance.spTabStickerContainner.AddTabSticker(stickerCate);
                    });
                }
                else
                {
                    // cate chua duoc mua
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PacChat.MessageCore.Sticker.Sticker.LoadedCategories.TryGetValue(Cate.Key, out var stickerCate);
                        ChatPage.Instance.spTabStickerContainner.AddCateIntoStore(stickerCate);
                    });
                }
            }

        }
    }
}
