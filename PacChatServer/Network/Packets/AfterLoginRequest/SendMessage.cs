﻿using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Network.Protocol;
using PacChatServer.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class SendMessage : IPacket
    {
        public int DestID { get; set; }
        public string Message { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            DestID = buffer.ReadInt();        
            Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;  
            //if (PacChatServer.GetServer().OnlineUsers.ContainsKey(DestID))
            //{
            //    SendMessageResult packet = new SendMessageResult();
            //    packet.SourceID = chatSession.Owner.ID;
            //    packet.Message = Message;
            //    foreach (ChatSession destSession in PacChatServer.GetServer().OnlineUsers[DestID].sessions)
            //    {
            //        destSession.Send(packet);
            //    }               
            //}                        
        }
    }
}
