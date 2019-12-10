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

        public String FileID { get; private set; }

        public String SourceUrl
        {
            get { return (String)GetValue(SourceUrlProperty); }
            set { SetValue(SourceUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceUrlProperty =
            DependencyProperty.Register("SourceUrl", typeof(String), typeof(ThumbnailButton), new PropertyMetadata(String.Empty));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == SourceUrlProperty && !String.IsNullOrEmpty(SourceUrl))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(SourceUrl, UriKind.Absolute);
                bitmap.EndInit();

                Thumbnail.Source = bitmap;
            }

            base.OnPropertyChanged(e);
        }
    }
}
