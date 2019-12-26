using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using MaterialDesignThemes.Wpf;
using PacChat.Resources.CustomControls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Profile
{
    public class ModifyPasswordResult : IPacket
    {
        public bool Result { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Result = buffer.ReadBoolean();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Result)
                {
                    AnnouncementDialog dialog = new AnnouncementDialog("Modify password successfully");
                    DialogHost.Show(dialog);
                    Console.WriteLine("Successfully modify password");
                }
                else
                {
                    AnnouncementDialog dialog = new AnnouncementDialog("Current password does not match. Modification failed.");
                    DialogHost.Show(dialog);
                    Console.WriteLine("UnSuccessfully modify password");
                }
            });
        }
    }
}
