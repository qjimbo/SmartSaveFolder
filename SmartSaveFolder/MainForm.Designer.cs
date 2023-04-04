namespace SmartSaveFolder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonOriginal = new System.Windows.Forms.Button();
            this.buttonCurrent = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonExit = new System.Windows.Forms.Button();
            this.pictureBoxLink = new System.Windows.Forms.PictureBox();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLink)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOriginal
            // 
            this.buttonOriginal.Location = new System.Drawing.Point(12, 12);
            this.buttonOriginal.Name = "buttonOriginal";
            this.buttonOriginal.Size = new System.Drawing.Size(205, 23);
            this.buttonOriginal.TabIndex = 0;
            this.buttonOriginal.Text = "Open Original Save Games Folder";
            this.buttonOriginal.UseVisualStyleBackColor = true;
            this.buttonOriginal.Click += new System.EventHandler(this.buttonOriginal_Click);
            // 
            // buttonCurrent
            // 
            this.buttonCurrent.Enabled = false;
            this.buttonCurrent.Location = new System.Drawing.Point(223, 12);
            this.buttonCurrent.Name = "buttonCurrent";
            this.buttonCurrent.Size = new System.Drawing.Size(205, 23);
            this.buttonCurrent.TabIndex = 1;
            this.buttonCurrent.Text = "Open Current Save Games Folder";
            this.buttonCurrent.UseVisualStyleBackColor = true;
            this.buttonCurrent.Click += new System.EventHandler(this.buttonCurrent_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 41);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(415, 112);
            this.textBox.TabIndex = 2;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "SmartSaveFolder";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(170, 159);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(99, 23);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // pictureBoxLink
            // 
            this.pictureBoxLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxLink.Image = global::SmartSaveFolder.Properties.Resources.nmsr;
            this.pictureBoxLink.Location = new System.Drawing.Point(12, 156);
            this.pictureBoxLink.Name = "pictureBoxLink";
            this.pictureBoxLink.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLink.TabIndex = 4;
            this.pictureBoxLink.TabStop = false;
            this.pictureBoxLink.Click += new System.EventHandler(this.pictureBoxLink_Click);
            this.pictureBoxLink.MouseHover += new System.EventHandler(this.pictureBoxLink_MouseHover);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(334, 162);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(93, 17);
            this.chkStartup.TabIndex = 5;
            this.chkStartup.Text = "Run at startup";
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(39, 163);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(34, 13);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "v1.05";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 189);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.pictureBoxLink);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.buttonCurrent);
            this.Controls.Add(this.buttonOriginal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartSaveFolder for No Man\'s Sky";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLink)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOriginal;
        private System.Windows.Forms.Button buttonCurrent;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.PictureBox pictureBoxLink;
        private System.Windows.Forms.CheckBox chkStartup;
        private System.Windows.Forms.Label lblVersion;
    }
}

