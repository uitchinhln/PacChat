using PacChat.Resources.CustomControls.Notifications;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public bool Visible { get; private set; }

        public DownloadWindow()
        {
            InitializeComponent();
            TitleBar.MouseMove += FormDrag;
            Visible = false;
        }

        public void ShowPopUp()
        {
            if (Visibility != Visibility.Visible) Visibility = Visibility.Visible;
            Visible = true;
            var sb = this.FindResource("entrance") as Storyboard;
            sb.Begin();
        }

        public void HidePopUp()
        {
            Visible = false;
            var sb = this.FindResource("exit") as Storyboard;
            sb.Begin();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            HidePopUp();
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
