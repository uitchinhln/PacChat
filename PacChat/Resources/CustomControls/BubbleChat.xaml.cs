using System.Windows.Controls;
using System;

using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PacChat.Resources.CustomControls.Media;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for BubbleChat.xaml
    /// </summary>
    public partial class BubbleChat : UserControl
    {
        public BubbleChat()
        {
            InitializeComponent();
            // textBlock.Text = "asdasdsad";
            // SetAva("/PacChat/PacChat/Resources/Drawable/ava.jpg");
            NickName.Content = MainWindow.chatApplication.model.Title;
        }

        public void AddBubble(Bubble b)
        {
            spMessContainer.Children.Add(b);
        }

        public void InsertBubble(int index, Bubble b)
        {
            spMessContainer.Children.Insert(index, b);
        }

        public void AddMedia(ThumbnailBubble thumbnail)
        {
            spMessContainer.Children.Add(thumbnail);
        }

        public void InsertMedia(ThumbnailBubble thumbnail)
        {
            spMessContainer.Children.Insert(0, thumbnail);
        }

        public void SetAva(string path)
        {
            Avatar.ImageSource = path;
        }
    }
}
