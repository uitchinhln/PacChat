using Microsoft.Win32;
using PacChat.Network.RestAPI;
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

namespace PacChat.UIElements
{
    /// <summary>
    /// Interaction logic for ProfileAvatar.xaml
    /// </summary>
    public partial class ProfileAvatar : UserControl
    {
        public ProfileAvatar()
        {
            InitializeComponent();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var app = MainWindow.chatApplication;
            OpenFileDialog op = new OpenFileDialog();

            ProfileAPI.AvatarUpload(op.FileName, OnImageUploadCompleted, OnImageUploadError);
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                if (op.FileName.Length < 1) return;
                String path = op.FileNames[0];
                ProfileAPI.AvatarUpload(path, OnImageUploadCompleted, OnImageUploadError);
            }
        }

        void OnImageUploadError(Exception error)
        {
            Console.WriteLine(error);
        }

        void OnImageUploadCompleted()
        {
            Console.WriteLine("Avatar upload completed");
            AvtDisplayer.UpdateAllInstance();
        }
    }
}
