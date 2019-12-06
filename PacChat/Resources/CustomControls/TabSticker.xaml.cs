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
        public int CateID { get; set; }

        public string IconUri { get; set; }

        public string CateName { get; set; }

        public int TotalSticker { get; set; }

        public ChatPage Chatpage { get; set; }

        private List<int> _listStickerID;

        public TabSticker(int cateID, ChatPage chatpage)
        {
            InitializeComponent();
            CateID = cateID;
            Chatpage = chatpage;
            initTabSticker();

            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite.png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (1).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (2).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (3).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (4).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (5).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (6).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (7).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (8).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (9).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (10).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (11).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (12).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (13).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (14).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (15).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (16).png"));
            wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite (17).png"));
        }

        private void initListStickerID()
        {
            //lay cac sticker ID tu category ID
        }

        private void initTabSticker() // khoi tao tu cateID
        {

        }

        private void loadSticker()
        {
            foreach (var x in _listStickerID)
            {
                wplStickerContainner.Children.Add(new Sticker(Chatpage, true, 1, 1, 130, 100, "/PacChat;component/resources/drawable/sprite.png" + x.ToString() )); //lay info tu stickerID
            }
        }


    }
}
