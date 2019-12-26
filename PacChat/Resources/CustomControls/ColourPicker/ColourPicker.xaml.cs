using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace PacChat.Resources.CustomControls.ColourPicker
{

    public partial class ColourPicker : UserControl
    {
        public ColourPicker()
        {
            InitializeComponent();
            
        }

        public delegate void ClickHander(Color color);
        public event ClickHander buttonClick;

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            buttonClick(ColorPicker.Color);

        }


        public Color GetColor()
        {
            return ColorPicker.Color;
        }
    }
}

