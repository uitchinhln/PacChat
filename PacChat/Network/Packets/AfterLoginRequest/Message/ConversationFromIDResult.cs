using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore;
using CNetwork.Utils;
using System.Windows;
using System.Windows.Media;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class ConversationFromIDResult : IPacket
    {
        public string ConversationID { get; set; }
        public string ConversationName { get; set; }
        public int StatusCode { get; set; }
        public long LastActive { get; set; }
        public HashSet<string> Members { get; set; } = new HashSet<string>();
        public int LastMessID { get; set; }
        public int LastMediaID { get; set; }
        public int LastAttachmentID { get; set; }
        public int PreviewCode { get; set; }
        public int BubbleColor { get; set; }
        public string PreviewContent { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            ConversationName = ByteBufUtils.ReadUTF8(buffer);
            StatusCode = buffer.ReadInt();
            LastActive = buffer.ReadLong();

            // Get the number of members in this conversation
            string temp = ByteBufUtils.ReadUTF8(buffer);
            while (temp != "~")
            {
                Members.Add(temp);
                temp = ByteBufUtils.ReadUTF8(buffer);
            }

            LastMessID = buffer.ReadInt();
            LastMediaID = buffer.ReadInt();
            LastAttachmentID = buffer.ReadInt();
            PreviewCode = buffer.ReadInt();
            BubbleColor = buffer.ReadInt();
            PreviewContent = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // Load message to UI
            Application.Current.Dispatcher.Invoke(() =>
            {
                var app = MainWindow.chatApplication;

                app.model.Conversations[ConversationID].LastMessID = LastMessID;

                if (app.model.Conversations[ConversationID].FirstTimeLoaded)
                {
                    app.model.Conversations[ConversationID].FirstTimeLoaded = false;
                    app.model.Conversations[ConversationID].LastMediaID = LastMediaID;
                    app.model.Conversations[ConversationID].LastMediaIDBackup = LastMediaID;
                }

                app.model.Conversations[ConversationID].LastAttachmentID = LastAttachmentID;
                app.model.Conversations[ConversationID].ConversationName = ConversationName;
                app.model.Conversations[ConversationID].Members = Members.ToList();


                byte[] bytes = BitConverter.GetBytes(BubbleColor);
                app.model.Conversations[ConversationID].Color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
                ChatPage.Instance.bubbleColor = app.model.Conversations[ConversationID].Color;

                //if (app.model.MediaWindows.ContainsKey(ConversationID)
                //&& app.model.MediaWindows[ConversationID] != null)
                //{
                //    app.model.MediaWindows[ConversationID].Close();
                //    app.model.MediaWindows[ConversationID] = null;
                //}
                ChatPage.Instance.LoadMessages(ConversationID, true);
                //MainWindow.Instance.MediaPlayerWindow.MediaPlayer.Clean();

                Console.WriteLine("Conversation load");

                if (LastActive > 0)
                {
                    string active = "Active ";
                    string timeUnit = "minute";

                    if (LastActive > 1) timeUnit += "s";

                    if (LastActive > 59)
                    {
                        timeUnit = "hour";
                        LastActive /= 60;

                        if (LastActive > 1) timeUnit += "s";
                    }

                    active += LastActive + " " + timeUnit + " ago";
                    ChatPage.Instance.LastActive.Text = active;
                    ChatPage.Instance.Avatar.IsOnline = false;
                }
                else
                {
                    ChatPage.Instance.LastActive.Text = "Active Now";
                    ChatPage.Instance.Avatar.IsOnline = true;
                }
            });
        }
    }
}
