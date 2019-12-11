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
    /// Interaction logic for ThumbnailItem.xaml
    /// </summary>
    public partial class ThumbnailButton : UserControl
    {
        public ThumbnailButton()
        {
            InitializeComponent();

        }
        
        public String FileName { get; private set; }

        public String StreamURL { get; private set; }

        public String ThumbnailUrl
        {
            get { return (String)GetValue(ThumbnailURLProperty); }
            set { SetValue(ThumbnailURLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbnailURLProperty =
            DependencyProperty.Register("ThumbnailUrl", typeof(String), typeof(ThumbnailButton), new PropertyMetadata(String.Empty));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ThumbnailURLProperty && !String.IsNullOrEmpty(ThumbnailUrl))
            {
                String url = ThumbnailUrl;
                Task task = new Task(() =>
                {
                    WebClient wc = new WebClient();
                    BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(url)));
                    Application.Current.Dispatcher.Invoke(() => ImgThumbnail.Source = bitmap);
                });
                task.Start();
            }

            base.OnPropertyChanged(e);
        }
    }
}
