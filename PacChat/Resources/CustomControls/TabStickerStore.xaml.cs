using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PacChat.MessageCore.Sticker;
using PacChat.Network.RestAPI;
using PacChat.Utils;
using PacChat.Network.Packets.AfterLoginRequest.Sticker;
using PacChat.Network;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for TabStickerStore.xaml
    /// </summary>
    public partial class TabStickerStore : UserControl
    {
        public int ID { get; set; }
        public StickerCategory Cate { get; set; }


        public TabStickerStore(StickerCategory cate)
        {
            InitializeComponent();
            Cate = cate;
            loadStore();
        }

        private void loadStore() //load thumbnail of sticker
        {
            StickerAPI.DownloadImage(Cate.ThumbImg, (thumbImage) =>
            {
                imgPreview.Source = thumbImage;

            });
            StickerAPI.DownloadImage(Cate.IconPreview, (iconPreviewImage) =>
            {
                imgIconPreview.Source = iconPreviewImage;
            });
            txtName.Text = Cate.Name;

        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            ChatConnection.Instance.Send(new BuyStickerCategoryRequest() { CateID = Cate.ID });
        }
    }
}
