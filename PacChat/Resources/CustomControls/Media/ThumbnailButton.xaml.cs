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

        #region Add Click Event
        static ThumbnailButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThumbnailButton), new FrameworkPropertyMetadata(typeof(ThumbnailButton)));

        }
        public static RoutedEvent ClickEvent =
        EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ThumbnailButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);

            RaiseEvent(args);

        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            OnClick();
        }
        #endregion

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
                WebClient wc = new WebClient();
                BitmapFrame bitmap = BitmapFrame.Create(new MemoryStream(wc.DownloadData(ThumbnailUrl)));
                ImgThumbnail.Source = bitmap;
            }

            base.OnPropertyChanged(e);
        }
    }
}
