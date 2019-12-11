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
                BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData("https://image.freepik.com/free-photo/image-human-brain_99433-298.jpg")));
                Application.Current.Dispatcher.Invoke(() => ImgFull.Source = bitmap);

            });
            task.Start();
        }
    }
}
