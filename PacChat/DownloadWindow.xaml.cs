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
        public DownloadWindow()
        {
            InitializeComponent();
            ShowPopUp();
        }

        public void ShowPopUp()
        {
            var sb = this.FindResource("entrance") as Storyboard;
            sb.Begin();
        }

        public void HidePopUp()
        {
            var sb = this.FindResource("exit") as Storyboard;
            sb.Begin();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            HidePopUp();
        }
    }
}
