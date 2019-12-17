using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace PacChat.Resources.CustomControls.Notifications
{
    /// <summary>
    /// Interaction logic for DownloadProgressNoti.xaml
    /// </summary>
    public partial class DownloadProgressNoti : UserControl
    {
        public string FileLocation { get; set; }

        public DownloadProgressNoti()
        {
            InitializeComponent();
        }

        public void SetFileName(string name)
        {
            FileName.Text = name;
        }

        public void SetProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            int value = e.ProgressPercentage;
            ProgressBar.Value = value;
            DownloadStatus.Text = value + "%";
        }

        public void FinalizeDownload(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar.Value = 100;
            ProgressBar.Visibility = Visibility.Collapsed;
            DownloadStatus.Text = "Download Completed";
        }
        
        public void DownloadError()
        {
            ProgressBar.Value = 0;
            ProgressBar.Visibility = Visibility.Collapsed;
            DownloadStatus.Text = "Download Error";
        }

        private void ClickMask_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(FileLocation))
            {
                Process proc = Process.Start(FileLocation);
            }
        }
    }
}
