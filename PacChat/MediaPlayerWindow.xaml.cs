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
using System.Windows.Shapes;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for MediaPlayerWindow.xaml
    /// </summary>
    public partial class MediaPlayerWindow : Window
    {
        public MediaPlayerWindow()
        {
            InitializeComponent();
            MediaPlayer.BtnClose.Click += BtnClose_Click;
            MediaPlayer.TitleBar.MouseMove += FormDrag;
            MediaPlayer.BtnFullScreen.Click += BtnFullScreen_Click;
        }

        private void BtnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            isMaximized = !isMaximized;
        }

        private bool _isMaximized;
        public bool isMaximized
        {
            get
            {
                return _isMaximized;
            }
            set
            {
                _isMaximized = value;
                this.WindowState = _isMaximized == false ? WindowState.Normal : WindowState.Maximized;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void FormDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                }
                catch (Exception) { }
            }
        }
    }
}
