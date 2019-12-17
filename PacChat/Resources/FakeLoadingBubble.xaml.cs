using PacChat.MessageCore.Message;
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

namespace PacChat.Resources
{
    /// <summary>
    /// Interaction logic for FakeLoadingBubble.xaml
    /// </summary>
    public partial class FakeLoadingBubble : UserControl
    {
        public FakeLoadingBubble()
        {
            InitializeComponent();
        }

        public void OnVideoUploadCompleted(Dictionary<string, string> result)
        {
            foreach (var video in result)
            {
                string fileName = video.Key;
                string fileID = video.Value;

                VideoMessage message = new VideoMessage();
                message.FileID = fileID;
                message.FileName = fileName;

                ChatPage.Instance.SendMessage(message);
            }
            ChatPage.Instance.spMessagesContainer.Children.Remove(this);
        }

        public void OnImageUploadCompleted(Dictionary<string, string> result)
        {
            foreach (var image in result)
            {
                string fileName = image.Key;
                string fileID = image.Value;

                ImageMessage message = new ImageMessage();
                message.FileID = fileID;
                message.FileName = fileName;

                ChatPage.Instance.SendMessage(message);
            }
            ChatPage.Instance.spMessagesContainer.Children.Remove(this);
        }

        public void OnFileUploadCompleted(Dictionary<string, string> results)
        {
            foreach (var file in results)
            {
                AttachmentMessage message = new AttachmentMessage();
                message.FileID = file.Value;
                message.FileName = file.Key;

                ChatPage.Instance.SendMessage(message, false, false);
            }

            ChatPage.Instance.spMessagesContainer.Children.Remove(this);
        }
    }
}
