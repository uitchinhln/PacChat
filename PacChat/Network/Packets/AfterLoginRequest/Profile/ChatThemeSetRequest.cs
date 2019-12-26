using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PacChat.Network.Packets.AfterLoginRequest.Profile
{
    public class ChatThemeSetRequest : IPacket
    {
        #region Chat Background
        public String BackgroundId { get; set; } = "0";
        public int BackgroundBlur { get; set; } = 50;

        public int BackgroundColor { get; set; } = -1;

        public ChatBackgroundType UseType { get; set; } = 0;
        #endregion

        public int IconColor { get; set; } = -13882324;

        public void Decode(IByteBuffer buffer)
        {
            BackgroundId = ByteBufUtils.ReadUTF8(buffer);
            BackgroundBlur = buffer.ReadInt();

            BackgroundColor = buffer.ReadInt();
            UseType = (ChatBackgroundType) buffer.ReadInt();

            IconColor = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, BackgroundId);
            byteBuf.WriteInt(BackgroundBlur);

            byteBuf.WriteInt(BackgroundColor);

            byteBuf.WriteInt((int) UseType);

            byteBuf.WriteInt(IconColor);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            Console.WriteLine("ChatThemeSet packet received");
            Application.Current.Dispatcher.Invoke(() =>
            {
                SettingPage settingPage = SettingPage.Instance;

                try
                {
                    Guid test = Guid.Parse(BackgroundId);

                }
                catch
                {
                    try
                    {
                        String path = ResourceUtil.ChatPageResource[Convert.ToInt32(BackgroundId)];
                        if (!File.Exists(path)) return;

                        FileStream stream = null;
                        try
                        {
                            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = stream;
                            bitmap.EndInit();

                            settingPage.AddBGPreview(bitmap);
                        }
                        catch { throw; }
                        //finally { stream.Close(); }
                    }
                    catch { }
                }
                settingPage.blurLv.Value = BackgroundBlur;

                settingPage.BGColorPicker.ColorPicker.Color = ColorUtils.IntToColor(BackgroundColor);
                settingPage.iconColorPicker.ColorPicker.Color = ColorUtils.IntToColor(IconColor);

                ChatPage.Instance.ChangeIconColor(ColorUtils.IntToColor(IconColor));

                switch (UseType)
                {
                    case ChatBackgroundType.Color:
                        ChatPage.Instance.SetSolidBG(ColorUtils.IntToColor(BackgroundColor));
                        break;
                    case ChatBackgroundType.Image:
                        break;
                }
            });
        }
    }
}
