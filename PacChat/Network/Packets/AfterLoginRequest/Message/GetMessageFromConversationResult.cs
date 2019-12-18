using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class GetMessageFromConversationResult : IPacket
    {
        //List of user seen this message
        public HashSet<string> SeenBy { get; private set; } = new HashSet<string>();
        //if Revoked = true, the message will be hidden to all users.
        public bool Revoked { get; set; } = false;
        //IDs of user who reacted and react id;
        public Dictionary<string, int> Reacts { get; private set; } = new Dictionary<string, int>();

        //Type of message
        /// <summary>
        /// 1: Attachment
        /// 2: Image
        /// 3: Sticker
        /// 4: Text
        /// 5: Video
        /// </summary>
        public List<int> MessType { get; set; } = new List<int>();
        public List<string> SenderID { get; set; } = new List<string>();

        public List<AbstractMessage> Content { get; set; } = new List<AbstractMessage>();

        public void Decode(IByteBuffer buffer)
        {
            /*
            // Get ids that have seen this message
            string temp = ByteBufUtils.ReadUTF8(buffer);
            while (temp != "~")
            {
                SeenBy.Add(temp);
                temp = ByteBufUtils.ReadUTF8(buffer);
            }

            Revoked = buffer.ReadInt() == 1 ? true : false;
            
            // Get ids that have reacted this message & their reaction id (int)
            // Please write to buffer in this format: user_id_0-reaction_id_0-user_id_1-reaction_id_1-...
            temp = ByteBufUtils.ReadUTF8(buffer);
            int reactionID = buffer.ReadInt();
            while (temp != "~")
            {
                Reacts.Add(temp, reactionID);
                temp = ByteBufUtils.ReadUTF8(buffer);
                reactionID = buffer.ReadInt();
            }
            */

            string temp = ByteBufUtils.ReadUTF8(buffer);

            while (!temp.Equals("~"))
            {
                SenderID.Add(temp);
                MessType.Add(buffer.ReadInt());

                AbstractMessage Message = null;
                switch (MessType.Last())
                {
                    case 1:
                        Message = new AttachmentMessage();
                        (Message as AttachmentMessage).DecodeFromBuffer(buffer);
                        break;
                    case 2:
                        Message = new ImageMessage();
                        (Message as ImageMessage).DecodeFromBuffer(buffer);
                        break;
                    case 3:
                        Message = new StickerMessage();
                        (Message as StickerMessage).DecodeFromBuffer(buffer);
                        break;
                    case 4:
                        Message = new TextMessage();
                        (Message as TextMessage).DecodeFromBuffer(buffer);
                        break;
                    case 5:
                        Message = new VideoMessage();
                        (Message as VideoMessage).DecodeFromBuffer(buffer);
                        break;
                }
                Content.Add(Message);
                temp = ByteBufUtils.ReadUTF8(buffer);
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // Create a message instance and put information into it
            Application.Current.Dispatcher.Invoke(() =>
            {
                var app = MainWindow.chatApplication;
                for (int i = 0; i < SenderID.Count; ++i)
                {
                    Content[i].SenderID = SenderID[i];

                    if (app.model.SelfID.CompareTo(SenderID[i]) == 0)
                        ChatPage.Instance.SendMessage(
                            Content[i],
                            true, true);
                    else
                        ChatPage.Instance.SendLeftMessages(
                            Content[i],
                            true, true);
                }
            });
        }
    }
}
