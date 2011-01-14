namespace TweetShutdown
{
    partial class frmMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtPCname = new System.Windows.Forms.TextBox();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtAdvLog = new System.Windows.Forms.TextBox();
            this.btnAdvReStart = new System.Windows.Forms.Button();
            this.txtAdvPIN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdvAuthorize = new System.Windows.Forms.Button();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.tmrClearStatus = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btncontextRun = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.btncontextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrCheckTweet = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.btnRun);
            this.groupBox1.Controls.Add(this.txtPCname);
            this.groupBox1.Controls.Add(this.btnAdvanced);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtAdvLog);
            this.groupBox1.Controls.Add(this.btnAdvReStart);
            this.groupBox1.Controls.Add(this.txtAdvPIN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdvAuthorize);
            this.groupBox1.Controls.Add(this.chkAutoStart);
            this.groupBox1.Location = new System.Drawing.Point(9, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "PC name : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Username : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Username : ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(53, 127);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Status";
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(56, 76);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(220, 36);
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "Start Tweet Shutdown!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtPCname
            // 
            this.txtPCname.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TweetShutdown.Properties.Settings.Default, "pcname", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPCname.Location = new System.Drawing.Point(71, 163);
            this.txtPCname.Name = "txtPCname";
            this.txtPCname.Size = new System.Drawing.Size(92, 20);
            this.txtPCname.TabIndex = 8;
            this.txtPCname.Text = global::TweetShutdown.Properties.Settings.Default.pcname;
            this.txtPCname.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(208, 161);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(119, 23);
            this.btnAdvanced.TabIndex = 12;
            this.btnAdvanced.Text = "Advanced Options >>";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TweetShutdown.Properties.Settings.Default, "username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUsername.Location = new System.Drawing.Point(76, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(251, 20);
            this.txtUsername.TabIndex = 8;
            this.txtUsername.Text = global::TweetShutdown.Properties.Settings.Default.username;
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // txtAdvLog
            // 
            this.txtAdvLog.Location = new System.Drawing.Point(365, 99);
            this.txtAdvLog.Multiline = true;
            this.txtAdvLog.Name = "txtAdvLog";
            this.txtAdvLog.Size = new System.Drawing.Size(316, 85);
            this.txtAdvLog.TabIndex = 1;
            // 
            // btnAdvReStart
            // 
            this.btnAdvReStart.Enabled = false;
            this.btnAdvReStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvReStart.Location = new System.Drawing.Point(365, 70);
            this.btnAdvReStart.Name = "btnAdvReStart";
            this.btnAdvReStart.Size = new System.Drawing.Size(316, 23);
            this.btnAdvReStart.TabIndex = 7;
            this.btnAdvReStart.Text = "ReStart Service with PIN";
            this.btnAdvReStart.UseVisualStyleBackColor = true;
            this.btnAdvReStart.Click += new System.EventHandler(this.btnAdvReStart_Click);
            // 
            // txtAdvPIN
            // 
            this.txtAdvPIN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvPIN.Location = new System.Drawing.Point(485, 42);
            this.txtAdvPIN.Name = "txtAdvPIN";
            this.txtAdvPIN.Size = new System.Drawing.Size(196, 22);
            this.txtAdvPIN.TabIndex = 6;
            this.txtAdvPIN.TextChanged += new System.EventHandler(this.txtAdvPIN_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(362, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Authorization PIN :";
            // 
            // btnAdvAuthorize
            // 
            this.btnAdvAuthorize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvAuthorize.Location = new System.Drawing.Point(365, 13);
            this.btnAdvAuthorize.Name = "btnAdvAuthorize";
            this.btnAdvAuthorize.Size = new System.Drawing.Size(316, 23);
            this.btnAdvAuthorize.TabIndex = 4;
            this.btnAdvAuthorize.Text = "Authorize Tweet Shutdown for Custom Mention";
            this.btnAdvAuthorize.UseVisualStyleBackColor = true;
            this.btnAdvAuthorize.Click += new System.EventHandler(this.btnAdvAuthorize_Click);
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Checked = global::TweetShutdown.Properties.Settings.Default.autostart;
            this.chkAutoStart.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TweetShutdown.Properties.Settings.Default, "autostart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoStart.Location = new System.Drawing.Point(76, 53);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(182, 17);
            this.chkAutoStart.TabIndex = 3;
            this.chkAutoStart.Text = "Start Automatically with Windows";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckStateChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // tmrClearStatus
            // 
            this.tmrClearStatus.Enabled = true;
            this.tmrClearStatus.Tick += new System.EventHandler(this.tmrClearStatus_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Text = "Tweet Shutdown!";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.btnEditSettings_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btncontextRun,
            this.btnEditSettings,
            this.btncontextExit});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(194, 70);
            // 
            // btncontextRun
            // 
            this.btncontextRun.Name = "btncontextRun";
            this.btncontextRun.Size = new System.Drawing.Size(193, 22);
            this.btncontextRun.Text = "&Start Tweet Shutdown!";
            this.btncontextRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnEditSettings
            // 
            this.btnEditSettings.Name = "btnEditSettings";
            this.btnEditSettings.Size = new System.Drawing.Size(193, 22);
            this.btnEditSettings.Text = "&Edit Settings";
            this.btnEditSettings.Click += new System.EventHandler(this.btnEditSettings_Click);
            // 
            // btncontextExit
            // 
            this.btncontextExit.Name = "btncontextExit";
            this.btncontextExit.Size = new System.Drawing.Size(193, 22);
            this.btncontextExit.Text = "E&xit";
            this.btncontextExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHide
            // 
            this.btnHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(274, 12);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(29, 23);
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "_";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(309, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(29, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tweet Shutdown!";
            // 
            // tmrCheckTweet
            // 
            this.tmrCheckTweet.Interval = 15000;
            this.tmrCheckTweet.Tick += new System.EventHandler(this.tmrCheckTweet_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 237);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tweet Shutdown";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.Timer tmrClearStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnEditSettings;
        private System.Windows.Forms.ToolStripMenuItem btncontextExit;
        private System.Windows.Forms.Button btnAdvReStart;
        private System.Windows.Forms.TextBox txtAdvPIN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdvLog;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnAdvAuthorize;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem btncontextRun;
        private System.Windows.Forms.Timer tmrCheckTweet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPCname;
    }
}

