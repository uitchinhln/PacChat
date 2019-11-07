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
using MaterialDesignThemes.Wpf;
using PacChat.Resources.CustomControls;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _previous; // 0 = left, 1 = right
        private bool _new; // 0 = false, 1 = true
        private BubbleChat _previousChat;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaterialDesignThemes.Wpf.ShadowAssist.SetShadowDepth(this, ShadowDepth.Depth0);
            _new = true;
            _previous = true;

        }

        private void FormDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OnGotFocusHandler(object sender, RoutedEventArgs e)
        {
            Button tb = e.Source as Button;
            tb.Background = Brushes.Red;
        }
        private void OnLostFocusHandler(object sender, RoutedEventArgs e)
        {
            Button tb = e.Source as Button;
            tb.Background = Brushes.White;
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_previous == true)
            {
                _previousChat = new BubbleChat();
                spContent.Children.Add(_previousChat);
            }
                
            Bubble b = new Bubble();
            _previousChat.AddBubble(b);
            _previous = false;
            viewScroller.ScrollToEnd();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            spContent.Children.Add(new Bubble());
            _previous = true;
            viewScroller.ScrollToEnd();
        }
    }
}
