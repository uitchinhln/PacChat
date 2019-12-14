using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PacChat.MessageCore.Sticker;
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
        public MessageCore.Sticker.StickerCategory Cate { get; set; }

        public ChatPage Chatpage { get; set; }

        public TabSticker(MessageCore.Sticker.StickerCategory cate, ChatPage chatpage)
        {
            InitializeComponent();
            Chatpage = chatpage;
            Cate = cate;
            initTabSticker();

        }

        private void initTabSticker() // khoi tao tu cateID
        {
            foreach(var x in Cate.Stickers)
            {
                wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, x.Duration, x.SpriteURL));
            }
        }


    }
}
