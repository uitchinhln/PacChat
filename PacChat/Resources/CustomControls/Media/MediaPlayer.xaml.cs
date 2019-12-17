using MaterialDesignThemes.Wpf;
using PacChat.Cache.Core;
using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest.Message;
using PacChat.Network.RestAPI;
using PacChat.Resources.CustomControls.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
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
        private LRUCache<String, ImageSource> ImgCache = new LRUCache<string, ImageSource>(15, 2);

        public MediaPlayer()
        {
            InitializeComponent();

            #region Run Demo
            //Demo();
            #endregion
        }

        int borderRight = int.MaxValue;
        public bool IsReachedRight { get; set; } = false;
        public string ConversationID { get; set; }

        Thread imgThread, thumbThread;


        ThumbnailButton currentBtn;

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

            if (currentBtn.Image != null)
                SetBackground(currentBtn.Image);
            else
            {
                String url = currentBtn.ThumbnailUrl;
                if (thumbThread != null && thumbThread.IsAlive)
                    thumbThread.Abort();
                thumbThread = new Thread(() =>
                {
                    try
                    {
                        WebClient wc = new WebClient();
                        BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(url)));
                        wc.Dispose();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SetBackground(bitmap);
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
                thumbThread.Start();
            }

            if (PacPlayer.IsSupport(currentBtn.FileName))
            {
                ShowVideo(currentBtn.StreamURL);
            }
            else
            {
                ShowImage(currentBtn.StreamURL);
            }
            currentBtn.IsActive = true;
        }

        private void SetBackground(ImageSource bitmap)
        {
            VisualBrush vb = new VisualBrush();
            Image im = new Image();
            BlurEffect ef = new BlurEffect();
            ef.Radius = 30;
            im.Source = bitmap;
            im.Effect = ef;

            vb.Visual = im;
            vb.Stretch = Stretch.UniformToFill;
            vb.Viewbox = new Rect(0.05, 0.05, 0.9, 0.9);
            PlayerBackground.Background = vb;
        }

        public void Clean(bool hard = true)
        {
            Gallery.Children.Clear();
            ImgCache.Clear();

            ImgFull.Source = null;
            ImgFull.IsEnabled = false;

            VideoFull.Close();

            currentBtn = null;
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
                ImgFull.Visibility = Visibility.Hidden;

                if (ImgCache.Contains(imageURL))
                {
                    ImgFull.Source = ImgCache.Get(imageURL);

                    ImgFull.MaxHeight = ImgFull.Source.Height;
                    ImgFull.MaxWidth = ImgFull.Source.Width;

                    ImgFull.Visibility = Visibility.Visible;
                    LoadingAhihi.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (imgThread != null && imgThread.IsAlive)
                        imgThread.Abort();
                    imgThread = new Thread(() =>
                    {
                        try
                        {
                            WebClient wc = new WebClient();
                            BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(imageURL)));
                            wc.Dispose();
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                ImgCache.AddReplace(imageURL, bitmap);
                                ImgFull.Source = bitmap;

                                ImgFull.MaxHeight = bitmap.PixelHeight;
                                ImgFull.MaxWidth = bitmap.PixelWidth;
                                ImgFull.Visibility = Visibility.Visible;

                                LoadingAhihi.Visibility = Visibility.Hidden;
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    });
                    imgThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadThumbnail(MediaInfo media, int index, bool swapTo = false)
        {
            ThumbnailButton btn = new ThumbnailButton(media);

            btn.Click += BtnClick;
            if (index > Gallery.Children.Count - 1)
            {
                Gallery.Children.Add(btn);
            }
            else
            {
                Gallery.Children.Insert(index, btn);
            }

            if (swapTo)
            {
                SwapToBtn(btn);
            }
        }

        public void AddMediaItem(String conversationID, String fileID, String fileName, int position, bool reachedRight = false)
        {
            Console.WriteLine("reached right: " + reachedRight);
            foreach (var uiElement in Gallery.Children)
            {
                if (!(uiElement is ThumbnailButton)) continue;
                ThumbnailButton btn = uiElement as ThumbnailButton;
                if (btn.FileID.Equals(fileID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            String thumbUrl = StreamAPI.GetMediaThumbnailURL(fileID, conversationID);
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);

            MediaInfo media = new MediaInfo(thumbUrl, streamUrl, fileName, fileID);
            LoadThumbnail(media, int.MaxValue);

            IsReachedRight = reachedRight;
            borderRight = Math.Min(position, borderRight);
        }

        public void AddMediaItemToFirst(String conversationID, String fileID, String fileName, int position, bool reachedLeft = true)
        {
            foreach (var uiElement in Gallery.Children)
            {
                if (!(uiElement is ThumbnailButton)) continue;
                ThumbnailButton btn = uiElement as ThumbnailButton;
                if (btn.FileID.Equals(fileID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            String thumbUrl = StreamAPI.GetMediaThumbnailURL(fileID, conversationID);
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);

            MediaInfo media = new MediaInfo(thumbUrl, streamUrl, fileName, fileID);
            LoadThumbnail(media, 0);
        }

        public void ShowMedia(String fileID)
        {
            foreach (var uiElement in Gallery.Children)
            {
                if (!(uiElement is ThumbnailButton)) continue;
                ThumbnailButton btn = uiElement as ThumbnailButton;
                if (btn.FileID.Equals(fileID, StringComparison.OrdinalIgnoreCase))
                {
                    SwapToBtn(btn);
                    break;
                }
            }
            FillGallery();
        }

        public void FillGallery()
        {
            if (GalleryScroller.ComputedHorizontalScrollBarVisibility != Visibility.Visible && !IsReachedRight)
            {
                Console.WriteLine("Media conversation id: " + ConversationID);
                var app = MainWindow.chatApplication;
                if (!String.IsNullOrEmpty(ConversationID) && app.model.Conversations.ContainsKey(ConversationID) &&
                    app.model.Conversations[ConversationID] != null)
                    if (app.model.Conversations[ConversationID].LastMediaID >= 0)
                    {
                        GetMediaFromConversation packet = new GetMediaFromConversation();
                        packet.ConversationID = ConversationID;
                        packet.MediaPosition = app.model.Conversations[ConversationID].LastMediaID;
                        packet.Quantity = 15;
                        _ = ChatConnection.Instance.Send(packet);
                        app.model.Conversations[ConversationID].LastMediaID -= 15;
                    }
            }
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
            if (scrollViewer.HorizontalOffset == scrollViewer.ScrollableWidth)
            {
                if (!IsReachedRight)
                {
                    //Request data here
                    Console.WriteLine("Media conversation id: " + ConversationID);
                    var app = MainWindow.chatApplication;
                    if (!String.IsNullOrEmpty(ConversationID) && app.model.Conversations.ContainsKey(ConversationID) &&
                        app.model.Conversations[ConversationID] != null)
                        if (app.model.Conversations[ConversationID].LastMediaID >= 0)
                        {
                            GetMediaFromConversation packet = new GetMediaFromConversation();
                            packet.ConversationID = ConversationID;
                            packet.MediaPosition = app.model.Conversations[ConversationID].LastMediaID;
                            packet.Quantity = 5;
                            _ = ChatConnection.Instance.Send(packet);
                            app.model.Conversations[ConversationID].LastMediaID -= 5;
                        }
                }
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if (currentBtn == null) return;
            String dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PacChat/Downloads");
            Directory.CreateDirectory(dir);
            String savePath = System.IO.Path.Combine(dir, currentBtn.FileName);

            if (!MainWindow.Instance.DownloadWindow.Visible)
            {
                MainWindow.Instance.DownloadWindow.ShowPopUp();
            }

            DownloadProgressNoti noti = new DownloadProgressNoti();
            noti.SetFileName(currentBtn.FileName);
            MainWindow.Instance.DownloadWindow.DownloadList.Children.Add(noti);

            FileAPI.DownloadMedia(currentBtn.StreamURL, savePath,
                noti.SetProgress, noti.FinalizeDownload, null);
        }

        private void BtnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (FullScreenIcon.Kind == PackIconKind.Fullscreen)
            {
                FullScreenIcon.Kind = PackIconKind.FullscreenExit;
            }
            else
            {
                FullScreenIcon.Kind = PackIconKind.Fullscreen;
            }
        }

        public void Demo()
        {
            ThumbnailButton btn = new ThumbnailButton()
            {
                ThumbnailUrl = StreamAPI.GetMediaThumbnailURL("7ca94feb-4f57-4beb-8b6a-fe9225337794", "7516cdee-0971-472c-9a01-b2804dcedd9f"),
                StreamURL = StreamAPI.GetMediaURL("7ca94feb-4f57-4beb-8b6a-fe9225337794", "7516cdee-0971-472c-9a01-b2804dcedd9f"),
                FileName = "dreamstime_xxl_65780868_small.jpg",
                FileID = "7ca94feb-4f57-4beb-8b6a-fe9225337794"
            };
            btn.Click += BtnClick;
            Gallery.Children.Add(btn);
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
