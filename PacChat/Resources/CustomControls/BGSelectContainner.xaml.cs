using System;
using System.Collections.Generic;
using System.IO;
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

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for BGSelectContainner.xaml
    /// </summary>
    public partial class BGSelectContainner : UserControl
    {
        public BGSelectContainner(string uri)
        {
            InitializeComponent();
            BGUri = uri;
            SetImage();
        }

        public string BGUri { get; set; }

       public void SetImage()
        {
            FileStream stream = new FileStream(BGUri, FileMode.Open, FileAccess.Read);
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                imageBG.Source = bitmap;
            } catch
            {
                stream.Close();
                throw;
            }
        }


        private void buttonBG_Click(object sender, RoutedEventArgs e)
        {
            SettingPage.Instance.AddBGPreview(BGUri);
            Console.WriteLine("button");
        }
    }
}
