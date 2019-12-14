using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PacChat.Network.RestAPI;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl
    {

        public Sticker(ChatPage chatpage, bool clickable, int id, int cateid, int size, int duration, string urisheet)
        {
            InitializeComponent();

            ID = id;
            CateID = cateid;
            Size = size;
            Duration = duration;
            UriSheet = urisheet;
            Chatpage = chatpage;
            Clickable = clickable;
            
            loadSticker();
        }

        public ChatPage Chatpage
        {
            get; set;
        }
        
        public bool Clickable
        {
            get; set;
        }


        public int ID
        {
            get; set;
        }

        public int CateID
        {
            get; set;
        }

        public int Size
        {
            get; set;
        }

        public int Duration
        {
            get; set;
        }

        public string UriSheet
        {
            get; set;
        }

        private void loadSticker()
        {
            Animator.Animator anim = new Animator.Animator();
            anim.CountLimit = 10;
            anim.Interval = TimeSpan.FromSeconds((double)Duration / 1000);

            StickerAPI.DownloadImage(UriSheet, (stickerSheet) =>
            {
                //Console.WriteLine(stickerSheet.PixelWidth);
                anim.ImageSource = stickerSheet;
                anim.VerticalOffset = Size;
                anim.HorizontalOffset = Size;
                anim.Clickable = Clickable;
                sticker.Children.Add(anim);
            });
            
        }

        private void sticker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Clickable)
            {
                Chatpage.sendSticker(false, ID, CateID, Size, Duration, UriSheet);
            }
        }
    }
}
