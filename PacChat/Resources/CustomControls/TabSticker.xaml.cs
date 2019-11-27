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
        private int _cateID;

        private string _iconUri;

        private string _cateName;

        private int _totalSticker;


        public TabSticker(int cateID)
        {
            InitializeComponent();
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite.png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (1).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (2).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (3).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (4).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (5).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (6).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (7).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (8).png"));
            wplStickerContainner.Children.Add(new Sticker(1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (9).png"));
        }

        public int CateID
        {
            get { return _cateID; }
            set { _cateID = value; }
        }

        public string IconUri
        {
            get { return _iconUri; }
            set { _iconUri = value; }
        }

        public string CateName
        {
            get { return _cateName; }
            set { _cateName = value; }
        }

        public int TotalSticker
        {
            get { return _totalSticker; }
            set { _totalSticker = value; }

        }
    }
}
