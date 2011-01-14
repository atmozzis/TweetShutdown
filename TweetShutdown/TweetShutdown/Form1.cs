using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TweetSharp;
using TweetShutdown.Properties;


namespace TweetShutdown
{
    public partial class frmMain : Form
    {
        #region P/Invoke
#if Active
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_APPWINDOW = 0x00040000;
#endif
        #endregion

        TwitterService service;
        OAuthRequestToken requestToken;

        string consumerKey = "Ya7DuTqdm59e1xOZTZBS2A";
        string consumerSecret = "aZvUPnFHsvoJAEflRfdNtRp2RcOa4TuBk1YkwHZdV3g";

        string exeDir;
        string errorlogDir;
        long since_ID;
        bool TweetShuttingDown = false;

        public frmMain()
        {
            InitializeComponent();

            InitDir();

            InitTwitterService();

            if (Properties.Settings.Default.running == true)
            {
                RunTweetShutdown();
            }

            InitMainForm();
        }

        private void InitDir()
        {
            exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            exeDir = exeDir.Remove(0, 6) + "\\";
            errorlogDir = exeDir + "error.log";
        }

        private void InitTwitterService()
        {
            service = new TwitterService(consumerKey, consumerSecret);
            service.AuthenticateWith(Properties.Settings.Default.oauthToken, Properties.Settings.Default.oauthSecret);
        }

        private void InitSince_ID()
        {
            try
            {
                since_ID = 0;
                IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsMentioningMe(1);
                foreach (var tweet in tweetsByUser)
                {
                    since_ID = tweet.Id;
                }
            }
            catch (Exception ex)
            {
                LogTwitterAccessError(ex.Message);
            }

            if (since_ID == 0)
            {
                LogTwitterAccessError("Error: Failed to get since_ID!");
            }
        }

        private void InitMainForm()
        {
            lblStatus.Text = "";
            this.Width = 350;
            this.Icon = Resources.TweetShutdown;
            notifyIcon.Icon = Resources.TweetShutdown;

            if (Properties.Settings.Default.running == true)
            {
                ChangeUIStarted();
                //HideForm();       //  Design default, not necessary
                this.Refresh();
            }
            else
            {
                ChangeUIStopped();
                ShowForm();
                this.Refresh();
            }
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
        }

        private void ShowForm()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void HideForm()
        {
            #region P/Invoke Method
#if Active
            SetWindowLong(this.Handle, GWL_EXSTYLE, (GetWindowLong(this.Handle,GWL_EXSTYLE) | WS_EX_TOOLWINDOW) & ~WS_EX_APPWINDOW);
#endif
            #endregion

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        private void ChangeUIStarted()
        {
            lblStatus.Text = "";
            txtUsername.Enabled = false;
            btnRun.Text = "Stop Tweet Shutdown!";
            btncontextRun.Text = "Stop Tweet Shutdown!";
        }

        private void ChangeUIStopped()
        {
            txtUsername.Enabled = true;
            btnRun.Text = "Start Tweet Shutdown!";
            btncontextRun.Text = "Start Tweet Shutdown!";
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.autostart == true)
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.SetValue(Application.ProductName, Application.ExecutablePath);
                regKey.Close();
            }
            else
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.DeleteValue(Application.ProductName);
                regKey.Close();
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditSettings_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (this.Width == 350)
            {
                this.Width = 705;
                btnAdvanced.Text = "Advanced Options <<";
            }
            else
            {
                this.Width = 350;
                btnAdvanced.Text = "Advanced Options >>";
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            this.notifyIcon.Visible = false;
            this.Refresh();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.username.Trim() == "")
            {
                lblStatus.Text = "Error: No Username Entered!";
                return;
            }

            if (Properties.Settings.Default.running == false)
            {
                RunTweetShutdown();
            }
            else
            {
                StopTweetShutdown();
            }
        }

