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
    public class GetSelfProfileResult : IPacket
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Town { get; set; } = "Default";
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }


        public void Decode(IByteBuffer buffer)
        {
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            Town = ByteBufUtils.ReadUTF8(buffer);
            Email = ByteBufUtils.ReadUTF8(buffer);
            DateOfBirth = DateTime.Parse(ByteBufUtils.ReadUTF8(buffer));
            Gender = (Gender)buffer.ReadInt();

            // Avatar ID
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // Display information on setting tab
            Application.Current.Dispatcher.Invoke(() =>
            {
                var setting = SettingPage.Instance;
                setting.FirstNameInp.Text = FirstName;
                setting.LastNameInp.Text = LastName;
                setting.Email.Text = Email;
                setting.BirthdayInp.SelectedDate = DateOfBirth;
                setting.GenderInp.SelectedIndex = (int)Gender - 1;
                setting.AddressInp.Text = Town;

                MainWindow.Instance.ProfileContext.FullName.Content = FirstName + " " + LastName;
                // Bind to Avatar displayer
            });
        }
    }
}
