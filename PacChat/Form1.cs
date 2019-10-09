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
        tabMessages _tabMessagesInstance;
        tabGroupMessages _tabGroupMessagesInstance;
        tabNoti _tabNotiInstance;
        tabSetting _tabSettingInstance;
        tabInfo _tabInfoInstance;
        public Form1()
        {
            InitializeComponent();
            init();
            buttonBigAva.Visible = false;
            buttonSmallAva.Visible = true;
            panelMenu.Width = 45;
            _tabMessagesInstance.BringToFront();
        }

        private void ButtonNotiTab_Click(object sender, EventArgs e)
        {
            _tabNotiInstance.BringToFront();
        }

        private void ButtonMessagesTab_Click(object sender, EventArgs e)
        {
            _tabMessagesInstance.BringToFront();
        }

        private void ButtonGroupMessagesTab_Click(object sender, EventArgs e)
        {
            _tabGroupMessagesInstance.BringToFront();
        }

        private void ButtonSettingTab_Click(object sender, EventArgs e)
        {
            _tabSettingInstance.BringToFront();
        }

        private void ButtonInfoTab_Click(object sender, EventArgs e)
        {
            _tabInfoInstance.BringToFront();
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width <= 45) // show menu
            {
                buttonMenu.Image = new Bitmap(PacChat.Properties.Resources.icon_Back);
                buttonBigAva.Visible = true;
                buttonSmallAva.Visible = false;
                panelMenu.Visible = false;

                buttonNotiTab.Location = new Point(0, 202);
                buttonMessagesTab.Location = new Point(0, 246);
                buttonGroupMessagesTab.Location = new Point(0, 290);

                panelMenu.Width = 180;
                bunifuTransition1.ShowSync(panelMenu);
                

            }
            else
            {
                buttonMenu.Image = new Bitmap(PacChat.Properties.Resources.icon_Menu);
                buttonBigAva.Visible = false;
                buttonSmallAva.Visible = true;
                panelMenu.Visible = false;

                buttonNotiTab.Location = new Point(0, 137);
                buttonMessagesTab.Location = new Point(0, 180);
                buttonGroupMessagesTab.Location = new Point(0, 224);

                panelMenu.Width = 45;
                bunifuTransition1.ShowSync(panelMenu);
                
            }
        }


        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void init()
        {
            _tabMessagesInstance = new tabMessages();
            _tabMessagesInstance.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_tabMessagesInstance);

            _tabGroupMessagesInstance = new tabGroupMessages();
            _tabGroupMessagesInstance.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_tabGroupMessagesInstance);


            _tabNotiInstance = new tabNoti();
            _tabNotiInstance.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_tabNotiInstance);

            _tabSettingInstance = new tabSetting();
            _tabSettingInstance.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_tabSettingInstance);

            _tabInfoInstance = new tabInfo();
            _tabInfoInstance.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_tabInfoInstance);
        }

    }
}
