using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class GetMediaFromConversationResult : IPacket
    {
        public List<string> FileIDs { get; set; } = new List<string>();
        public List<string> FileNames { get; set; } = new List<string>();
        public List<int> Positions { get; set; } = new List<int>();

        public void Decode(IByteBuffer buffer)
        {
            string id = ByteBufUtils.ReadUTF8(buffer);
            while (!id.Equals("~"))
            {
                FileIDs.Add(id);
                FileNames.Add(ByteBufUtils.ReadUTF8(buffer));
                Positions.Add(buffer.ReadInt());
                id = ByteBufUtils.ReadUTF8(buffer);
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine("New media player");
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow.Instance.MediaPlayerWindow = new MediaPlayerWindow();
                var mediaPlayer = MainWindow.Instance.MediaPlayerWindow;
                var app = MainWindow.chatApplication;

                for (int i = 0; i < FileIDs.Count; ++i)
                {
                    mediaPlayer.MediaPlayer.AddMediaItem
                    (
                        conversationID: app.model.currentSelectedConversation,
                        fileID: FileIDs[i],
                        fileName: FileNames[i],
                        position: Positions[i],
                        reachedRight: app.model.Conversations[app.model.currentSelectedConversation].LastMediaID <= 0
                    );
                    Console.WriteLine("Added");
                }

                MainWindow.Instance.MediaPlayerWindow.MediaPlayer.ShowMedia(FileIDs[0]); 
                MainWindow.Instance.MediaPlayerWindow.MediaPlayer.ShowMedia(app.model.currentMediaFileID);
                MainWindow.Instance.MediaPlayerWindow.ShowDialog();
            });
        }
    }
}
