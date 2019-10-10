namespace PacChat
{
    partial class tabBubbleMess
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMessages = new System.Windows.Forms.Label();
            this.panelAva = new System.Windows.Forms.Panel();
            this.panelAvaImage = new System.Windows.Forms.Panel();
            this.picboxAvaImage = new System.Windows.Forms.PictureBox();
            this.panelAva.SuspendLayout();
            this.panelAvaImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxAvaImage)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMessages
            // 
            this.labelMessages.AutoSize = true;
            this.labelMessages.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelMessages.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.labelMessages.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelMessages.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessages.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelMessages.Location = new System.Drawing.Point(168, 0);
            this.labelMessages.MaximumSize = new System.Drawing.Size(140, 0);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Padding = new System.Windows.Forms.Padding(3);
            this.labelMessages.Size = new System.Drawing.Size(139, 150);
            this.labelMessages.TabIndex = 3;
            this.labelMessages.Text = " This is a sample text message. This is a sample text message. This is a sample t" +
    "ext message. \r\n\r\nThis is a sample text message. \r\n";
            // 
            // panelAva
            // 
            this.panelAva.Controls.Add(this.panelAvaImage);
            this.panelAva.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAva.Location = new System.Drawing.Point(307, 0);
            this.panelAva.Name = "panelAva";
            this.panelAva.Size = new System.Drawing.Size(56, 159);
            this.panelAva.TabIndex = 2;
            // 
            // panelAvaImage
            // 
            this.panelAvaImage.BackColor = System.Drawing.Color.White;
            this.panelAvaImage.Controls.Add(this.picboxAvaImage);
            this.panelAvaImage.Location = new System.Drawing.Point(8, 0);
            this.panelAvaImage.Name = "panelAvaImage";
            this.panelAvaImage.Padding = new System.Windows.Forms.Padding(1);
            this.panelAvaImage.Size = new System.Drawing.Size(32, 32);
            this.panelAvaImage.TabIndex = 1;
            // 
            // picboxAvaImage
            // 
            this.picboxAvaImage.BackColor = System.Drawing.Color.Transparent;
            this.picboxAvaImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picboxAvaImage.Image = global::PacChat.Properties.Resources.icon_ava_32;
            this.picboxAvaImage.Location = new System.Drawing.Point(1, 1);
            this.picboxAvaImage.Name = "picboxAvaImage";
            this.picboxAvaImage.Size = new System.Drawing.Size(30, 30);
            this.picboxAvaImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picboxAvaImage.TabIndex = 0;
            this.picboxAvaImage.TabStop = false;
            // 
            // tabBubbleMess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelMessages);
            this.Controls.Add(this.panelAva);
            this.Name = "tabBubbleMess";
            this.Size = new System.Drawing.Size(363, 159);
            this.panelAva.ResumeLayout(false);
            this.panelAvaImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxAvaImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessages;
        private System.Windows.Forms.Panel panelAva;
        private System.Windows.Forms.Panel panelAvaImage;
        private System.Windows.Forms.PictureBox picboxAvaImage;
    }
}
