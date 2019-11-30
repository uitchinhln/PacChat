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
using PacChat.Resources.CustomControls;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for TestWindows.xaml
    /// </summary>
    public partial class TestWindows : Window
    {


        public TestWindows()
        {
            InitializeComponent();
            ImageContainner img = new ImageContainner(1, "D:/PacChat/PacChat/Resources/Drawable/hinh3.png");
            test.Children.Add(img);
        }



    }
}
