using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.Resources.CustomControls;
using System;
using System.Collections.Generic;
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
            foreach (int cateID in BoughtStickerPacks)
            {
                //CateID tồn tại thì k load lên
                if (!PacChat.MessageCore.Sticker.Sticker.LoadedCategories.ContainsKey(cateID)) continue;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    PacChat.MessageCore.Sticker.Sticker.LoadedCategories.TryGetValue(cateID, out var stickerCate);
                    ChatPage.Instance.spTabStickerContainner.AddTabSticker(stickerCate);
                });
            }
        }
    }
}
