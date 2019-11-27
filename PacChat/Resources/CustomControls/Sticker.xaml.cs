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

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl
    {
        private int _cateID;

        private int _ID;

        private int _size;

        private int _duration;

        private string _uriSheet;
        public Sticker(int id, int cateid, int size, int duration, string urisheet)
        {
            InitializeComponent();

            ID = id;
            CateID = cateid;
            Size = size;
            Duration = duration;
            UriSheet = urisheet;

            loadSticker();
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int CateID
        {
            get { return _cateID; }
            set { _cateID = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public string UriSheet
        {
            get { return _uriSheet; }
            set { _uriSheet = value; }
        }

        private void loadSticker()
        {
            Animator.Animator anim = new Animator.Animator();
            anim.countLimit = 10;
            anim.Interval = TimeSpan.FromSeconds((double)_duration / 1000);
            anim.ImageSource = new BitmapImage(new Uri(_uriSheet, UriKind.RelativeOrAbsolute));
            anim.VerticalOffset = _size;
            anim.HorizontalOffset = _size;
            sticker.Children.Add(anim);
        }
    }
}
