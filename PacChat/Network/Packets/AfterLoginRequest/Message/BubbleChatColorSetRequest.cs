using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.Resources.CustomControls;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class BubbleChatColorSetRequest : IPacket
    {
        public String ConversationID { get; set; }
        public int BubbleColor { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            BubbleColor = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            byteBuf.WriteInt(BubbleColor);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            MainWindow.chatApplication.model.Conversations.TryGetValue(ConversationID, out var conversationBubble);
            if (conversationBubble == null) return;

            conversationBubble.Color = ColorUtils.IntToColor(BubbleColor);

            if (MainWindow.chatApplication.model.currentSelectedConversation == ConversationID)
            {
                ChatPage.Instance.bubbleColor = conversationBubble.Color;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChatPage.Instance.bubbleColorPicker.ColorPicker.Color = conversationBubble.Color;
                    foreach (var item in ChatPage.Instance.spMessagesContainer.Children)
                    {
                        if (!(item is Bubble)) continue;
                        (item as Bubble).SetBG(conversationBubble.Color);
                        if (conversationBubble.Color.R + conversationBubble.Color.G + conversationBubble.Color.B >= 565)
                            (item as Bubble).SetTextColor(Colors.Black);
                        else
                            (item as Bubble).SetTextColor(Colors.White);
                    }
                });
            }
        }
    }
}
