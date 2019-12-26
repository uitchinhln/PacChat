using PacChat.Resources.CustomControls.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static DownloadWindow _instance;

        private DownloadWindow()
        {
            InitializeComponent();
            TitleBar.MouseMove += FormDrag;
            Visible = false;
        }

        private void ShowPopUp()
        {
            if (Visibility != Visibility.Visible) Visibility = Visibility.Visible;
            Visible = true;
            var sb = this.FindResource("entrance") as Storyboard;
            sb.Begin();
        }

        private void HidePopUp()
        {
            Visible = false;
            var sb = this.FindResource("exit") as Storyboard;
            sb.Begin();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void LoadData()
        {
            var app = MainWindow.chatApplication;
            foreach (var item in app.model.DownloadProgresses)
            {
                this.DownloadList.Children.Insert(0, item);
            }
        }

        public static void ShowPopup()
        {
            if (_instance == null || !Application.Current.Windows.OfType<DownloadWindow>().Any())
            {
                _instance = new DownloadWindow();
                _instance.LoadData();
            }
            if (!_instance.Visible)
            {
                _instance.ShowPopUp();
            }
            _instance.Activate();
        }

        public static DownloadWindow Instance
        {
            get
            {
                ShowPopup();
                return _instance;
            }
        }

        public bool IsDownloading(string fileID)
        {
            DownloadProgressNoti noti = DownloadList.Children.OfType<DownloadProgressNoti>()
                .Where(p => !p.IsCompleted && p.FileID.Equals(fileID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return noti != null;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.HidePopUp();
            this.DownloadList.Children.RemoveRange(0, this.DownloadList.Children.Count);
            base.OnClosing(e);
        }
    }
}
