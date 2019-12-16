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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PacChat.ChatAMVC;
using PacChat.Network.Packets.AfterLoginRequest;
using PacChat.Network;
using PacChat.Utils;
using PacChat.Network.Packets.AfterLoginRequest.Sticker;
using PacChat.Network.Packets.AfterLoginRequest.Notification;
using PacChat.Network.Packets.AfterLoginRequest.Message;
using PacChat.MessageCore.Sticker;
using PacChat.Network.Packets.AfterLoginRequest.Profile;
using PacChat.Network.RestAPI;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static int CurrentWindowWidth { get; set; }
        public static int CurrentWindowHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight{ get; set; }

        public MediaPlayerWindow MediaPlayerWindow { get; set; }

        #region Chat_AMVC
        private ChatModel _chatModel;
        private ChatView _chatView;
        private ChatController _chatController;
        public static ChatApplication chatApplication;

        private void InitAMVC()
        {
            _chatModel = new ChatModel();
            _chatView = new ChatView();
            _chatController = new ChatController();
            chatApplication = new ChatApplication();
            chatApplication.InitializeMVC(_chatModel, _chatView, _chatController);
        }
        #endregion

        private bool _isMaximized;
        public bool isMaximized
        {
            get
            {
                return _isMaximized;
            }
            set
            {
                _isMaximized = value;
                Application.Current.MainWindow.WindowState = _isMaximized == false ? WindowState.Normal : WindowState.Maximized;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            InitAMVC();

            Packets.SendPacket<GetFriendIDs>();
            Packets.SendPacket<GetNotifications>();
            Packets.SendPacket<GetSelfID>();
            Packets.SendPacket<GetSelfProfile>();
            Packets.SendPacket<RecentConversations>();
            Sticker.Load(() =>
            {
                Packets.SendPacket<GetBoughtStickerPacksRequest>();


            });

            SetNotificationDotState(false);

            ProfileAPI.GetAvatar((avaPath) =>
            {
                SelfAvatar.ImageSource = avaPath;
            });

            // Media window
            //MediaPlayerWindow = new MediaPlayerWindow();
            //MediaPlayerWindow.Show();

            /*
            try
            {
                GetFriendIDs data = new GetFriendIDs();
                _ = ChatConnection.Instance.Send(data);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            */
        }

        private void FormDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ToggleLeftSidePanel()
        {
            // var sb = this.FindResource("left-side-panel-" + (_isPanelOpened ? "compress" : "expand")) as Storyboard;
            // sb.Begin();
            // _isPanelOpened = !_isPanelOpened;
        }

        private void BtnNoti_Click(object sender, RoutedEventArgs e)
        {
            TabTransitioner.SelectedIndex = 0;
            SetNotificationDotState(false);
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            TabTransitioner.SelectedIndex = 2;
        }


        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            TabTransitioner.SelectedIndex = 1;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleLeftSidePanel();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            TabTransitioner.SelectedIndex = 4;
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            TabTransitioner.SelectedIndex = 3;
        }
        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            isMaximized = !isMaximized;
        }

        public void OpenProfileDisplayer(string name, string email, string dob, string address)
        {
            ProfileDisplayer.Display(name, email, dob, address);
            var sb = this.FindResource("left-side-panel-expand") as Storyboard;
            sb.Begin();
        }

        public void CloseProfileDisplayer()
        {
            var sb = this.FindResource("left-side-panel-compress") as Storyboard;
            sb.Begin();
        }

        public void SetNotificationDotState(bool display)
        {
            NotificationDot.Visibility = display ? Visibility.Visible : Visibility.Hidden;
        }

        private void UserList_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
