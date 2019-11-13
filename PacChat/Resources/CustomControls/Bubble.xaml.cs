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
    /// Interaction logic for Bubble.xaml
    /// </summary>
    public partial class Bubble : UserControl
    {

        public string Messages
        {
            get { return textBlock.Text.ToString(); }
            set { textBlock.Text = value.ToString(); }
        }

        public Bubble()
        {
            InitializeComponent();
        }

        public void SetText(string s)
        {
            textBlock.Text = s;
        }



        public void SetBG(Color x)
        {
            borderTextBlock.Background = new SolidColorBrush(x);
        }

        public void SetTextColor(Color x)
        {
            textBlock.Foreground = new SolidColorBrush(x);
        }

        public void SetSeen(bool seened)
        {
            if (seened) textBlockSeen.Visibility = Visibility.Visible;
            else textBlockSeen.Visibility = Visibility.Collapsed;
        }

        public void SetDirect(bool direct)
        {
            if (direct) //left
            {
                this.HorizontalAlignment = HorizontalAlignment.Left;
                Thickness margin = borderBubble.Margin;
                margin.Left = 3;
                margin.Right = 170;
                margin.Bottom = 2;
                borderBubble.Margin = margin;
                
            }
            else
            {
                this.HorizontalAlignment = HorizontalAlignment.Right;
                Thickness margin = borderBubble.Margin;
                margin.Left = 170;
                margin.Right = 15;
                margin.Bottom = 2;
                borderBubble.Margin = margin;
            }
        }

    }
}
