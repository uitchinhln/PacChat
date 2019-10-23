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
        private int _panelMaxSize, _panelMinSize;
        private int _panelOffset;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void MetaDataInit()
        {
            _panelMinSize = 32;
            _panelOffset = 32;

            _panelMaxSize = 128;
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
            ToggleLeftSidePanel();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            ToggleLeftSidePanel();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            ToggleLeftSidePanel();
        }
    }
}
