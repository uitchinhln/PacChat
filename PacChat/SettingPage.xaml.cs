using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PacChat.Resources.CustomControls;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : UserControl
    {
        public SettingPage()
        {
            InitializeComponent();
            loadBG_Gallery();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = MainWindow.chatApplication;
            storeDataToModel();
            app.controller.OnProfileChanged();
        }

        // This method stores data to model, then controller get data and update Database
        // in ChatAMVC -> ChatController: OnProfileChanged()
        private void storeDataToModel()
        {
            var app = MainWindow.chatApplication;
            var model = app.model;

            model.FirstName = FirstNameInp.Text;
            model.LastName = LastNameInp.Text;
            model.BirthDay = BirthdayInp.SelectedDate.Value;
            model.Gender = GenderInp.SelectedIndex;
        }

        private void loadBG_Gallery()
        {
            for (int i=1; i<=16;i++)
            {
                BGSelectContainner bg = new BGSelectContainner("BG" + i.ToString() + ".jpg");
                wrappanelBG_Gallery.Children.Add(bg);
            }
   
        }

        private void GeneralTab_Click(object sender, RoutedEventArgs e)
        {
            TabTrans.SelectedIndex = 0;
        }

        private void PasswordTab_Click(object sender, RoutedEventArgs e)
        {
            TabTrans.SelectedIndex = 1;
        }

        private void CustomTab_Click(object sender, RoutedEventArgs e)
        {
            TabTrans.SelectedIndex = 2;
        }

        private void BG_Computer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BG_Preview.ImageSource = new BitmapImage(new Uri(op.FileName, UriKind.RelativeOrAbsolute));

            }
        }
        
  
    }
}
