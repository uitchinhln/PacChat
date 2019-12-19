using Microsoft.Win32;
using PacChat.Network.RestAPI;
using PacChat.Resources.CustomControls.Notifications;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for Bubble.xaml
    /// </summary>
    public partial class Bubble : UserControl
    {
        public BubbleType Type { get; set; }

        public bool IsLeft { get; set; } = true;

        public string Messages
        {
            get { return textBlock.Text.ToString(); }
            set
            {
                textBlock.Text = value.ToString();
            }
        }

        public double MessagesFontSize
        {
            get { return textBlock.FontSize; }
            set { textBlock.FontSize = (double)value; }
        }

        public Bubble()
        {
            InitializeComponent();

            AttachmentLink.Visibility = Visibility.Hidden;
        }

        public void SetText(string s)
        {
            textBlock.Text = s;
        }

        public void ControlUpdate()
        {
            if (Type == BubbleType.Attachment)
            {
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.TextDecorations = TextDecorations.Underline;
                AttachmentLink.Visibility = Visibility.Visible;
            }
        }

        public void SetBG(Color x)
        {
            borderTextBlock.Background = new SolidColorBrush(x);
        }

        public void SetTextColor(Color x)
        {
            textBlock.Foreground = new SolidColorBrush(x);
        }

        public void SetSeen(bool seened)
        {
            if (seened) textBlockSeen.Visibility = Visibility.Visible;
            else textBlockSeen.Visibility = Visibility.Collapsed;
        }

        public void SetDirect(bool direct)
        {
            if (direct) //left
            {
                this.HorizontalAlignment = HorizontalAlignment.Left;
                Thickness margin = borderBubble.Margin;
                margin.Left = 3;
                margin.Right = 170;
                margin.Bottom = 2;
                borderBubble.Margin = margin;
            }
            else
            {
                this.HorizontalAlignment = HorizontalAlignment.Right;
                Thickness margin = borderBubble.Margin;
                margin.Left = 170;
                margin.Right = 15;
                margin.Bottom = 2;
                borderBubble.Margin = margin;
            }
        }

        public void AddSlideFromBottom(Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        public void SetLink(string fileID)
        {
            AttachmentLink.Content = fileID;
        }

        private void AttachmentLink_Click(object sender, RoutedEventArgs e)
        {
            var app = MainWindow.chatApplication;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Select a place to download";
            dialog.Filter = "All file types|*.*";
            dialog.FileName = textBlock.Text;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialog.ShowDialog() == true)
            {
                //if (!MainWindow.Instance.DownloadWindow.Visible)
                //{
                //    MainWindow.Instance.DownloadWindow.ShowPopUp();
                //}
                var downloadWindow = DownloadWindow.Instance;

                DownloadProgressNoti noti = new DownloadProgressNoti();
                noti.SetFileName(System.IO.Path.GetFileName(dialog.FileName));
                noti.FileLocation = dialog.FileName;
                downloadWindow.DownloadList.Children.Insert(0, noti);
                MainWindow.chatApplication.model.DownloadProgresses.Add(noti);

                FileAPI.DownloadAttachment(app.model.currentSelectedConversation,
                    AttachmentLink.Content.ToString(), dialog.FileName,
                    noti.SetProgress, noti.FinalizeDownload, OnDownloadFileError);
            }
        }

        private void OnDownloadFileError(Exception error)
        {
            throw new NotImplementedException();
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download completed");
        }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
        }
    }
}
