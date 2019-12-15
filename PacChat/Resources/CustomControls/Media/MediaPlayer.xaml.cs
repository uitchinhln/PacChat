using MaterialDesignThemes.Wpf;
using PacChat.Cache.Core;
using PacChat.Network.RestAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PacChat.Resources.CustomControls.Media
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        private LRUCache<String, BitmapImage> ImgCache = new LRUCache<string, BitmapImage>(40, 2);

        public MediaPlayer()
        {
            InitializeComponent();

            #region Run Demo
            Demo();
            #endregion
        }

        int borderRight = int.MaxValue;
        int loadIndex = 0;
        public bool IsReachedRight { get; set; } = false;

        int borderLeft = 0;
        int leftIndex = 0;
        public bool IsReachedLeft { get; set; } = false;

        ThumbnailButton currentBtn;

        List<MediaInfo> loadedMediaInfo = new List<MediaInfo>();

        private void BtnClick(object sender, EventArgs e)
        {
            SwapToBtn(sender as ThumbnailButton);
        }

        private void SwapToBtn(ThumbnailButton btn)
        {
            if (btn == currentBtn) return;

            if (currentBtn != null)
            {
                currentBtn.IsActive = false;
            }
            currentBtn = btn;

            currentBtn.IsActive = true;
            if (currentBtn.Image != null)
                SetBackground(currentBtn.Image);
            else
            {
                WebClient wc = new WebClient();
                BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(currentBtn.ThumbnailUrl)));
                SetBackground(bitmap);
            }

            if (PacPlayer.SupportedExtensions.Contains(System.IO.Path.GetExtension(currentBtn.FileName).ToLower()))
            {
                ShowVideo(currentBtn.StreamURL);
            }
            else
            {
                ShowImage(currentBtn.StreamURL);
            }
        }

        private void SetBackground(ImageSource bitmap)
        {
            VisualBrush vb = new VisualBrush();
            Image im = new Image();
            BlurEffect ef = new BlurEffect();
            ef.Radius = 40;
            im.Source = bitmap;
            im.Effect = ef;

            vb.Visual = im;
            vb.Stretch = Stretch.UniformToFill;
            vb.Viewbox = new Rect(0.05, 0.05, 0.9, 0.9);
            PlayerBackground.Background = vb;
        }

        public void Clean(bool hard = true)
        {
            if (hard)            
                loadedMediaInfo.Clear();
            Gallery.Children.Clear();
            ImgCache.Clear();

            ImgFull.Source = null;
            ImgFull.IsEnabled = false;

            VideoFull.Close();

            currentBtn = null;
            loadIndex = 0;
            leftIndex = 0;
        }

        public void ShowVideo(String streamURL)
        {
            try
            {
                VideoFull.Close();

                VideoFull.Visibility = Visibility.Visible;
                ImgFull.Visibility = Visibility.Hidden;

                LoadingAhihi.Visibility = Visibility.Visible;

                VideoFull.Source = new Uri(streamURL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ShowImage(String imageURL)
        {
            try
            {
                LoadingAhihi.Visibility = Visibility.Visible;
                VideoFull.Close();

                VideoFull.Visibility = Visibility.Hidden;
                ImgFull.Visibility = Visibility.Visible;

                if (ImgCache.Contains(imageURL))
                {
                    ImgFull.Source = ImgCache.Get(imageURL);
                } else
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(imageURL));
                    ImgCache.AddReplace(imageURL, bitmap);
                    ImgFull.Source = bitmap;
                }

                LoadingAhihi.Visibility = Visibility.Hidden;
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadThumbnail(MediaInfo media, int index, bool swapTo = false)
        {
            ThumbnailButton btn = new ThumbnailButton(media);
            btn.Click += BtnClick;
            if (index > Gallery.Children.Count-1)
            {
                Gallery.Children.Add(btn);
                loadIndex++;
            } else
            {
                Gallery.Children.Insert(index, btn);
                leftIndex--;
            }

            if (swapTo)
            {
                SwapToBtn(btn);
            }
        }

        public void AddMediaItem(String conversationID, String fileID, String fileName, int position, bool reachedRight = false)
        {
            String thumbUrl = StreamAPI.GetMediaThumbnailURL(fileID, conversationID);
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);

            MediaInfo media = new MediaInfo(thumbUrl, streamUrl, fileName, fileID);
            loadedMediaInfo.Add(media);
            IsReachedRight = reachedRight;
            borderRight = Math.Min(position, borderRight);
        }

        public void AddMediaItemToFirst(String conversationID, String fileID, String fileName, int position, bool reachedLeft = true)
        {
            String thumbUrl = StreamAPI.GetMediaThumbnailURL(fileID, conversationID);
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);

            MediaInfo media = new MediaInfo(thumbUrl, streamUrl, fileName, fileID);
            loadedMediaInfo.Insert(0, media);

            loadIndex++;
            leftIndex++;
            IsReachedLeft = reachedLeft;
            borderLeft = Math.Max(position, borderLeft);
        }

        public void ShowMedia(String fileID)
        {
            int index = loadedMediaInfo.FindIndex(p => p.FileID.Equals(fileID, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
                throw new KeyNotFoundException();

            int start = Math.Max(0, index - 10);
            int end = Math.Max(loadedMediaInfo.Count, index + 10);

            this.Clean(false);

            for (int i = start; i < end; i++)
            {
                LoadThumbnail(loadedMediaInfo[i], int.MaxValue, i == index);
            }

            loadIndex = end-1;
            leftIndex = start;
        }

        #region Horizontal Support
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }
        #endregion

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.HorizontalOffset == scrollViewer.ScrollableWidth && loadedMediaInfo.Count > 0)
            {
                if (loadIndex < loadedMediaInfo.Count)
                {
                    for (int i = 0; i < 5  && loadIndex < loadedMediaInfo.Count; i++)
                    {
                        this.LoadThumbnail(loadedMediaInfo[loadIndex], int.MaxValue);
                    }
                } else if (!IsReachedRight)
                {
                    //Request data here
                }
            }
            if (scrollViewer.HorizontalOffset == 0 && loadedMediaInfo.Count > 0)
            {
                if (leftIndex > 0)
                {
                    int temp = leftIndex;
                    for (int i = Math.Max(0, temp - 5); i < temp; i++)
                    {
                        this.LoadThumbnail(loadedMediaInfo[i], 0);
                    }
                }
                else if (!IsReachedLeft)
                {
                    //Request data here
                }
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if (currentBtn == null) return;
            String dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PacChat/Downloads");
            Directory.CreateDirectory(dir);
            String savePath = System.IO.Path.Combine(dir, currentBtn.FileName);

            FileAPI.DownloadMedia(currentBtn.StreamURL, savePath, null, null, null);
        }

        private void BtnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (FullScreenIcon.Kind == PackIconKind.Fullscreen)
            {
                FullScreenIcon.Kind = PackIconKind.FullscreenExit;
            } else
            {
                FullScreenIcon.Kind = PackIconKind.Fullscreen;
            }
        }

        private void Demo()
        {
            ThumbnailButton btn = new ThumbnailButton()
            {
                ThumbnailUrl = "https://smaller-pictures.appspot.com/images/dreamstime_xxl_65780868_small.jpg",
                StreamURL = "https://smaller-pictures.appspot.com/images/dreamstime_xxl_65780868_small.jpg",
                FileName = "dreamstime_xxl_65780868_small.jpg",
            };
            btn.Click += BtnClick;
            Gallery.Children.Add(btn);

            ThumbnailButton btn2 = new ThumbnailButton()
            {
                ThumbnailUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/images/BigBuckBunny.jpg",
                StreamURL = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
                FileName = "BigBuckBunny.mp4",
            };
            btn2.Click += BtnClick;
            Gallery.Children.Add(btn2);

            ThumbnailButton btn3 = new ThumbnailButton()
            {
                ThumbnailUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/images/ElephantsDream.jpg",
                StreamURL = "G:/Downloads/Ahihi.mp4",
                FileName = "Ahihi.mp4",
            };
            btn3.Click += BtnClick;
            Gallery.Children.Add(btn3);

            ThumbnailButton btn4 = new ThumbnailButton()
            {
                ThumbnailUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/images/ForBiggerBlazes.jpg",
                StreamURL = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4",
                FileName = "ForBiggerBlazes.mp4",
            };
            btn4.Click += BtnClick;
            Gallery.Children.Add(btn4);

            ThumbnailButton btn5 = new ThumbnailButton()
            {
                ThumbnailUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/images/ForBiggerEscapes.jpg",
                StreamURL = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
                FileName = "ForBiggerEscapes.mp4",
            };
            btn5.Click += BtnClick;
            Gallery.Children.Add(btn5);

            for (int i = 0; i < 15; i++)
            {
                ThumbnailButton btn6 = new ThumbnailButton()
                {
                    ThumbnailUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/images/VolkswagenGTIReview.jpg",
                    StreamURL = "G:/Downloads/Ahihi.mp4",
                    FileName = "VolkswagenGTIReview.mp4",
                };
                btn6.Click += BtnClick;
                Gallery.Children.Add(btn6);
            }

            SwapToBtn(btn);
        }
    }

    public class MediaInfo
    {
        public String ThumbURL { get; set; }
        public String StreamURL { get; set; }
        public String FileName { get; set; }
        public String FileID { get; set; }

        public MediaInfo()
        {

        }

        public MediaInfo(String thumbUrl, String streamUrl, String fileName, String fileID)
        {
            ThumbURL = thumbUrl;
            StreamURL = streamUrl;
            FileName = fileName;
            FileID = fileID;
        }
    }
}
