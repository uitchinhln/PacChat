using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for AvatarDisplayer.xaml
    /// </summary>
    public partial class AvatarDisplayer : UserControl
    {
        public AvatarDisplayer()
        {
            InitializeComponent();
        }

        public Border AvatarBorder { get => AvaBorder; }

        public String ImageSource
        {
            get { return (String)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(String), typeof(AvatarDisplayer), new PropertyMetadata(String.Empty));



        public bool IsOnline
        {
            get { return (bool)GetValue(IsOnlineProperty); }
            set { SetValue(IsOnlineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOnline.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOnlineProperty =
            DependencyProperty.Register("IsOnline", typeof(bool), typeof(AvatarDisplayer), new PropertyMetadata(false));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ImageSourceProperty && !String.IsNullOrEmpty(ImageSource))
            {
                try
                {
                    LoadingAhihi.Visibility = Visibility.Visible;
                    ImageAva.ImageSource = new BitmapImage(new Uri(ImageSource, UriKind.RelativeOrAbsolute));
                    LoadingAhihi.Visibility = Visibility.Hidden;
                } catch
                {

                }
            }

            if (e.Property == IsOnlineProperty)
            {
                if (IsOnline)
                {
                    OnlineDot.Visibility = Visibility.Visible;
                } else
                {
                    OnlineDot.Visibility = Visibility.Hidden;
                }
            }

            base.OnPropertyChanged(e);
        }





        #region Custom ClickEvent
        public event EventHandler Click;

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
        #endregion
    }
}
