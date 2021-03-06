﻿using System;
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
using System.Windows.Media.Effects;
using PacChat.Utils;
using System.IO;
using PacChat.Network.Packets.AfterLoginRequest.Profile;
using PacChat.Network;
using PacChat.Network.RestAPI;
using PacChat.Network.Packets.AfterLoginRequest.Message;

namespace PacChat
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : UserControl
    {
        public static SettingPage Instance;
        private ImageSource currentImage;
        private bool editFlag; // 0: edit, 1: save

        public SettingPage()
        {
            InitializeComponent();
            currentImage = null;
            Instance = this;
            loadBG_Gallery();
            
            BGColorPicker.buttonClick += BubbleColorPicker_buttonClick;

            editFlag = false;
            EditBtn.Content = "Edit...";
            FirstNameInp.IsEnabled = false;
            LastNameInp.IsEnabled = false;
            BirthdayInp.IsEnabled = false;
            GenderInp.IsEnabled = false;
            AddressInp.IsEnabled = false;
        }

        private void BubbleColorPicker_buttonClick(Color color)
        {
            Console.WriteLine(ColorUtils.ColorToInt(color));
            ChatPage.Instance.SetSolidBG(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ModifyPassword packet = new ModifyPassword();
            packet.OldPassword = OldPassword.Password;
            packet.NewPassword = NewPassword.Password;
            _ = ChatConnection.Instance.Send(packet);
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
            int count = 0;
            foreach (String uri in ResourceUtil.ChatPageResource)
            {
                if (!File.Exists(uri))
                {
                    ResourceAPI.DownloadResource(uri, null, (sender, e) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            BGSelectContainner bg = new BGSelectContainner(uri);
                            wrappanelBG_Gallery.Children.Add(bg);
                            count++;
                            if (count >= ResourceUtil.ChatPageResource.Count)
                            {
                                Packets.SendPacket<ChatThemeGetRequest>();
                            } 
                        });
                    }, (e) => Console.WriteLine(e));
                } else
                {
                    try
                    {
                        BGSelectContainner bg = new BGSelectContainner(uri);
                        wrappanelBG_Gallery.Children.Add(bg);
                        count++;
                        if (count >= ResourceUtil.ChatPageResource.Count)
                        {
                            Packets.SendPacket<ChatThemeGetRequest>();
                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        try
                        {
                            File.Delete(uri);
                            ResourceAPI.DownloadResource(uri, null, (sender, e) =>
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    BGSelectContainner bg = new BGSelectContainner(uri);
                                    wrappanelBG_Gallery.Children.Add(bg);
                                    count++;
                                    if (count >= ResourceUtil.ChatPageResource.Count)
                                    {
                                        Packets.SendPacket<ChatThemeGetRequest>();
                                    }
                                });
                            }, (e) => Console.WriteLine(e));
                        } catch (Exception e) { Console.WriteLine(e); }
                    }
                }
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
                FileStream stream = null;
                try
                {
                    stream = new FileStream(op.FileName, FileMode.Open, FileAccess.Read);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();

                    AddBGPreview(bitmap);
                }
                catch
                {
                    throw;
                }
                //finally
                //{
                //    stream.Close();
                //}

                //AddBGPreview(op.FileName);
            }
        }

        public void AddBGPreview(ImageSource source)
        {
            currentImage = source.Clone();
            VisualBrush vb = new VisualBrush();
            Image im = new Image();
            BlurEffect ef = new BlurEffect();
            ef.Radius = (int)GetBlurLv();

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
            vb.Stretch = Stretch.Uniform;
            vb.Viewbox = new Rect(0.05, 0.05, 0.9, 0.9);
            borderBG_Preview.Background = vb;

            ChatPage.Instance.addBackgroundImage(source, (int)GetBlurLv());
        }

        public int GetBlurLv()
        {
            return (int)blurLv.Value;
        }

        private void blurLv_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (currentImage != null)
            {
                AddBGPreview(currentImage);
            }
        }

        private void ProfileAvatar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (editFlag)
            {
                // Save & switch back to edit...
                EditBtn.Content = "Edit...";
                FirstNameInp.IsEnabled = false;
                LastNameInp.IsEnabled = false;
                BirthdayInp.IsEnabled = false;
                GenderInp.IsEnabled = false;
                AddressInp.IsEnabled = false;
                editFlag = false;
                UpdateSelfProfile packet = new UpdateSelfProfile();
                packet.FirstName = FirstNameInp.Text;
                packet.LastName = LastNameInp.Text;
                packet.DateOfBirth = BirthdayInp.SelectedDate.Value;
                packet.Gender = (Gender)GenderInp.SelectedIndex;
                packet.Town = AddressInp.Text;
                _ = ChatConnection.Instance.Send(packet);
            }
            else
            {
                EditBtn.Content = "Save";
                FirstNameInp.IsEnabled = true;
                LastNameInp.IsEnabled = true;
                BirthdayInp.IsEnabled = true;
                GenderInp.IsEnabled = true;
                AddressInp.IsEnabled = true;
                editFlag = true;
            }
        }
    }
}
