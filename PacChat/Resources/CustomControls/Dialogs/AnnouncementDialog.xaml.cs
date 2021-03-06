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

namespace PacChat.Resources.CustomControls.Dialogs
{
    /// <summary>
    /// Interaction logic for AnnouncementDialog.xaml
    /// </summary>
    public partial class AnnouncementDialog : UserControl
    {
        public AnnouncementDialog(params string[] messages)
        {
            InitializeComponent();
            txtContent.Text = String.Join("\n", messages);
        }
    }
}
