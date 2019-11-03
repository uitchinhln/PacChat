using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacChat
{
    public partial class tabMessages : UserControl
    {
        public tabMessages()
        {
            InitializeComponent();
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {

            Size size = new Size();
            Point location = new Point();
            string text = txtMessContent.Text;
            Console.WriteLine(text);
            tabBubbleMess bubble = new tabBubbleMess();
            panel3.Controls.Add(bubble);
            bubble.SetMess(text);
            bubble.Dock = DockStyle.Bottom;
            location = bubble.Location;
            size = bubble.Size;

            bubble.Dock = DockStyle.None;
            bubble.Location = location;
            bubble.Size = size;

   
        }
    }
}
