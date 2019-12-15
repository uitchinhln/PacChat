using PacChat.Resources.CustomControls.Media;
using System;
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

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for ThumbnailBubble.xaml
    /// </summary>
    public partial class ThumbnailBubble : UserControl
    {
        public ThumbnailBubble()
        {
            InitializeComponent();
        }

        public ThumbnailBubble(MediaInfo mediaInfo) : this()
        {
            if (mediaInfo == null)
                throw new NullReferenceException("MediaInfo cannot be null.");
            this.FileName = mediaInfo.FileName;
            this.FileID = mediaInfo.FileID;
            this.StreamURL = mediaInfo.StreamURL;
            this.ThumbnailUrl = mediaInfo.ThumbURL;
            this.IsVideoThumbnail = PacPlayer.IsSupport(FileName);
        }

        public ImageSource Image { get => MediaThumb.ImageSource; }

        public String FileName { get; set; }
        public String FileID { get; set; }

        public String StreamURL { get; set; }

        public bool IsVideoThumbnail { get; set; }

        #region Custom Property
        public String ThumbnailUrl
        {
            get { return (String)GetValue(ThumbnailURLProperty); }
            set { SetValue(ThumbnailURLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbnailURLProperty =
            DependencyProperty.Register("ThumbnailUrl", typeof(String), typeof(ThumbnailBubble), new PropertyMetadata(String.Empty));

        #endregion

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ThumbnailURLProperty && !String.IsNullOrEmpty(ThumbnailUrl))
            {
                LoadingAhihi.Visibility = Visibility.Visible;
                String url = IsVideoThumbnail ? ThumbnailUrl : StreamURL;
                Task task = new Task(() =>
                {
                    try
                    {
                        WebClient wc = new WebClient();
                        BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(url)));
                        wc.Dispose();
                        Application.Current.Dispatcher.Invoke(() =>
                        {

                            if (!IsVideoThumbnail)
                            {
                                if (bitmap.PixelHeight > 300)
                                {
                                    BubbleBkg.Height = 300;
                                    BubbleBkg.Width = bitmap.PixelWidth * 300 / bitmap.PixelHeight;
                                } else if (bitmap.PixelWidth > 500)
                                {
                                    BubbleBkg.Width = 500;
                                    BubbleBkg.Height = bitmap.PixelHeight * 500 / bitmap.PixelWidth;
                                }

                                MediaThumb.ImageSource = bitmap;
                            } else
                            {
                                PlayIcon.Visibility = Visibility.Visible;
                            }

                            LoadingAhihi.Visibility = Visibility.Hidden;
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
            }

            base.OnPropertyChanged(e);
        }

        public event EventHandler Click;

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        private void ClickMask_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Instance.MediaPlayerWindow != null)
            {
                MainWindow.Instance.MediaPlayerWindow.ShowDialog();
                MainWindow.Instance.MediaPlayerWindow.MediaPlayer.ShowMedia(FileID);
            }
        }
    }
}
