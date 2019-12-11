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

namespace PacChat.Resources.CustomControls.Media
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        public MediaPlayer()
        {
            InitializeComponent();

            Task task = new Task(() =>
            {
                WebClient wc = new WebClient();
                BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData("https://smaller-pictures.appspot.com/images/dreamstime_xxl_65780868_small.jpg")));
                Application.Current.Dispatcher.Invoke(() => ImgFull.Source = bitmap);

            });
            task.Start();

            //ImgFull.Source = new BitmapImage(new Uri("https://i.imgur.com/i5xTVVY.jpg"));

            for (int i = 0; i < 10; i++)
            {
                Gallery.Children.Add(new ThumbnailButton() {
                    ThumbnailUrl = "https://smaller-pictures.appspot.com/images/dreamstime_xxl_65780868_small.jpg",
                    Margin = new Thickness()
                    {
                        Left = 5,
                        Right = 5
                    }
                });
            }
        }



        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }
    }
}
