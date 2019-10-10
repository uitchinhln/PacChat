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
    public partial class tabBubbleMess : UserControl
    {

        public tabBubbleMess()
        {
            InitializeComponent();
        }

        public void SetMess(string text)
        {
            labelMessages.Text = text;
            changeSize();
        }

        private void changeSize()
        {
            //this.Height = labelMessContent.Height;
        }

    }
}
