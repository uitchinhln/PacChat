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

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for ProfileContext.xaml
    /// </summary>
    public partial class ProfileContext : UserControl
    {
        public ProfileContext()
        {
            InitializeComponent();
            this.Avatar.Click += Avatar_Click;
            BtnDownload.Click += BtnDownload_Click;
            Avatar.AvaBackground.Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#FFF0F0F0");
        }

        private void Avatar_Click(object sender, EventArgs e)
        {
            SettingPage.Instance.SelfAvt.ChangeAva();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var downloadWindow = DownloadWindow.Instance;
        }
    }
}
