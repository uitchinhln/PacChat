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
    /// Interaction logic for ImageContainner.xaml
    /// </summary>
    public partial class ImageContainner : UserControl
    {
        private int _id;

        private double _height;

        private double _width;

        private double _thumbHeight;

        private double _thumbWidth;



        public ImageContainner()
        {
            InitializeComponent();
        }
    }
}
