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

        private bool _clickable;

        public Sticker(ChatPage chatpage, bool clickable, int id, int cateid, int size, int duration, string urisheet)
        {
            InitializeComponent();

            ID = id;
            CateID = cateid;
            Size = size;
            Duration = duration;
            UriSheet = urisheet;
            Chatpage = chatpage;
            Clickable = clickable;
            
            loadSticker();
        }

        public ChatPage Chatpage
        {
            get; set;
        }
        
        public bool Clickable
        {
            get { return _clickable; }
            set { _clickable = value; }
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
            anim.Clickable = _clickable;
            sticker.Children.Add(anim);
        }

        private void sticker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Clickable)
            {
                Chatpage.sendSticker(false, ID, CateID, Size, Duration, UriSheet);
            }
        }
    }
}
