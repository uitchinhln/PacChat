using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.Entity.EntityProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class ChatThemeSetRequest : IPacket
    {
        public ChatTheme Theme { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Theme = new ChatTheme();
            Theme.BackgroundId = ByteBufUtils.ReadUTF8(buffer);
            Theme.BackgroundBlur = buffer.ReadInt();

            Theme.BackgroundColor = buffer.ReadInt();
            Theme.Use = (BackgroundType)buffer.ReadInt();

            Theme.IconColor = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, Theme.BackgroundId);
            byteBuf.WriteInt(Theme.BackgroundBlur);

            byteBuf.WriteInt(Theme.BackgroundColor);

            byteBuf.WriteInt((int)Theme.Use);

            byteBuf.WriteInt(Theme.IconColor);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            ChatUser user = chatSession.Owner;

            user.ChatThemeSettings.BackgroundId = Theme.BackgroundId;
            user.ChatThemeSettings.BackgroundBlur = Theme.BackgroundBlur;

            user.ChatThemeSettings.BackgroundColor = Theme.BackgroundColor;

            user.ChatThemeSettings.Use = Theme.Use;

            user.ChatThemeSettings.IconColor = Theme.IconColor;

            user.SaveChatTheme();

            user.Send(this, chatSession);
        }
    }
}