        private void RunTweetShutdown()
        {
            Properties.Settings.Default.running = true;
            Save();

            ChangeUIStarted();
            tmrCheckTweet.Enabled = true;

            InitSince_ID();
        }

        private void StopTweetShutdown()
        {
            Properties.Settings.Default.running = false;
            Save();

            ChangeUIStopped();
            tmrCheckTweet.Enabled = false;
        }

        private void btnAdvAuthorize_Click(object sender, EventArgs e)
        {
            // Step 1 - Retrieve an OAuth Request Token
            requestToken = service.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());
        }

        private void btnAdvReStart_Click(object sender, EventArgs e)
        {
            // Step 3 - Exchange the Request Token for an Access Token
            String verifier = txtAdvPIN.Text; // <-- This is input into your application by your user
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(access.Token, access.TokenSecret);

            Properties.Settings.Default.oauthToken = access.Token;
            Properties.Settings.Default.oauthSecret = access.TokenSecret;
            Save();

            RunTweetShutdown();
        }

        private void txtAdvPIN_TextChanged(object sender, EventArgs e)
        {
            if (txtAdvPIN.Text.Length > 0)
            {
                btnAdvReStart.Enabled = true;
            }
            else
            {
                btnAdvReStart.Enabled = false;
            }
        }

        private void tmrCheckTweet_Tick(object sender, EventArgs e)
        {
            try
            {
                StartProcessing();
            }
            catch (Exception ex)
            {
                LogTwitterAccessError(ex.Message);
            }
        }

        private void StartProcessing()
        {
            TweetShuttingDown = false;
            IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsMentioningMeSince(since_ID, 200);
            foreach (var tweet in tweetsByUser)
            {
                if (tweet.User.ScreenName == Properties.Settings.Default.username)
                {
                    txtAdvLog.Text = tweet.User.ScreenName + "\n" + tweet.CreatedDate.ToLongTimeString() + "\n" + tweet.Text;
                    //MessageBox.Show(txtAdvLog.Text);
                    ProcessTweet(tweet);
                    if (TweetShuttingDown == true) break;
                }
            }
        }

        private void ProcessTweet(TwitterStatus Tweet)
        {
            String tweetText = Tweet.Text.Trim().ToLower();

            if (!tweetText.Contains(Properties.Settings.Default.pcname))
            {
                return;
            }

            if (tweetText.Contains("shutdown"))
            {
                ProcessTweetShutdown("-s -f -t 30"); // Shutdown
            }
            else if (tweetText.Contains("logoff"))
            {
                ProcessTweetShutdown("-l -f -t 30"); // Logoff
            }
            else if (tweetText.Contains("restart"))
            {
                ProcessTweetShutdown("-r -f -t 30"); // Restart
            }
            else if (tweetText.Contains("hibernate"))
            {
                ProcessTweetSuspend(PowerState.Hibernate);
            }
            else if (tweetText.Contains("standby")
                    || tweetText.Contains("sleep"))
            {
                ProcessTweetSuspend(PowerState.Suspend);
            }
        }

        private void ProcessTweetShutdown(String Arg)
        {
            Save();
            TweetShuttingDown = true;
            System.Diagnostics.Process.Start("shutdown", Arg);
            this.Close();
        }

        private void ProcessTweetSuspend(PowerState state)
        {
            Save();
            InitSince_ID();
            TweetShuttingDown = true;
            Application.SetSuspendState(state, true, false);
        }

        private void LogTwitterAccessError(String log)
        {
            String Date = DateTime.Now.ToShortDateString();
            String Time = DateTime.Now.ToShortTimeString();
            File.AppendAllText(errorlogDir, "\n" + Date + " " + Time + "\t" + log);
            lblStatus.Text = "Error: Failed to access Twitter!";
            StopTweetShutdown();
        }

        private void tmrClearStatus_Tick(object sender, EventArgs e)
        {

        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            int i;
        }
    }
}
