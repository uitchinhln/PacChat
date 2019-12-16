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
        private Dictionary<int, TabStickerStore> cateStore;

        private Dictionary<int, MessageCore.Sticker.StickerCategory> cateStoreList;

        private Dictionary<int, MessageCore.Sticker.Sticker> recentStickerList;

        public TabStickerContainner()
        {
            InitializeComponent();
            cateStore = new Dictionary<int, TabStickerStore>();
            cateStoreList = new Dictionary<int, MessageCore.Sticker.StickerCategory>();
            recentStickerList = new Dictionary<int, MessageCore.Sticker.Sticker>(); 

        }
        
        #region Sticker tab

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
                tabCrlSticker.Items.Add(a);
            });

        }
        
        #endregion

        #region Sticker store tab
        public void AddToStoreList(MessageCore.Sticker.StickerCategory cate)
        {
            cateStoreList.Add(cate.ID, cate);
        }

        public void AddCateIntoStore(MessageCore.Sticker.StickerCategory cate)
        {
            TabStickerStore store = new TabStickerStore(cate);
            store.Margin = new Thickness(5, 5, 5, 5);
            cateStore.Add(cate.ID, store);
            splStickerStore.Children.Add(store);
        }

        public void RemoveCateInStore(MessageCore.Sticker.StickerCategory cate)
        {
            splStickerStore.Children.Remove(cateStore[cate.ID]);
        }

        #endregion

        #region Sticker recent tab

        public void AddStickerToRecentTabFromSV(MessageCore.Sticker.Sticker sticker)
        {
            if (recentStickerList.ContainsKey(sticker.ID)) return;

            wplStickerRecent.Children.Add( new Sticker(ChatPage.Instance, true, sticker.ID, sticker.CategoryID, 130, sticker.Duration, sticker.SpriteURL, false));
            recentStickerList.Add(sticker.ID, sticker);
        }
        
        public void AddStickerToRecentTab(MessageCore.Sticker.Sticker sticker)
        {
            if (recentStickerList.ContainsKey(sticker.ID)) return;
            
            wplStickerRecent.Children.Insert(0, new Sticker(ChatPage.Instance, true, sticker.ID, sticker.CategoryID, 130, sticker.Duration, sticker.SpriteURL, false));
            recentStickerList.Add(sticker.ID, sticker);
            
        }

        #endregion


        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int count = 0;
            List<int> temp = new List<int>();
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {

                foreach(var x in cateStoreList)
                {
                    count++;
                    AddCateIntoStore(x.Value);
                    temp.Add(x.Key);
                    if (count == 5) break;
                }

                foreach (var x in temp)
                {
                    cateStoreList.Remove(x);
                }
            }
        }
    }
}
