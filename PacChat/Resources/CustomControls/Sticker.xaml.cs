﻿using System;
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
using PacChat.MessageCore.Message;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl
    {

        public Sticker(ChatPage chatpage, bool clickable, int id, int cateid, int size, int duration, string urisheet, bool runWhenLoaded)
        {
            InitializeComponent();
            
            ID = id;
            CateID = cateid;
            Size = size;
            Duration = duration;
            UriSheet = urisheet;
            Chatpage = chatpage;
            Clickable = clickable;
            IsRunWhenLoaded = runWhenLoaded;
        }

        public bool IsRunWhenLoaded { get; set; }

        public ChatPage Chatpage { get; set; }
        
        public bool Clickable { get; set; }


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
            if (UriSheet != null)
            {
                progressBar.Visibility = Visibility.Visible;
                StickerAPI.DownloadImage(UriSheet, (stickerSheet) =>
                {
                    anim.ImageSource = stickerSheet;
                    anim.VerticalOffset = Size;
                    anim.HorizontalOffset = Size;
                    anim.Clickable = Clickable;
                    anim.isRunWhenLoaded = IsRunWhenLoaded;
                    sticker.Children.Add(anim);
                    progressBar.Visibility = Visibility.Hidden;
                });
            }
            
        }

        private void sticker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Clickable)
            {
                ChatPage.Instance.spTabStickerContainner.AddStickerToRecentTab(MessageCore.Sticker.Sticker.LoadedStickers[ID]);
                ChatPage.Instance.SendMessage(new StickerMessage()
                {
                    Sticker = new MessageCore.Sticker.Sticker() 
                    { ID = ID, CategoryID = CateID, Duration = Duration, SpriteURL = UriSheet}
                });
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadSticker();
        }
    }
}
