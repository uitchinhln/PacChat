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
using Animator;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for TabSticker.xaml
    /// </summary>
    public partial class TabSticker : UserControl
    {
        public TabSticker()
        {
            InitializeComponent();
            Animator.Animator a = new Animator.Animator();
            Image imgae = new Image();
            a.countLimit = 3;
            a.ImageSource = new BitmapImage(new Uri("D:/sprite (1).png", UriKind.Absolute));
            a.HorizontalOffset = 130;
            a.VerticalOffset = 130;
            a.Interval = TimeSpan.FromSeconds(0.1);
            wplStickerContainner.Children.Add(a);
        }
    }
}
