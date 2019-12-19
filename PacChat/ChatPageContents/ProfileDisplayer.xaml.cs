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

namespace PacChat.ChatPageContents
{
    /// <summary>
    /// Interaction logic for ProfileDisplayer.xaml
    /// </summary>
    public partial class ProfileDisplayer : UserControl
    {
        public ProfileDisplayer()
        {
            InitializeComponent();
        }

        public void Display(string id, string name, string email, string dob, string address)
        {
            AvatarDisplayer.UserID = id;
            NameDisplayer.Text = name;
            EmailDisplayer.Text = email;
            DoBDisplayer.Text = dob;
            AddressDisplayer.Text = address;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.CloseProfileDisplayer();
        }
    }
}
