namespace PacChat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.Header = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonGroupMessagesTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonMenuTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonSettingTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonInfoTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonMessagesTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonNotiTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.tabMessages = new System.Windows.Forms.Panel();
            this.tabInfoInstance = new PacChat.tabInfo();
            this.tabSettingInstance = new PacChat.tabSetting();
            this.tabNotiInstance = new PacChat.tabNoti();
            this.tabGroupMessagesInstance = new PacChat.tabGroupMessages();
            this.tabMessagesInstance = new PacChat.tabMessages();
            this.panelMenu.SuspendLayout();
            this.tabMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(220)))), ((int)(((byte)(217)))));
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(938, 32);
            this.Header.TabIndex = 0;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.panelMenu.Controls.Add(this.buttonGroupMessagesTab);
            this.panelMenu.Controls.Add(this.buttonMenuTab);
            this.panelMenu.Controls.Add(this.buttonSettingTab);
            this.panelMenu.Controls.Add(this.buttonInfoTab);
            this.panelMenu.Controls.Add(this.buttonMessagesTab);
            this.panelMenu.Controls.Add(this.buttonNotiTab);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 32);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(55, 548);
            this.panelMenu.TabIndex = 1;
            // 
            // buttonGroupMessagesTab
            // 
            this.buttonGroupMessagesTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonGroupMessagesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonGroupMessagesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonGroupMessagesTab.BorderRadius = 0;
            this.buttonGroupMessagesTab.ButtonText = "";
            this.buttonGroupMessagesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGroupMessagesTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonGroupMessagesTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonGroupMessagesTab.Iconimage = global::PacChat.Properties.Resources.icon_Group;
            this.buttonGroupMessagesTab.Iconimage_right = null;
            this.buttonGroupMessagesTab.Iconimage_right_Selected = null;
            this.buttonGroupMessagesTab.Iconimage_Selected = null;
            this.buttonGroupMessagesTab.IconMarginLeft = 13;
            this.buttonGroupMessagesTab.IconMarginRight = 0;
            this.buttonGroupMessagesTab.IconRightVisible = true;
            this.buttonGroupMessagesTab.IconRightZoom = 0D;
            this.buttonGroupMessagesTab.IconVisible = true;
            this.buttonGroupMessagesTab.IconZoom = 60D;
            this.buttonGroupMessagesTab.IsTab = true;
            this.buttonGroupMessagesTab.Location = new System.Drawing.Point(0, 198);
            this.buttonGroupMessagesTab.Name = "buttonGroupMessagesTab";
            this.buttonGroupMessagesTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonGroupMessagesTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonGroupMessagesTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonGroupMessagesTab.selected = false;
            this.buttonGroupMessagesTab.Size = new System.Drawing.Size(56, 67);
            this.buttonGroupMessagesTab.TabIndex = 10;
            this.buttonGroupMessagesTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGroupMessagesTab.Textcolor = System.Drawing.Color.White;
            this.buttonGroupMessagesTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGroupMessagesTab.Click += new System.EventHandler(this.ButtonGroupMessagesTab_Click);
            // 
            // buttonMenuTab
            // 
            this.buttonMenuTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMenuTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonMenuTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMenuTab.BorderRadius = 0;
            this.buttonMenuTab.ButtonText = "";
            this.buttonMenuTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMenuTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonMenuTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonMenuTab.Iconimage = ((System.Drawing.Image)(resources.GetObject("buttonMenuTab.Iconimage")));
            this.buttonMenuTab.Iconimage_right = null;
            this.buttonMenuTab.Iconimage_right_Selected = null;
            this.buttonMenuTab.Iconimage_Selected = null;
            this.buttonMenuTab.IconMarginLeft = 13;
            this.buttonMenuTab.IconMarginRight = 0;
            this.buttonMenuTab.IconRightVisible = true;
            this.buttonMenuTab.IconRightZoom = 0D;
            this.buttonMenuTab.IconVisible = true;
            this.buttonMenuTab.IconZoom = 60D;
            this.buttonMenuTab.IsTab = true;
            this.buttonMenuTab.Location = new System.Drawing.Point(0, 0);
            this.buttonMenuTab.Name = "buttonMenuTab";
            this.buttonMenuTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonMenuTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMenuTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonMenuTab.selected = false;
            this.buttonMenuTab.Size = new System.Drawing.Size(56, 67);
            this.buttonMenuTab.TabIndex = 0;
            this.buttonMenuTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuTab.Textcolor = System.Drawing.Color.White;
            this.buttonMenuTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenuTab.Click += new System.EventHandler(this.ButtonMenuTab_Click);
            // 
            // buttonSettingTab
            // 
            this.buttonSettingTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonSettingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonSettingTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSettingTab.BorderRadius = 0;
            this.buttonSettingTab.ButtonText = "";
            this.buttonSettingTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSettingTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonSettingTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonSettingTab.Iconimage = global::PacChat.Properties.Resources.icon_Setting;
            this.buttonSettingTab.Iconimage_right = null;
            this.buttonSettingTab.Iconimage_right_Selected = null;
            this.buttonSettingTab.Iconimage_Selected = null;
            this.buttonSettingTab.IconMarginLeft = 15;
            this.buttonSettingTab.IconMarginRight = 0;
            this.buttonSettingTab.IconRightVisible = true;
            this.buttonSettingTab.IconRightZoom = 0D;
            this.buttonSettingTab.IconVisible = true;
            this.buttonSettingTab.IconZoom = 45D;
            this.buttonSettingTab.IsTab = true;
            this.buttonSettingTab.Location = new System.Drawing.Point(0, 453);
            this.buttonSettingTab.Name = "buttonSettingTab";
            this.buttonSettingTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonSettingTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonSettingTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonSettingTab.selected = false;
            this.buttonSettingTab.Size = new System.Drawing.Size(56, 48);
            this.buttonSettingTab.TabIndex = 5;
            this.buttonSettingTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettingTab.Textcolor = System.Drawing.Color.White;
            this.buttonSettingTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettingTab.Click += new System.EventHandler(this.ButtonSettingTab_Click);
            // 
            // buttonInfoTab
            // 
            this.buttonInfoTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonInfoTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonInfoTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonInfoTab.BorderRadius = 0;
            this.buttonInfoTab.ButtonText = "";
            this.buttonInfoTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInfoTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonInfoTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonInfoTab.Iconimage = global::PacChat.Properties.Resources.icon_Info;
            this.buttonInfoTab.Iconimage_right = null;
            this.buttonInfoTab.Iconimage_right_Selected = null;
            this.buttonInfoTab.Iconimage_Selected = null;
            this.buttonInfoTab.IconMarginLeft = 15;
            this.buttonInfoTab.IconMarginRight = 0;
            this.buttonInfoTab.IconRightVisible = true;
            this.buttonInfoTab.IconRightZoom = 0D;
            this.buttonInfoTab.IconVisible = true;
            this.buttonInfoTab.IconZoom = 45D;
            this.buttonInfoTab.IsTab = true;
            this.buttonInfoTab.Location = new System.Drawing.Point(0, 500);
            this.buttonInfoTab.Name = "buttonInfoTab";
            this.buttonInfoTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonInfoTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonInfoTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonInfoTab.selected = false;
            this.buttonInfoTab.Size = new System.Drawing.Size(56, 48);
            this.buttonInfoTab.TabIndex = 4;
            this.buttonInfoTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInfoTab.Textcolor = System.Drawing.Color.White;
            this.buttonInfoTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInfoTab.Click += new System.EventHandler(this.ButtonInfoTab_Click);
            // 
            // buttonMessagesTab
            // 
            this.buttonMessagesTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMessagesTab.BorderRadius = 0;
            this.buttonMessagesTab.ButtonText = "";
            this.buttonMessagesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMessagesTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonMessagesTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonMessagesTab.Iconimage = global::PacChat.Properties.Resources.icon_User;
            this.buttonMessagesTab.Iconimage_right = null;
            this.buttonMessagesTab.Iconimage_right_Selected = null;
            this.buttonMessagesTab.Iconimage_Selected = null;
            this.buttonMessagesTab.IconMarginLeft = 13;
            this.buttonMessagesTab.IconMarginRight = 0;
            this.buttonMessagesTab.IconRightVisible = true;
            this.buttonMessagesTab.IconRightZoom = 0D;
            this.buttonMessagesTab.IconVisible = true;
            this.buttonMessagesTab.IconZoom = 60D;
            this.buttonMessagesTab.IsTab = true;
            this.buttonMessagesTab.Location = new System.Drawing.Point(0, 132);
            this.buttonMessagesTab.Name = "buttonMessagesTab";
            this.buttonMessagesTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonMessagesTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonMessagesTab.selected = true;
            this.buttonMessagesTab.Size = new System.Drawing.Size(56, 67);
            this.buttonMessagesTab.TabIndex = 2;
            this.buttonMessagesTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMessagesTab.Textcolor = System.Drawing.Color.White;
            this.buttonMessagesTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMessagesTab.Click += new System.EventHandler(this.ButtonMessagesTab_Click);
            // 
            // buttonNotiTab
            // 
            this.buttonNotiTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonNotiTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonNotiTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNotiTab.BorderRadius = 0;
            this.buttonNotiTab.ButtonText = "";
            this.buttonNotiTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNotiTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonNotiTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonNotiTab.Iconimage = global::PacChat.Properties.Resources.icon_Noti;
            this.buttonNotiTab.Iconimage_right = null;
            this.buttonNotiTab.Iconimage_right_Selected = null;
            this.buttonNotiTab.Iconimage_Selected = null;
            this.buttonNotiTab.IconMarginLeft = 13;
            this.buttonNotiTab.IconMarginRight = 0;
            this.buttonNotiTab.IconRightVisible = true;
            this.buttonNotiTab.IconRightZoom = 0D;
            this.buttonNotiTab.IconVisible = true;
            this.buttonNotiTab.IconZoom = 60D;
            this.buttonNotiTab.IsTab = true;
            this.buttonNotiTab.Location = new System.Drawing.Point(0, 66);
            this.buttonNotiTab.Name = "buttonNotiTab";
            this.buttonNotiTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonNotiTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonNotiTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonNotiTab.selected = false;
            this.buttonNotiTab.Size = new System.Drawing.Size(56, 67);
            this.buttonNotiTab.TabIndex = 1;
            this.buttonNotiTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNotiTab.Textcolor = System.Drawing.Color.White;
            this.buttonNotiTab.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNotiTab.Click += new System.EventHandler(this.ButtonNotiTab_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.Header;
            this.bunifuDragControl1.Vertical = true;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.tabInfoInstance);
            this.tabMessages.Controls.Add(this.tabSettingInstance);
            this.tabMessages.Controls.Add(this.tabNotiInstance);
            this.tabMessages.Controls.Add(this.tabGroupMessagesInstance);
            this.tabMessages.Controls.Add(this.tabMessagesInstance);
            this.tabMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMessages.Location = new System.Drawing.Point(55, 32);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Size = new System.Drawing.Size(883, 548);
            this.tabMessages.TabIndex = 2;
            // 
            // tabInfoInstance
            // 
            this.tabInfoInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfoInstance.Location = new System.Drawing.Point(0, 0);
            this.tabInfoInstance.Name = "tabInfoInstance";
            this.tabInfoInstance.Size = new System.Drawing.Size(883, 548);
            this.tabInfoInstance.TabIndex = 4;
            // 
            // tabSettingInstance
            // 
            this.tabSettingInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSettingInstance.Location = new System.Drawing.Point(0, 0);
            this.tabSettingInstance.Name = "tabSettingInstance";
            this.tabSettingInstance.Size = new System.Drawing.Size(883, 548);
            this.tabSettingInstance.TabIndex = 3;
            // 
            // tabNotiInstance
            // 
            this.tabNotiInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNotiInstance.Location = new System.Drawing.Point(0, 0);
            this.tabNotiInstance.Name = "tabNotiInstance";
            this.tabNotiInstance.Size = new System.Drawing.Size(883, 548);
            this.tabNotiInstance.TabIndex = 2;
            // 
            // tabGroupMessagesInstance
            // 
            this.tabGroupMessagesInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGroupMessagesInstance.Location = new System.Drawing.Point(0, 0);
            this.tabGroupMessagesInstance.Name = "tabGroupMessagesInstance";
            this.tabGroupMessagesInstance.Size = new System.Drawing.Size(883, 548);
            this.tabGroupMessagesInstance.TabIndex = 1;
            // 
            // tabMessagesInstance
            // 
            this.tabMessagesInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMessagesInstance.Location = new System.Drawing.Point(0, 0);
            this.tabMessagesInstance.Name = "tabMessagesInstance";
            this.tabMessagesInstance.Size = new System.Drawing.Size(883, 548);
            this.tabMessagesInstance.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 580);
            this.Controls.Add(this.tabMessages);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            this.tabMessages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel Header;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuFlatButton buttonNotiTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonMenuTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonSettingTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonInfoTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonGroupMessagesTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonMessagesTab;
        private System.Windows.Forms.Panel tabMessages;
        private tabInfo tabInfoInstance;
        private tabSetting tabSettingInstance;
        private tabNoti tabNotiInstance;
        private tabGroupMessages tabGroupMessagesInstance;
        private tabMessages tabMessagesInstance;
    }
}

