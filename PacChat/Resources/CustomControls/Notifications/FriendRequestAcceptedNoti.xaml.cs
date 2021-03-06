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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacChat.Resources.CustomControls.Notifications
{
    /// <summary>
    /// Interaction logic for FriendRequestAcceptedNoti.xaml
    /// </summary>
    public partial class FriendRequestAcceptedNoti : UserControl
    {
        public int Position { get; set; }
        public FriendRequestAcceptedNoti()
        {
            InitializeComponent();
        }

        public void SetInfo(int position, string id, string name)
        {
            Position = position;
            ClickMask.Content = id;
            FriendName.Text = name;
        }

        private void ClickMask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
