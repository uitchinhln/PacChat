using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Profile
{
    public class GetDisplayedProfileResult : IPacket
    {
        public string Name { get; set; }
        public string Town { get; set; } = "Default";
        public string DoB { get; set; }
        public string Email { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Name = ByteBufUtils.ReadUTF8(buffer);
            Email = ByteBufUtils.ReadUTF8(buffer);
            DoB = ByteBufUtils.ReadUTF8(buffer);
            Town = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine("Got profile");
            // Display profile of user
            Application.Current.Dispatcher.Invoke(() => 
            MainWindow.Instance.OpenProfileDisplayer(
                name: Name,
                email: Email,
                dob: DoB,
                address: Town));
        }
    }
}
