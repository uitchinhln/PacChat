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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isPanelOpened = false;
        private List<Button> _panelButtons = new List<Button>();

        private bool _isMaximized;
        public bool isMaximized
        {
            get
            {
                return _isMaximized;
            }
            set
            {
                Application.Current.MainWindow.WindowState = isMaximized ? WindowState.Normal : WindowState.Maximized;
                _isMaximized = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void MetaDataInit()
        {
        }

        private void FormDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ToggleLeftSidePanel()
        {
            var sb = this.FindResource("left-side-panel-" + (_isPanelOpened ? "compress" : "expand")) as Storyboard;
            sb.Begin();
            _isPanelOpened = !_isPanelOpened;
        }

        private void BtnNoti_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            isMaximized = true;
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleLeftSidePanel();
        }
    }
}
