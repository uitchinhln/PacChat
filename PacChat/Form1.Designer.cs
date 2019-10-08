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
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonSmallAva = new Bunifu.Framework.UI.BunifuImageButton();
            this.buttonBigAva = new Bunifu.Framework.UI.BunifuImageButton();
            this.buttonMenu = new Bunifu.Framework.UI.BunifuImageButton();
            this.buttonGroupMessagesTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonSettingTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonInfoTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonMessagesTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.buttonNotiTab = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.Header = new System.Windows.Forms.Panel();
            this.buttonMin = new Bunifu.Framework.UI.BunifuImageButton();
            this.buttonClose = new Bunifu.Framework.UI.BunifuImageButton();
            this.panelContent = new System.Windows.Forms.Panel();
            this.bunifuTransition1 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSmallAva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBigAva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonMenu)).BeginInit();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonClose)).BeginInit();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.buttonSmallAva);
            this.panelMenu.Controls.Add(this.buttonBigAva);
            this.panelMenu.Controls.Add(this.buttonMenu);
            this.panelMenu.Controls.Add(this.buttonGroupMessagesTab);
            this.panelMenu.Controls.Add(this.buttonSettingTab);
            this.panelMenu.Controls.Add(this.buttonInfoTab);
            this.panelMenu.Controls.Add(this.buttonMessagesTab);
            this.panelMenu.Controls.Add(this.buttonNotiTab);
            this.bunifuTransition1.SetDecoration(this.panelMenu, BunifuAnimatorNS.DecorationType.None);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(45, 627);
            this.panelMenu.TabIndex = 1;
            // 
            // buttonSmallAva
            // 
            this.buttonSmallAva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.bunifuTransition1.SetDecoration(this.buttonSmallAva, BunifuAnimatorNS.DecorationType.None);
            this.buttonSmallAva.Image = global::PacChat.Properties.Resources.icon_SmallAva1;
            this.buttonSmallAva.ImageActive = null;
            this.buttonSmallAva.Location = new System.Drawing.Point(0, 90);
            this.buttonSmallAva.Name = "buttonSmallAva";
            this.buttonSmallAva.Size = new System.Drawing.Size(45, 45);
            this.buttonSmallAva.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonSmallAva.TabIndex = 7;
            this.buttonSmallAva.TabStop = false;
            this.buttonSmallAva.Zoom = 10;
            // 
            // buttonBigAva
            // 
            this.buttonBigAva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.bunifuTransition1.SetDecoration(this.buttonBigAva, BunifuAnimatorNS.DecorationType.None);
            this.buttonBigAva.Image = global::PacChat.Properties.Resources.icon_ava;
            this.buttonBigAva.ImageActive = null;
            this.buttonBigAva.Location = new System.Drawing.Point(24, 57);
            this.buttonBigAva.Name = "buttonBigAva";
            this.buttonBigAva.Size = new System.Drawing.Size(128, 128);
            this.buttonBigAva.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonBigAva.TabIndex = 7;
            this.buttonBigAva.TabStop = false;
            this.buttonBigAva.Zoom = 0;
            // 
            // buttonMenu
            // 
            this.buttonMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.bunifuTransition1.SetDecoration(this.buttonMenu, BunifuAnimatorNS.DecorationType.None);
            this.buttonMenu.Image = global::PacChat.Properties.Resources.icon_Menu;
            this.buttonMenu.ImageActive = null;
            this.buttonMenu.Location = new System.Drawing.Point(6, 14);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(30, 30);
            this.buttonMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonMenu.TabIndex = 5;
            this.buttonMenu.TabStop = false;
            this.buttonMenu.Zoom = 10;
            this.buttonMenu.Click += new System.EventHandler(this.ButtonMenu_Click);
            // 
            // buttonGroupMessagesTab
            // 
            this.buttonGroupMessagesTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonGroupMessagesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonGroupMessagesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonGroupMessagesTab.BorderRadius = 0;
            this.buttonGroupMessagesTab.ButtonText = "  Group chat";
            this.buttonGroupMessagesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.buttonGroupMessagesTab, BunifuAnimatorNS.DecorationType.None);
            this.buttonGroupMessagesTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonGroupMessagesTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonGroupMessagesTab.Iconimage = global::PacChat.Properties.Resources.icon_Group;
            this.buttonGroupMessagesTab.Iconimage_right = null;
            this.buttonGroupMessagesTab.Iconimage_right_Selected = null;
            this.buttonGroupMessagesTab.Iconimage_Selected = null;
            this.buttonGroupMessagesTab.IconMarginLeft = 0;
            this.buttonGroupMessagesTab.IconMarginRight = 0;
            this.buttonGroupMessagesTab.IconRightVisible = true;
            this.buttonGroupMessagesTab.IconRightZoom = 0D;
            this.buttonGroupMessagesTab.IconVisible = true;
            this.buttonGroupMessagesTab.IconZoom = 45D;
            this.buttonGroupMessagesTab.IsTab = true;
            this.buttonGroupMessagesTab.Location = new System.Drawing.Point(0, 290);
            this.buttonGroupMessagesTab.Name = "buttonGroupMessagesTab";
            this.buttonGroupMessagesTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonGroupMessagesTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonGroupMessagesTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonGroupMessagesTab.selected = false;
            this.buttonGroupMessagesTab.Size = new System.Drawing.Size(180, 45);
            this.buttonGroupMessagesTab.TabIndex = 10;
            this.buttonGroupMessagesTab.Text = "  Group chat";
            this.buttonGroupMessagesTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGroupMessagesTab.Textcolor = System.Drawing.Color.White;
            this.buttonGroupMessagesTab.TextFont = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGroupMessagesTab.Click += new System.EventHandler(this.ButtonGroupMessagesTab_Click);
            // 
            // buttonSettingTab
            // 
            this.buttonSettingTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonSettingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonSettingTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSettingTab.BorderRadius = 0;
            this.buttonSettingTab.ButtonText = "  Setting";
            this.buttonSettingTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.buttonSettingTab, BunifuAnimatorNS.DecorationType.None);
            this.buttonSettingTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonSettingTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonSettingTab.Iconimage = global::PacChat.Properties.Resources.icon_Setting;
            this.buttonSettingTab.Iconimage_right = null;
            this.buttonSettingTab.Iconimage_right_Selected = null;
            this.buttonSettingTab.Iconimage_Selected = null;
            this.buttonSettingTab.IconMarginLeft = 0;
            this.buttonSettingTab.IconMarginRight = 0;
            this.buttonSettingTab.IconRightVisible = true;
            this.buttonSettingTab.IconRightZoom = 0D;
            this.buttonSettingTab.IconVisible = true;
            this.buttonSettingTab.IconZoom = 45D;
            this.buttonSettingTab.IsTab = true;
            this.buttonSettingTab.Location = new System.Drawing.Point(0, 536);
            this.buttonSettingTab.Name = "buttonSettingTab";
            this.buttonSettingTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonSettingTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonSettingTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonSettingTab.selected = false;
            this.buttonSettingTab.Size = new System.Drawing.Size(180, 45);
            this.buttonSettingTab.TabIndex = 5;
            this.buttonSettingTab.Text = "  Setting";
            this.buttonSettingTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettingTab.Textcolor = System.Drawing.Color.White;
            this.buttonSettingTab.TextFont = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettingTab.Click += new System.EventHandler(this.ButtonSettingTab_Click);
            // 
            // buttonInfoTab
            // 
            this.buttonInfoTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonInfoTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonInfoTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonInfoTab.BorderRadius = 0;
            this.buttonInfoTab.ButtonText = "  Info";
            this.buttonInfoTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.buttonInfoTab, BunifuAnimatorNS.DecorationType.None);
            this.buttonInfoTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonInfoTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonInfoTab.Iconimage = global::PacChat.Properties.Resources.icon_Info;
            this.buttonInfoTab.Iconimage_right = null;
            this.buttonInfoTab.Iconimage_right_Selected = null;
            this.buttonInfoTab.Iconimage_Selected = null;
            this.buttonInfoTab.IconMarginLeft = 0;
            this.buttonInfoTab.IconMarginRight = 0;
            this.buttonInfoTab.IconRightVisible = true;
            this.buttonInfoTab.IconRightZoom = 0D;
            this.buttonInfoTab.IconVisible = true;
            this.buttonInfoTab.IconZoom = 45D;
            this.buttonInfoTab.IsTab = true;
            this.buttonInfoTab.Location = new System.Drawing.Point(0, 580);
            this.buttonInfoTab.Name = "buttonInfoTab";
            this.buttonInfoTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonInfoTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonInfoTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonInfoTab.selected = false;
            this.buttonInfoTab.Size = new System.Drawing.Size(180, 45);
            this.buttonInfoTab.TabIndex = 4;
            this.buttonInfoTab.Text = "  Info";
            this.buttonInfoTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInfoTab.Textcolor = System.Drawing.Color.White;
            this.buttonInfoTab.TextFont = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInfoTab.Click += new System.EventHandler(this.ButtonInfoTab_Click);
            // 
            // buttonMessagesTab
            // 
            this.buttonMessagesTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMessagesTab.BorderRadius = 0;
            this.buttonMessagesTab.ButtonText = "  Chat";
            this.buttonMessagesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.buttonMessagesTab, BunifuAnimatorNS.DecorationType.None);
            this.buttonMessagesTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonMessagesTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonMessagesTab.Iconimage = global::PacChat.Properties.Resources.icon_User;
            this.buttonMessagesTab.Iconimage_right = null;
            this.buttonMessagesTab.Iconimage_right_Selected = null;
            this.buttonMessagesTab.Iconimage_Selected = null;
            this.buttonMessagesTab.IconMarginLeft = 0;
            this.buttonMessagesTab.IconMarginRight = 0;
            this.buttonMessagesTab.IconRightVisible = true;
            this.buttonMessagesTab.IconRightZoom = 0D;
            this.buttonMessagesTab.IconVisible = true;
            this.buttonMessagesTab.IconZoom = 45D;
            this.buttonMessagesTab.IsTab = true;
            this.buttonMessagesTab.Location = new System.Drawing.Point(0, 246);
            this.buttonMessagesTab.Name = "buttonMessagesTab";
            this.buttonMessagesTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonMessagesTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonMessagesTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonMessagesTab.selected = true;
            this.buttonMessagesTab.Size = new System.Drawing.Size(180, 45);
            this.buttonMessagesTab.TabIndex = 2;
            this.buttonMessagesTab.Text = "  Chat";
            this.buttonMessagesTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMessagesTab.Textcolor = System.Drawing.Color.White;
            this.buttonMessagesTab.TextFont = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMessagesTab.Click += new System.EventHandler(this.ButtonMessagesTab_Click);
            // 
            // buttonNotiTab
            // 
            this.buttonNotiTab.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonNotiTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonNotiTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNotiTab.BorderRadius = 0;
            this.buttonNotiTab.ButtonText = "  Notification";
            this.buttonNotiTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.buttonNotiTab, BunifuAnimatorNS.DecorationType.None);
            this.buttonNotiTab.DisabledColor = System.Drawing.Color.Gray;
            this.buttonNotiTab.Iconcolor = System.Drawing.Color.Transparent;
            this.buttonNotiTab.Iconimage = global::PacChat.Properties.Resources.icon_Noti;
            this.buttonNotiTab.Iconimage_right = null;
            this.buttonNotiTab.Iconimage_right_Selected = null;
            this.buttonNotiTab.Iconimage_Selected = null;
            this.buttonNotiTab.IconMarginLeft = 0;
            this.buttonNotiTab.IconMarginRight = 0;
            this.buttonNotiTab.IconRightVisible = true;
            this.buttonNotiTab.IconRightZoom = 0D;
            this.buttonNotiTab.IconVisible = true;
            this.buttonNotiTab.IconZoom = 45D;
            this.buttonNotiTab.IsTab = true;
            this.buttonNotiTab.Location = new System.Drawing.Point(0, 202);
            this.buttonNotiTab.Name = "buttonNotiTab";
            this.buttonNotiTab.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.buttonNotiTab.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(159)))), ((int)(((byte)(217)))));
            this.buttonNotiTab.OnHoverTextColor = System.Drawing.Color.White;
            this.buttonNotiTab.selected = false;
            this.buttonNotiTab.Size = new System.Drawing.Size(180, 45);
            this.buttonNotiTab.TabIndex = 1;
            this.buttonNotiTab.Text = "  Notification";
            this.buttonNotiTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNotiTab.Textcolor = System.Drawing.Color.White;
            this.buttonNotiTab.TextFont = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNotiTab.Click += new System.EventHandler(this.ButtonNotiTab_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.Header;
            this.bunifuDragControl1.Vertical = true;
            // 
            // Header
            // 
            this.Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Header.Controls.Add(this.buttonMin);
            this.Header.Controls.Add(this.buttonClose);
            this.bunifuTransition1.SetDecoration(this.Header, BunifuAnimatorNS.DecorationType.None);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1032, 38);
            this.Header.TabIndex = 6;
            // 
            // buttonMin
            // 
            this.buttonMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bunifuTransition1.SetDecoration(this.buttonMin, BunifuAnimatorNS.DecorationType.None);
            this.buttonMin.Image = global::PacChat.Properties.Resources.icon_Minimize;
            this.buttonMin.ImageActive = null;
            this.buttonMin.Location = new System.Drawing.Point(973, 8);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(19, 19);
            this.buttonMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonMin.TabIndex = 5;
            this.buttonMin.TabStop = false;
            this.buttonMin.Zoom = 10;
            this.buttonMin.Click += new System.EventHandler(this.ButtonMin_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bunifuTransition1.SetDecoration(this.buttonClose, BunifuAnimatorNS.DecorationType.None);
            this.buttonClose.Image = global::PacChat.Properties.Resources.icon_Close;
            this.buttonClose.ImageActive = null;
            this.buttonClose.Location = new System.Drawing.Point(999, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(19, 19);
            this.buttonClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonClose.TabIndex = 5;
            this.buttonClose.TabStop = false;
            this.buttonClose.Zoom = 10;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelContent.Controls.Add(this.Header);
            this.bunifuTransition1.SetDecoration(this.panelContent, BunifuAnimatorNS.DecorationType.None);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(45, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1032, 627);
            this.panelContent.TabIndex = 2;
            // 
            // bunifuTransition1
            // 
            this.bunifuTransition1.AnimationType = BunifuAnimatorNS.AnimationType.Transparent;
            this.bunifuTransition1.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 1F;
            this.bunifuTransition1.DefaultAnimation = animation1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 627);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.bunifuTransition1.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonSmallAva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBigAva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonMenu)).EndInit();
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonClose)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panelMenu;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuFlatButton buttonNotiTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonSettingTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonInfoTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonGroupMessagesTab;
        private Bunifu.Framework.UI.BunifuFlatButton buttonMessagesTab;
        private System.Windows.Forms.Panel panelContent;
        private Bunifu.Framework.UI.BunifuImageButton buttonMenu;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition1;
        private Bunifu.Framework.UI.BunifuImageButton buttonClose;
        private Bunifu.Framework.UI.BunifuImageButton buttonMin;
        private System.Windows.Forms.Panel Header;
        private Bunifu.Framework.UI.BunifuImageButton buttonBigAva;
        private Bunifu.Framework.UI.BunifuImageButton buttonSmallAva;
    }
}

