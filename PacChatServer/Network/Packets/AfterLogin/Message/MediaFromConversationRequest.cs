using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class MediaFromConversationRequest : IPacket
    {
        public Guid ConversationID { get; set; }
        public int MediaPosition { get; set; }
        public int Quantity { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
            MediaPosition = buffer.ReadInt();
            Quantity = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            AbstractConversation conversation = new ConversationStore().Load(ConversationID);
            if (conversation == null) return;

            MediaFromConversationResponse packet = new MediaFromConversationResponse();

            if (conversation.MediaID.Count > 0)
            {
                for (int i = MediaPosition; i >= Math.Max(0, MediaPosition - Quantity + 1); --i)
                {
                    AbstractMessage mediaMessage = new MessageStore().Load(conversation.MediaID[i], ConversationID);

                    string fileID, fileName;
                    fileID = fileName = "~";

                    if (mediaMessage is ImageMessage)
                    {
                        fileID = (mediaMessage as ImageMessage).FileID;
                        fileName = (mediaMessage as ImageMessage).FileName;
                    }
                    else if (mediaMessage is VideoMessage)
                    {
                        fileID = (mediaMessage as VideoMessage).FileID;
                        fileName = (mediaMessage as VideoMessage).FileName;
                    }

                    if (fileID.Equals("~") || fileName.Equals("~")) continue;
                    packet.Positions.Add(i);
                    packet.FileIDs.Add(fileID);
                    packet.FileNames.Add(fileName);
                }
            }
            chatSession.Send(packet);
        }
    }
}
