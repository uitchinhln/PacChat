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

namespace PacChat.Resources.CustomControls.ColourPicker
{
    /// <summary>
    /// Interaction logic for ColourPickerNonePopup.xaml
    /// </summary>
    public partial class ColourPickerNonePopup : UserControl
    {
        public ColourPickerNonePopup()
        {
            InitializeComponent();
            btnAccept.Click += BtnAccept_Click;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            ChatPage.Instance.ChangeIconColor(ColorPicker.Color);
        }
    }
}
