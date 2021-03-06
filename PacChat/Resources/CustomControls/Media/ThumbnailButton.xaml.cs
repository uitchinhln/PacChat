﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls.Media
{
    /// <summary>
    /// Interaction logic for ThumbnailItem.xaml
    /// </summary>
    public partial class ThumbnailButton : UserControl
    {
        public ThumbnailButton()
        {
            InitializeComponent();
            this.OpacityMask.Opacity = 0.5;
        }

        public ThumbnailButton(MediaInfo mediaInfo) :this()
        {
            if (mediaInfo == null)
                throw new NullReferenceException("MediaInfo cannot be null.");
            this.FileName = mediaInfo.FileName;
            this.FileID = mediaInfo.FileID;
            this.StreamURL = mediaInfo.StreamURL;
            this.ThumbnailUrl = mediaInfo.ThumbURL;
        }

        public ImageSource Image { get => ImgThumbnail.Source; }

        string fileName;
        bool isVideo = false;

        public String FileName 
        { 
            get => fileName; 
            set {
                fileName = value;
                isVideo = PacPlayer.IsSupport(fileName);
            } 
        }
        public String FileID { get; set; }

        public String StreamURL { get; set; }

        #region Custom Property
        public String ThumbnailUrl
        {
            get { return (String)GetValue(ThumbnailURLProperty); }
            set { SetValue(ThumbnailURLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbnailURLProperty =
            DependencyProperty.Register("ThumbnailUrl", typeof(String), typeof(ThumbnailButton), new PropertyMetadata(String.Empty));



        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(ThumbnailButton), new PropertyMetadata(false));

        #endregion

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ThumbnailURLProperty && !String.IsNullOrEmpty(ThumbnailUrl))
            {
                String url = ThumbnailUrl;
                Task task = new Task(() =>
                {
                    try
                    {
                        WebClient wc = new WebClient();
                        BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(url)));
                        wc.Dispose();
                        Application.Current.Dispatcher.Invoke(() => {
                            ImgThumbnail.Source = bitmap;

                            ImgThumbnail.Width = ImgThumbnail.ActualHeight;
                            ImgThumbnail.Height = ImgThumbnail.ActualWidth;

                            Gat.Children.Remove(LoadingAhihi);
                        });
                    }
                    catch (WebException)
                    {

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
                task.Start();
                if (isVideo)
                    PlayIcon.Visibility = Visibility.Visible;
            }

            if (e.Property == IsActiveProperty)
            {
                if (IsActive)
                {
                    this.OpacityMask.Opacity = 1;
                    PlayIcon.Visibility = Visibility.Hidden;
                } else
                {
                    this.OpacityMask.Opacity = 0.5;
                    if (isVideo)
                        PlayIcon.Visibility = Visibility.Visible;
                }
            }

            base.OnPropertyChanged(e);
        }

        public event EventHandler Click;

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
    }
}
