using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Windows.Media.Imaging;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for ImageContainner.xaml
    /// </summary>
    public partial class ImageContainner : UserControl
    {
        public int ID
        {
            get; set;
        }


        public string ImgUri
        {
            get; set;
        }



        public ImageContainner(int id, string imgUri)
        {
            InitializeComponent();

            ID = id;
            ImgUri = imgUri;
            InitImageAndThumb();
        }

        private void InitImageAndThumb()
        {

            imgThumb.Height = 130;
            imgFull.Source = new BitmapImage(new Uri(ImgUri, UriKind.RelativeOrAbsolute));
            imgThumb.Source = new BitmapImage(new Uri(ImgUri, UriKind.RelativeOrAbsolute));
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true)) return;

            //if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
            //    FruitListBox.Items.Add(FruitTextBox.Text.Trim());
        }
    }
}
