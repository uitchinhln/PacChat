﻿using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class SendMessageResponse : IPacket
    {
        public string ConversationID { get; set; }
        public string SenderID { get; set; }
        public AbstractMessage Message { get; set; }

        public void Decode(IByteBuffer buffer)
        {
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            ByteBufUtils.WriteUTF8(byteBuf, SenderID);
            byteBuf = Message.EncodeToBuffer(byteBuf);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
        }
    }
}
