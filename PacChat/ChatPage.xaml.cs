﻿using System;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PacChat.ChatPageContents;
using PacChat.Resources.CustomControls;
using PacChat.MessageCore.Message;
using PacChat.Utils;
using Microsoft.Win32;
using PacChat.Network.Packets.AfterLoginRequest.Message;
using PacChat.Network;
using PacChat.Network.RestAPI;
using static PacChat.Network.RestAPI.FileAPI;
using PacChat.Resources.CustomControls.Media;
using PacChat.MessageCore.Sticker;
using PacChat.Resources;
using System.IO;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        private BubbleChat _previousBubbleChat;
        private BubbleChat _headBubbleChat;
        private bool _button1Clicked;
        public static ChatPage Instance;
        public Color bubbleColor = (Color)ColorConverter.ConvertFromString("#0F4C82");

        public Color ThemeColor { get; set; }

        public ChatPage()
        {
            InitializeComponent();
            //Console.WriteLine(ColorUtils.ColorToInt((Color)ColorConverter.ConvertFromString("#FF2C2C2C")));
            Instance = this;
            _button1Clicked = false;
            // SetAva("/PacChat/PacChat/Resources/Drawable/ava.jpg");
            //loadStickerToContainner();
            Transitioner.SelectedIndex = 0;
            bubbleColorPicker.buttonClick += BubbleColorPicker_buttonClick;
        }

        private void BubbleColorPicker_buttonClick(Color color)
        {
            var chatApplication = MainWindow.chatApplication;

            if (String.IsNullOrEmpty(chatApplication.model.currentSelectedConversation))
            {
                return;
            }

            BubbleChatColorSetRequest packet = new BubbleChatColorSetRequest()
            {
                ConversationID = chatApplication.model.currentSelectedConversation,
                BubbleColor = ColorUtils.ColorToInt(color)
            };
            ChatConnection.Instance.Send(packet);

        }

        // Chat Input KeyDown if and only if message is text message
        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    ChatInput.AppendText(Environment.NewLine);
                    ChatInput.CaretIndex = ChatInput.Text.Length;
                    return;
                }

                // Send message here
                if (String.IsNullOrEmpty(ChatInput.Text)) return;

                SendMessage(new TextMessage() { Message = ChatInput.Text });

                // Clear textbox
                ChatInput.Text = String.Empty;
            }
        }

        public void UploadImage()
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.mp4|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" + 
              "MPEG4 (*.mp4)|*.mp4";

            FakeLoadingBubble bubble = new FakeLoadingBubble();

            if (op.ShowDialog() == true)
            {
                List<string> paths = op.FileNames.ToList();
                var app = MainWindow.chatApplication;
                FileAPI.UploadMedia(app.model.currentSelectedConversation,
                    paths, bubble.OnImageUploadCompleted, OnImageUploadError);
                AddFakeLoadingBubble(bubble);
            }
        }

        public void AddFakeLoadingBubble(FakeLoadingBubble bubble)
        {
            bubble.HorizontalAlignment = HorizontalAlignment.Right;
            spMessagesContainer.Children.Add(bubble);
        }

        public void UploadVideo()
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a video";
            op.Filter = "MPEG4 (*.mp4)|*.mp4";

            FakeLoadingBubble bubble = new FakeLoadingBubble();
           
            if (op.ShowDialog() == true)
            {
                List<string> paths = op.FileNames.ToList();
                var app = MainWindow.chatApplication;
                FileAPI.UploadMedia(app.model.currentSelectedConversation,
                    paths, bubble.OnVideoUploadCompleted, OnVideoUploadError);
                AddFakeLoadingBubble(bubble);
            }
        }

        public MediaInfo GetMediaInfo(string fileID, string fileName, string conversationID)
        {
            String thumbUrl = StreamAPI.GetMediaThumbnailURL(fileID, conversationID);
            String streamUrl = StreamAPI.GetMediaURL(fileID, conversationID);

            MediaInfo media = new MediaInfo(thumbUrl, streamUrl, fileName, fileID);
            return media;
        }

        #region Image Upload Handler
        private void OnImageUploadCompleted(Dictionary<string, string> result)
        {
            foreach (var image in result)
            {
                string fileName = image.Key;
                string fileID = image.Value;

                ImageMessage message = new ImageMessage();
                message.FileID = fileID;
                message.FileName = fileName;

                SendMessage(message);
            }
        }

        private void OnImageUploadError(Exception error)
        {
            Console.WriteLine(error);
        }
        #endregion

        #region Video Upload Handler
        private void OnVideoUploadCompleted(Dictionary<string, string> result)
        {
            foreach (var video in result)
            {
                string fileName = video.Key;
                string fileID = video.Value;

                VideoMessage message = new VideoMessage();
                message.FileID = fileID;
                message.FileName = fileName;

                SendMessage(message);
            }
        }

        private void OnVideoUploadError(Exception error)
        {
            Console.WriteLine(error);
        }
        #endregion

        #region Sticker send
        public void SendStickerOnTheRight(MessageCore.Sticker.Sticker stickerInfo, bool reversed = false)
        {
            try
            {
                Resources.CustomControls.Sticker sticker =
                    new Resources.CustomControls.Sticker(this, false, stickerInfo.ID, stickerInfo.CategoryID, 130, stickerInfo.Duration, stickerInfo.SpriteURL, true);

                Thickness margin = sticker.Margin;
                margin.Right = 30;
                margin.Top = 10;
                margin.Bottom = 10;
                sticker.HorizontalAlignment = HorizontalAlignment.Right;
                sticker.Margin = margin;

                if (reversed)
                {
                    spMessagesContainer.Children.Insert(0, sticker);
                }
                else
                {
                    spMessagesContainer.Children.Add(sticker);
                }
                MessagesContainer.ScrollToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Sticker ex: ", e);
            }
        }

        public void SendStickerOnTheLeft(MessageCore.Sticker.Sticker stickerInfo, bool reversed = false, BubbleChat previous = null, BubbleChat head = null)
        {
            try
            {
                Resources.CustomControls.Sticker sticker =
                new Resources.CustomControls.Sticker(this, false, stickerInfo.ID, stickerInfo.CategoryID, 130, stickerInfo.Duration, stickerInfo.SpriteURL, true);

                Thickness margin = sticker.Margin;
                margin.Left = 30;
                margin.Top = 10;
                margin.Bottom = 10;
                sticker.HorizontalAlignment = HorizontalAlignment.Left;
                sticker.Margin = margin;

                if (reversed)
                {
                    head.InsertSticker(sticker);
                }
                else
                {
                    previous.AddSticker(sticker);
                }
                // spMessagesContainer.Children.Add(sticker);
                MessagesContainer.ScrollToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Sticker ex: ", e);
            }
        }

        #endregion

        public void AddMedia(string fileID, string fileName, bool isSimulating)
        {
            var app = MainWindow.chatApplication;

            if (!app.model.MediaMessages.ContainsKey(app.model.currentSelectedConversation))
                app.model.MediaMessages.Add(app.model.currentSelectedConversation, new List<MediaPack>());

            var mediaPack = new MediaPack() { FileID = fileID, FileName = fileName };

            MediaPlayerWindow mediaWin = null;
            if (app.model.MediaWindows.ContainsKey(app.model.currentSelectedConversation))
                mediaWin = app.model.MediaWindows[app.model.currentSelectedConversation];

            if (isSimulating)
            {
                if (app.model.MediaMessages[app.model.currentSelectedConversation].
                    FindIndex(x => x.FileID.Equals(mediaPack.FileID)) < 0)
                {
                    app.model.MediaMessages[app.model.currentSelectedConversation].
                        Add(mediaPack);
                    if (mediaWin != null)
                        mediaWin.MediaPlayer.AddMediaItem
                        (
                            app.model.currentSelectedConversation,
                            fileID, fileName, 0
                        );
                    app.model.Conversations[app.model.currentSelectedConversation].LastMediaID--;
                }
            }
            else
            {
                if (app.model.MediaMessages[app.model.currentSelectedConversation].
                    FindIndex(x => x.FileID.Equals(mediaPack.FileID)) < 0)
                {
                    app.model.MediaMessages[app.model.currentSelectedConversation].
                        Insert(0, mediaPack);
                    if (mediaWin != null)
                        mediaWin.MediaPlayer.AddMediaItemToFirst
                        (
                            app.model.currentSelectedConversation,
                            fileID, fileName, 0
                        );
                    app.model.Conversations[app.model.currentSelectedConversation].LastMediaID--;
                }
            }

            return;

            if (app.model.MediaWindows.ContainsKey(app.model.currentSelectedConversation))
            {
                var mediaWindow = app.model.MediaWindows[app.model.currentSelectedConversation];
                if (mediaWindow != null && mediaWindow.Visibility == Visibility.Visible)
                {
                    if (isSimulating)
                        mediaWindow.MediaPlayer.AddMediaItem
                        (
                            conversationID: app.model.currentSelectedConversation,
                            fileID: fileID,
                            fileName: fileName,
                            0
                        );
                    else
                        mediaWindow.MediaPlayer.AddMediaItemToFirst
                        (
                            conversationID: app.model.currentSelectedConversation,
                            fileID: fileID,
                            fileName: fileName,
                            0
                        );
                }
            }

            return;

            if (!isSimulating)
                MainWindow.Instance.MediaPlayerWindow.MediaPlayer.AddMediaItemToFirst
                (
                    app.model.currentSelectedConversation,
                    fileID, fileName,
                    0,
                    true
                );
            else
                MainWindow.Instance.MediaPlayerWindow.MediaPlayer.AddMediaItem
                (
                    app.model.currentSelectedConversation,
                    fileID, fileName,
                    0,
                    true
                );
        }

        public void SendMessage(AbstractMessage msg, bool isSimulating = false, bool reversed = false, bool loadConversation = false) //on the Rightside
        {
            if (reversed) _headBubbleChat = null;
            else _previousBubbleChat = null;

            Bubble b = null;
            ThumbnailBubble thumbnail = null;
            var app = MainWindow.chatApplication;

            if (BubbleTypeParser.Parse(msg) == BubbleType.Text)
            {
                b = new Bubble();
                b.Messages = (msg as TextMessage).Message;
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Attachment)
            {
                b = new Bubble();
                b.Messages = (msg as AttachmentMessage).FileName;
                b.SetLink((msg as AttachmentMessage).FileID);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Image)
            {
                string fileID = (msg as ImageMessage).FileID;
                string fileName = (msg as ImageMessage).FileName;
                MediaInfo media = GetMediaInfo(fileID, fileName, app.model.currentSelectedConversation);
                thumbnail = new ThumbnailBubble(media);
                thumbnail.HorizontalAlignment = HorizontalAlignment.Right;
                thumbnail.Margin = new Thickness(0, 0, 16, 0);
                // thumbnail.IsActive = true;
                Console.WriteLine("Image sent");
                AddMedia(fileID, fileName, isSimulating);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Video)
            {
                string fileID = (msg as VideoMessage).FileID;
                string fileName = (msg as VideoMessage).FileName;
                MediaInfo media = GetMediaInfo(fileID, fileName, app.model.currentSelectedConversation);
                thumbnail = new ThumbnailBubble(media);
                thumbnail.HorizontalAlignment = HorizontalAlignment.Right;
                thumbnail.Margin = new Thickness(0, 0, 16, 0);
                // thumbnail.IsActive = true;

                AddMedia(fileID, fileName, isSimulating);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Sticker)
            {
                MessageCore.Sticker.Sticker sticker = (msg as StickerMessage).Sticker;
                //var result;
                MessageCore.Sticker.Sticker.LoadedStickers.TryGetValue(sticker.ID, out var result);
                SendStickerOnTheRight(result, reversed);
            }

            if (b != null)
            {
                b.Type = BubbleTypeParser.Parse(msg);
                b.ControlUpdate();

                b.SetBG(bubbleColor);

                b.IsLeft = false;

                if (bubbleColor.R + bubbleColor.G + bubbleColor.B >= 565) b.SetTextColor(Colors.Black);
                else b.SetTextColor(Colors.White);
                b.SetDirect(false);// true = left false = right
                b.SetSeen(false);
            }

            if (reversed)
            {
                if (b != null) spMessagesContainer.Children.Insert(0, b);
                if (thumbnail != null) spMessagesContainer.Children.Insert(0, thumbnail);
                if (loadConversation)
                    MessagesContainer.ScrollToEnd();
                else
                    MessagesContainer.ScrollToHome();
            }
            else
            {
                if (b != null) spMessagesContainer.Children.Add(b);
                if (thumbnail != null) spMessagesContainer.Children.Add(thumbnail);
                MessagesContainer.ScrollToEnd();
            }

            if (thumbnail != null) thumbnail.Click += ThumbnailClick;

            app.model.CurrentUserMessages.Add(new BubbleInfo(msg, false));
            app.model.Conversations[app.model.currentSelectedConversation].Bubbles.Add(new BubbleInfo(msg, false));

            if (isSimulating) return;
            SendMessage packet = new SendMessage();
            packet.ConversationID = app.model.currentSelectedConversation;
            packet.Message = msg;
            _ = ChatConnection.Instance.Send(packet);
        }

        public void SendLeftMessages(AbstractMessage msg, bool isSimulating = false, bool reversed = false, bool loadConversation = false)
        {
            Console.WriteLine(msg.SenderID);

            if (reversed)
            {
                if (_headBubbleChat == null)
                {
                    _headBubbleChat = new BubbleChat();
                    _headBubbleChat.SetAva(msg.SenderID);
                    _headBubbleChat.NickName.Foreground = iconColor.Foreground;
                    spMessagesContainer.Children.Insert(0, _headBubbleChat);
                }
            }
            else
            {
                if (_previousBubbleChat == null)
                {
                    _previousBubbleChat = new BubbleChat();
                    _previousBubbleChat.SetAva(msg.SenderID);
                    _previousBubbleChat.NickName.Foreground = iconColor.Foreground;
                    spMessagesContainer.Children.Add(_previousBubbleChat);
                }
            }

            Bubble b = null;
            ThumbnailBubble thumbnail = null;
            var app = MainWindow.chatApplication;

            if (BubbleTypeParser.Parse(msg) == BubbleType.Text)
            {
                b = new Bubble();
                b.Messages = (msg as TextMessage).Message;
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Attachment)
            {
                b = new Bubble();
                b.Messages = (msg as AttachmentMessage).FileName;
                b.SetLink((msg as AttachmentMessage).FileID);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Image)
            {
                string fileID = (msg as ImageMessage).FileID;
                string fileName = (msg as ImageMessage).FileName;
                MediaInfo media = GetMediaInfo(fileID, fileName, app.model.currentSelectedConversation);
                thumbnail = new ThumbnailBubble(media);
                thumbnail.HorizontalAlignment = HorizontalAlignment.Left;
                thumbnail.Margin = new Thickness(5, 0, 0, 0);
                Console.WriteLine("Image sent");
                AddMedia(fileID, fileName, isSimulating);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Video)
            {
                string fileID = (msg as VideoMessage).FileID;
                string fileName = (msg as VideoMessage).FileName;
                MediaInfo media = GetMediaInfo(fileID, fileName, app.model.currentSelectedConversation);
                thumbnail = new ThumbnailBubble(media);
                thumbnail.HorizontalAlignment = HorizontalAlignment.Left;
                thumbnail.Margin = new Thickness(5, 0, 0, 0);
                AddMedia(fileID, fileName, isSimulating);
            }
            else if (BubbleTypeParser.Parse(msg) == BubbleType.Sticker)
            {
                MessageCore.Sticker.Sticker sticker = (msg as StickerMessage).Sticker;
                MessageCore.Sticker.Sticker.LoadedStickers.TryGetValue(sticker.ID, out var result);
                SendStickerOnTheLeft(result, reversed, _previousBubbleChat, _headBubbleChat);
            }

            if (b != null)
            {
                b.Type = BubbleTypeParser.Parse(msg);
                b.ControlUpdate();

                b.SetSeen(false);
                b.SetBG(Color.FromRgb(246, 246, 246));
                b.SetDirect(true); // true = left false = right
            }

            if (reversed)
            {
                if (b != null) _headBubbleChat.InsertBubble(0, b);
                if (thumbnail != null) _headBubbleChat.InsertMedia(thumbnail);
                if (loadConversation)
                    MessagesContainer.ScrollToEnd();
                else
                    MessagesContainer.ScrollToHome();
            }
            else
            {
                if (b != null) _previousBubbleChat.AddBubble(b);
                if (thumbnail != null) _previousBubbleChat.AddMedia(thumbnail);
                MessagesContainer.ScrollToEnd();
            }

            if (thumbnail != null) thumbnail.Click += ThumbnailClick;

            app.model.CurrentUserMessages.Add(new BubbleInfo(msg, true));
            app.model.Conversations[app.model.currentSelectedConversation].Bubbles.Add(new BubbleInfo(msg, true));
        }

        public void ThumbnailClick(object sender, EventArgs e)
        {
            //ThumbnailBubble thumb = (sender as ThumbnailBubble);
            //MainWindow.Instance.MediaPlayerWindow.Show();
            //MainWindow.Instance.MediaPlayerWindow.MediaPlayer.ShowMedia(thumb.FileID);
        }

        public void SetActive(bool enabled)
        {
            Transitioner.SelectedIndex = enabled ? 1 : 0;
        }

        public void SetAva(string userID = null)
        {
            Avatar.UserID = userID;
        }

        public void ClearChatPage()
        {
            _previousBubbleChat = null;
            _headBubbleChat = null;
            spMessagesContainer.Children.Clear();
        }

        public void LoadChatPage(string conversationID, string userID = "")
        {
            Console.WriteLine("Load chat page on id: " + conversationID);
            SetActive(true);

            var app = MainWindow.chatApplication;

            if (conversationID.Equals("~") && !string.IsNullOrEmpty(userID))
            {
                SingleConversationFrUserID packet = new SingleConversationFrUserID();
                packet.UserID = userID;
                _ = ChatConnection.Instance.Send(packet);
                Console.WriteLine("Create conversation");
                LoadMessagesBtn.Visibility = Visibility.Collapsed;
                return;
            }

            app.model.currentSelectedConversation = conversationID;
            ConversationFromID convPacket = new ConversationFromID();
            convPacket.ConversationID = conversationID;
            _ = ChatConnection.Instance.Send(convPacket);

            return;
        }

        public void LoadMessages(string conversationID, bool loadConversation = false)
        {
            Console.WriteLine("Load mess");
            var app = MainWindow.chatApplication;
            if (app.model.Conversations[conversationID].LastMessID < 0)
                return;
            GetMessageFromConversation msgPacket = new GetMessageFromConversation();
            msgPacket.ConversationID = conversationID;
            msgPacket.MessagePosition = app.model.Conversations[conversationID].LastMessID;
            msgPacket.Quantity = 10;
            msgPacket.LoadConversation = loadConversation;
            app.model.Conversations[conversationID].LastMessID -= 10;
            _ = ChatConnection.Instance.Send(msgPacket);

            LoadMessagesBtn.Visibility = Visibility.Visible;
            if (app.model.Conversations[conversationID].LastMessID < 0)
                LoadMessagesBtn.Visibility = Visibility.Collapsed;
        }

        public void LoadMedia(string conversationID, bool firstTime)
        {
            Console.WriteLine("Load media");
            var app = MainWindow.chatApplication;
            if (app.model.Conversations[conversationID].LastMediaID < 0)
                return;
            GetMediaFromConversation packet = new GetMediaFromConversation();
            packet.ConversationID = conversationID;
            packet.MediaPosition = app.model.Conversations[conversationID].LastMediaID;
            packet.Quantity = 10;
            app.model.Conversations[conversationID].LastMediaID -= firstTime ? 0 : 10;
            _ = ChatConnection.Instance.Send(packet);
        }

        public void StoreChatPage(string conversationID)
        {
            return;

            var app = MainWindow.chatApplication;

            Console.WriteLine("Store chat page on id: " + conversationID + " with length: " + app.model.CurrentUserMessages.Count);

            ConversationBubble msgList = app.model.Conversations[conversationID];
            msgList.Bubbles.AddRange(app.model.CurrentUserMessages);

            app.model.CurrentUserMessages.Clear();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (ChatInput.Text == "") return;

            SendMessage(new TextMessage() { Message = ChatInput.Text });

            // Clear textbox
            ChatInput.Text = "";
        }


        private void btnSendImage_Click(object sender, RoutedEventArgs e)
        {
            UploadImage();
            return;
            //OpenFileDialog op = new OpenFileDialog();
            //ImageContainner image;
            //op.Title = "Select a picture";
            //op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            //  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            //  "Portable Network Graphic (*.png)|*.png";
            //if (op.ShowDialog() == true)
            //{
            //    _previousBubbleChat = null;
            //    image = new ImageContainner(1, op.FileName);
            //    image.HorizontalAlignment = HorizontalAlignment.Right;

            //    Thickness margin = image.Margin;
            //    margin.Right = 30;
            //    image.Margin = margin;
            //    spMessagesContainer.Children.Add(image);
            //    MessagesContainer.ScrollToEnd();
            //}

        }

        private void showLeftImage(int id, string uri)
        {
            //ImageContainner image = new ImageContainner(1, uri);
            //image.HorizontalAlignment = HorizontalAlignment.Right;

            //Thickness margin = image.Margin;
            //margin.Left = 30;
            //image.Margin = margin;
            //spMessagesContainer.Children.Add(image);
            //MessagesContainer.ScrollToEnd();
        }

        private void MessagesContainer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
        }

        private void LoadMessagesBtn_Click(object sender, RoutedEventArgs e)
        {
            var app = MainWindow.chatApplication;
            LoadMessages(app.model.currentSelectedConversation);
        }

        public void addBackgroundImage(ImageSource source, int blur)
        {
            VisualBrush vb = new VisualBrush();
            Image im = new Image();
            BlurEffect ef = new BlurEffect();
            ef.Radius = blur;

            //FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //try
            //{
            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.StreamSource = stream;
            //    bitmap.EndInit();

            //    im.Source = bitmap;
            //}
            //catch
            //{
            //    stream.Close();
            //    throw;
            //}
            im.Source = source;

            im.Effect = ef;

            vb.Visual = im;
            vb.Stretch = Stretch.UniformToFill;
            vb.Viewbox = new Rect(0.05, 0.05, 0.9, 0.9);
            ChatBorder.Background = vb;

        }

        private void btnAttachment_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a file";

            FakeLoadingBubble bubble = new FakeLoadingBubble();

            if (op.ShowDialog() == true)
            {
                List<string> paths = op.FileNames.ToList();
                var app = MainWindow.chatApplication;
                UploadAttachment(app.model.currentSelectedConversation,
                    paths, bubble.OnFileUploadCompleted, OnFileUploadError);
                AddFakeLoadingBubble(bubble);
            }
        }

        private void OnFileUploadError(Exception error)
        {
            Console.WriteLine(error);
        }

        private void OnFileUploadCompleted(Dictionary<string, string> results)
        {
            foreach (var file in results)
            {
                AttachmentMessage message = new AttachmentMessage();
                message.FileID = file.Value;
                message.FileName = file.Key;

                SendMessage(message, false, false);
            }
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            UploadVideo();
        }

        private double randomDouble(double a, double b)
        {
            Random rand = new Random();
            double result = (rand.NextDouble() * (b - a)) + a;
            return result;
        }

        private void btnBuzz_Click(object sender, RoutedEventArgs e)
        {
            Buzz();
            var app = MainWindow.chatApplication;
            _ = ChatConnection.Instance.Send(new BuzzSend() { ConversationID = app.model.currentSelectedConversation });


        }

        public void Buzz()
        {
            var main = PacChat.MainWindow.Instance;
            double currLeft = main.Left;
            double currTop = main.Top;
            double buffer = 10;
            Action<object> buzz = (o) =>
            {
                Random rand = new Random();

                //Tuple<double, double> curr =new Tuple<double, double>(main.Top, main.Left);

                Action a = () =>
                {
                    main.Left = currLeft + randomDouble(-buffer, buffer);
                    main.Top = currTop + randomDouble(-buffer, buffer);
                    buffer -= 0.2f;
                };

                for (int i = 0; i <= 50; i++)
                {
                    Dispatcher.Invoke(a);
                    System.Threading.Thread.Sleep(10);
                }

            };
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(buzz));
        }

        public void ChangeIconColor(Color color)
        {
            Brush colorBrush = new SolidColorBrush(color);
            iconAttach.Foreground = colorBrush;
            iconSend.Foreground = colorBrush;
            iconImage.Foreground = colorBrush;
            iconBuzz.Foreground = colorBrush;
            iconSticker.Foreground = colorBrush;
            ChatTitle.Foreground = colorBrush;
            iconColor.Foreground = colorBrush;

            foreach (var item in spMessagesContainer.Children)
            {
                if (!(item is BubbleChat)) continue;
                (item as BubbleChat).NickName.Foreground = colorBrush;
            }

            LastActive.Foreground = colorBrush.Clone();
            LastActive.Foreground.Opacity = 0.6;
        }

        public void SetSolidBG(Color color)
        {
            ChatBorder.Background = new SolidColorBrush(color);
        }
    }
}
