﻿namespace TweetShutdown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btncontextRun = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btncontextExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 191);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "PC name : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username : ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(53, 127);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status";
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(56, 76);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(220, 36);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Start Tweet Shutdown!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtPCname
            // 
            this.txtPCname.Location = new System.Drawing.Point(71, 163);
            this.txtPCname.Name = "txtPCname";
            this.txtPCname.Size = new System.Drawing.Size(92, 20);
            this.txtPCname.TabIndex = 6;
            this.txtPCname.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(197, 161);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(119, 23);
            this.btnAdvanced.TabIndex = 7;
            this.btnAdvanced.Text = "Advanced Options >>";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(76, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(182, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // txtAdvLog
            // 
            this.txtAdvLog.Location = new System.Drawing.Point(365, 99);
            this.txtAdvLog.Multiline = true;
            this.txtAdvLog.Name = "txtAdvLog";
            this.txtAdvLog.Size = new System.Drawing.Size(316, 85);
            this.txtAdvLog.TabIndex = 12;
            // 
            // btnAdvReStart
            // 
            this.btnAdvReStart.Enabled = false;
            this.btnAdvReStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvReStart.Location = new System.Drawing.Point(365, 70);
            this.btnAdvReStart.Name = "btnAdvReStart";
            this.btnAdvReStart.Size = new System.Drawing.Size(316, 23);
            this.btnAdvReStart.TabIndex = 11;
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
            this.txtAdvPIN.TabIndex = 10;
            this.txtAdvPIN.TextChanged += new System.EventHandler(this.txtAdvPIN_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(362, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Authorization PIN :";
            // 
            // btnAdvAuthorize
            // 
            this.btnAdvAuthorize.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvAuthorize.Location = new System.Drawing.Point(365, 13);
            this.btnAdvAuthorize.Name = "btnAdvAuthorize";
            this.btnAdvAuthorize.Size = new System.Drawing.Size(316, 23);
            this.btnAdvAuthorize.TabIndex = 8;
            this.btnAdvAuthorize.Text = "Authorize Tweet Shutdown for Custom Mention";
            this.btnAdvAuthorize.UseVisualStyleBackColor = true;
            this.btnAdvAuthorize.Click += new System.EventHandler(this.btnAdvAuthorize_Click);
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(76, 53);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(182, 17);
            this.chkAutoStart.TabIndex = 2;
            this.chkAutoStart.Text = "Start Automatically with Windows";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Tweet Shutdown!";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.btnEditSettings_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btncontextRun,
            this.btnEditSettings,
            this.toolStripSeparator1,
            this.btncontextExit});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(187, 76);
            // 
            // btncontextRun
            // 
            this.btncontextRun.Name = "btncontextRun";
            this.btncontextRun.Size = new System.Drawing.Size(186, 22);
            this.btncontextRun.Text = "&Start Tweet Shutdown!";
            this.btncontextRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnEditSettings
            // 
            this.btnEditSettings.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditSettings.Name = "btnEditSettings";
            this.btnEditSettings.Size = new System.Drawing.Size(186, 22);
            this.btnEditSettings.Text = "&Edit Settings";
            this.btnEditSettings.Click += new System.EventHandler(this.btnEditSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // btncontextExit
            // 
            this.btncontextExit.Name = "btncontextExit";
            this.btncontextExit.Size = new System.Drawing.Size(186, 22);
            this.btncontextExit.Text = "E&xit";
            this.btncontextExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.ClientSize = new System.Drawing.Size(705, 209);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TweetShutdown!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnEditSettings;
        private System.Windows.Forms.ToolStripMenuItem btncontextExit;
        private System.Windows.Forms.Button btnAdvReStart;
        private System.Windows.Forms.TextBox txtAdvPIN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdvLog;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnAdvAuthorize;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem btncontextRun;
        private System.Windows.Forms.Timer tmrCheckTweet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPCname;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

