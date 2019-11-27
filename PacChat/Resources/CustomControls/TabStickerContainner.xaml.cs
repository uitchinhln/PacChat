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
        private List<int> _idList;
        public List<int> IDList
        {
            get { return _idList; }
            set { _idList = value; }
        }


        public TabStickerContainner()
        {
            InitializeComponent();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();
            addStickerTabDemo();

        }

       

        private void addStickerTabDemo()
        {
            TabItem a = new TabItem
            {
                Width = 38,
                Height = 38,
                Content = new TabSticker(1),
                ToolTip = "Tiêm viêu",
            };
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/PacChat;component/resources/drawable/sprite_ic.png", UriKind.RelativeOrAbsolute));
            a.Header = img;

            
            tabCrlSticker.Items.Add(a);
        }

        private void initTabSticker(int cateID)
        {
            TabSticker tempTabSticker = new TabSticker(1);
            string iconUri = tempTabSticker.IconUri;
            string name = tempTabSticker.Name;

            TabItem a = new TabItem
            {
                Width = 38,
                Height = 38,
                Content = tempTabSticker,
            };
            a.ToolTip = name;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(iconUri, UriKind.RelativeOrAbsolute));
            a.Header = img;
            tabCrlSticker.Items.Add(a);
        }

        private void initTabStickerContainner()
        {
            foreach (var x in _idList)
            {
                initTabSticker(x);
            }
        }
    }
}
