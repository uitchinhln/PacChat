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
        private Dictionary<int, TabStickerStore> CateStore;

        private App app;


        public TabStickerContainner()
        {
            InitializeComponent();
            CateStore = new Dictionary<int, TabStickerStore>();
        }


        public void AddTabSticker(MessageCore.Sticker.StickerCategory cate)
        {
            StickerAPI.DownloadImage(cate.IconURL, (imageCateIcon) => 
            {
                TabItem a = new TabItem
                {
                    Width = 38,
                    Height = 38,
                    Content = new TabSticker(cate, ChatPage.Instance),
                    ToolTip = cate.Name
                };
                Image img = new Image();
                img.Source = imageCateIcon;
                a.Header = img;
                //a.Header = imageCateIcon;
                Console.WriteLine(cate.ID);
                tabCrlSticker.Items.Add(a);
            });

        }

        public void AddCateIntoStore(MessageCore.Sticker.StickerCategory cate)
        {
            TabStickerStore store = new TabStickerStore(cate);
            store.Margin = new Thickness(5, 5, 5, 5);
            CateStore.Add(cate.ID, store);
            splStickerStore.Children.Add(store);
        }

        public void RemoveCateInStore(MessageCore.Sticker.StickerCategory cate)
        {
            splStickerStore.Children.Remove(CateStore[cate.ID]);
        }

        private void loadStore()
        {
            int i = 0;
            foreach (var x in MessageCore.Sticker.Sticker.LoadedCategories)
            {
                ++i;
                TabStickerStore store = new TabStickerStore(x.Value);
                store.Margin = new Thickness(5, 5, 5, 5);
                splStickerStore.Children.Add(store);
                if (i >= 10) break;
            }
        }
    }
}
