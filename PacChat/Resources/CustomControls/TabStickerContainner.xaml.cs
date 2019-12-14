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
using PacChat.Network.RestAPI;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for TabStickerContainner.xaml
    /// </summary>
    public partial class TabStickerContainner : UserControl
    {
        private List<int> _idList;

        private App app;

        public ChatPage Chatpage
        {
            get; set;
        }

        public List<int> IDList
        {
            get { return _idList; }
            set { _idList = value; }
        }


        public TabStickerContainner(ChatPage _chatpage)
        {
            InitializeComponent();
            Chatpage = _chatpage;

            addStickerTabDemo();


        }

       

        private void addStickerTabDemo()
        {
            TabItem a = new TabItem
            {
                Width = 38,
                Height = 38,
                //Content = new TabSticker(1, Chatpage),
                ToolTip = "Tiêm viêu",
            };
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/PacChat;component/resources/drawable/sprite_ic.png", UriKind.RelativeOrAbsolute));
            a.Header = img;

            
            tabCrlSticker.Items.Add(a);
        }

        public void AddTabSticker(MessageCore.Sticker.StickerCategory cate)
        {
            StickerAPI.DownloadImage(cate.IconURL, (imageCateIcon) => 
            {
                TabItem a = new TabItem
                {
                    Width = 38,
                    Height = 38,
                    Content = new TabSticker(cate, Chatpage),
                    ToolTip = cate.Name
                };
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(cate.IconURL));
                a.Header = img;
                //a.Header = imageCateIcon;
                Console.WriteLine(cate.ID);
                tabCrlSticker.Items.Add(a);
            });

            
        }

        private void initTabStickerContainner()
        {
            foreach (var x in _idList)
            {
                //initTabSticker(x);
            }
        }

        private void loadStore()
        {
            TabItem a = new TabItem
            {
                Width = 38,
                Height = 38,
                //Content = tempTabSticker,
            };
        }
    }
}
