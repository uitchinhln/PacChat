using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabMessagesInstance.BringToFront();
        }

        private void ButtonNotiTab_Click(object sender, EventArgs e)
        {
            tabNotiInstance.BringToFront();
        }

        private void ButtonMessagesTab_Click(object sender, EventArgs e)
        {
            tabMessagesInstance.BringToFront();
        }

        private void ButtonGroupMessagesTab_Click(object sender, EventArgs e)
        {
            tabGroupMessagesInstance.BringToFront();
        }

        private void ButtonSettingTab_Click(object sender, EventArgs e)
        {
            tabSettingInstance.BringToFront();
        }

        private void ButtonInfoTab_Click(object sender, EventArgs e)
        {
            tabInfoInstance.BringToFront();
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 55) // active
            {

                panelMenu.Width = 200;
                //bunifuTransition1.ShowSync(panelMenu);
                buttonMenu.Image = new Bitmap(PacChat.Properties.Resources.icon_Back);
                

            }
            else
            {
                panelMenu.Width = 55;
                //bunifuTransition1.ShowSync(panelMenu);
                buttonMenu.Image = new Bitmap(PacChat.Properties.Resources.icon_Menu);
                
            }
            //timer1.Start();
        }


        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
