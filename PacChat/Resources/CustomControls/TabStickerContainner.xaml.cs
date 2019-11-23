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

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for TabStickerContainner.xaml
    /// </summary>
    public partial class TabStickerContainner : UserControl
    {
        public TabStickerContainner()
        {
            InitializeComponent();
            addStickerTab();
            addStickerTab();
            addStickerTab();
        }


        private void addStickerTab()
        {
            PackIcon icon = new PackIcon();
            icon.Kind = PackIconKind.Smiley;
            TabItem a = new TabItem
            {
                Width = 50,
                Height = 30,
                Header = icon,
                Content = new TabSticker(),
            };
            
            tabCrlSticker.Items.Add(a);
        }
    }
}
