using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Sticker
{
    public class BuyStickerCategoryResponse : IPacket
    {
        public int CateID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            CateID = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // da mua dc sticker
            if (!PacChat.MessageCore.Sticker.Sticker.LoadedCategories.ContainsKey(CateID)) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                PacChat.MessageCore.Sticker.Sticker.LoadedCategories.TryGetValue(CateID, out var stickerCate);
                ChatPage.Instance.spTabStickerContainner.AddTabSticker(stickerCate);
            });
        }
    }
}
