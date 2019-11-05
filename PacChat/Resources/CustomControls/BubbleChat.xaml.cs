using System.Windows.Controls;
using System;

using System.Windows;
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
    /// Interaction logic for BubbleChat.xaml
    /// </summary>
    public partial class BubbleChat : UserControl
    {
        public BubbleChat()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty setMessTextProperty =
            DependencyProperty.Register("Set Mess", typeof(string), typeof(BubbleChat), new
                PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(setMessTextProperty); }
            set { SetValue(setMessTextProperty, value); }
        }

        private static void OnSetTextChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            BubbleChat UserControl1Control = d as BubbleChat;
            UserControl1Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            textBlock.Text = e.NewValue.ToString();
        }
    }
}
