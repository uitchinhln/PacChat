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
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
<<<<<<< HEAD
            op.Filter =  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg";

            if (op.ShowDialog() == true)
            {
                var app = MainWindow.chatApplication;
                ProfileAPI.AvatarUpload(op.FileName, OnImageUploadCompleted, OnImageUploadError);
=======
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                if (op.FileName.Length < 1) return;
                String path = op.FileNames[0];
                var app = MainWindow.chatApplication;
                ProfileAPI.AvatarUpload(path, OnImageUploadCompleted, OnImageUploadError);
>>>>>>> 6c8fb3127bbe9f2ca7afa610cae61c2ee7138513
            }
        }

        private void OnImageUploadError(Exception error)
        {
            Console.WriteLine(error);
        }

        private void OnImageUploadCompleted()
        {
<<<<<<< HEAD
            Console.WriteLine("Avatar upload completed");
            AvtDisplayer.UserID = null;
            MainWindow.Instance.SelfAvatar.UserID = null;
=======
            AvtDisplayer.UpdateAllInstance();
>>>>>>> 6c8fb3127bbe9f2ca7afa610cae61c2ee7138513
        }
    }
}
