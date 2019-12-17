using PacChat.Network.RestAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for AvatarDisplayer.xaml
    /// </summary>
    public partial class AvatarDisplayer : UserControl
    {
        private static List<AvatarDisplayer> avatarInstance = new List<AvatarDisplayer>();

        public AvatarDisplayer()
        {
            InitializeComponent();
            avatarInstance.Add(this);
        }

        public Border AvatarBorder { get => AvaBorder; }

        public String UserID
        {
            get { return (String)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserIDProperty =
            DependencyProperty.Register("UserID", typeof(String), typeof(AvatarDisplayer), new PropertyMetadata(String.Empty));

        public bool IsOnline
        {
            get { return (bool)GetValue(IsOnlineProperty); }
            set { SetValue(IsOnlineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOnline.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOnlineProperty =
            DependencyProperty.Register("IsOnline", typeof(bool), typeof(AvatarDisplayer), new PropertyMetadata(false));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == UserIDProperty)
            {
                LoadingAhihi.Visibility = Visibility.Visible;
                if (String.IsNullOrEmpty(UserID))
                {
                    ProfileAPI.DownloadSelfAvatar((avaSource) =>
                    {
                        this.ImageAva.ImageSource = avaSource;
                        LoadingAhihi.Visibility = Visibility.Hidden;
                    }, (ex) => Console.WriteLine(ex));
                }
                else
                {
                    ProfileAPI.DownloadUserAvatar(UserID,(avaSource) =>
                    {
                        this.ImageAva.ImageSource = avaSource;
                        LoadingAhihi.Visibility = Visibility.Hidden;
                    }, (ex) => Console.WriteLine(ex));
                }
            }

            if (e.Property == IsOnlineProperty)
            {
                if (IsOnline)
                {
                    OnlineDot.Visibility = Visibility.Visible;
                } else
                {
                    OnlineDot.Visibility = Visibility.Hidden;
                }
            }

            base.OnPropertyChanged(e);
        }

        public void UpdateAllInstance(ImageSource source, String userID = null)
        {
            List<AvatarDisplayer> filtered;
            if (String.IsNullOrEmpty(userID))
            {
                filtered = avatarInstance.Where(p => String.IsNullOrEmpty(p.UserID)).ToList();
            } else
            {
                filtered = avatarInstance.Where(p => p.UserID == userID).ToList();
            }
            foreach (AvatarDisplayer item in filtered)
            {
                item.ImageAva.ImageSource = source;
                item.LoadingAhihi.Visibility = Visibility.Hidden;
            }
        }

        public void UpdateAllInstance(String userID = null)
        {
            LoadingAhihi.Visibility = Visibility.Visible;
            if (String.IsNullOrEmpty(userID))
            {
                ProfileAPI.DownloadSelfAvatar((avaSource) =>
                {
                    UpdateAllInstance(avaSource);
                    LoadingAhihi.Visibility = Visibility.Hidden;
                }, (ex) => Console.WriteLine(ex));
            }
            else
            {
                ProfileAPI.DownloadUserAvatar(userID, (avaSource) =>
                {
                    UpdateAllInstance(avaSource, userID);
                    LoadingAhihi.Visibility = Visibility.Hidden;
                }, (ex) => Console.WriteLine(ex));
            }
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            avatarInstance.Remove(this);
        }

        #region Custom ClickEvent
        public event EventHandler Click;

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
        #endregion

    }
}
