using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TweetSharp;
using System.Diagnostics;
using System.IO;
using TweetShutdown.Properties;

namespace TweetShutdown
{
    public partial class frmMain : Form
    {
        TwitterService service;
        OAuthRequestToken requestToken;


        string consumerKey = "Ya7DuTqdm59e1xOZTZBS2A";
        string consumerSecret = "aZvUPnFHsvoJAEflRfdNtRp2RcOa4TuBk1YkwHZdV3g";

        string exeDir;
        string errorlogDir;
        long since_ID;

        public frmMain()
        {
            InitializeComponent();

            InitDir();

            InitTwitterService();

            if (Properties.Settings.Default.running == true)
                InitSince_ID();

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
                LogError(ex.Message);
            }

            if (since_ID == 0)
            {
                LogError("Error: Failed to get since_ID!");
            }
        }

        private void InitMainForm()
        {
            lblStatus.Text = "";
            this.Width = 350;
            notifyIcon.Icon = Resources.twitterico;
            if (Properties.Settings.Default.running == true)
            {
                ChangeUIStarted();
                this.Refresh();
                HideForm();
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
        }

        private void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
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
            this.notifyIcon.Visible = false;
            this.Refresh();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
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

        private void ProcessTweetSuccess()
        {
            Properties.Settings.Default.Save();
            this.Close();
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
                LogError(ex.Message);
            }
        }

        private void StartProcessing()
        {
            IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsMentioningMeSince(since_ID, 200);
            foreach (var tweet in tweetsByUser)
            {
                if (tweet.User.ScreenName == Properties.Settings.Default.username)
                {
                    txtAdvLog.Text = tweet.User + "\n" + tweet.CreatedDate.ToLongTimeString() + "\n" + tweet.Text;
                    MessageBox.Show(txtAdvLog.Text);
                    //ProcessTweet(tweet);
                }
            }
        }

        private void ProcessTweet(TwitterStatus Tweet)
        {
            String tweetText = Tweet.Text.Trim().ToLower();

            if (tweetText.Contains("shutdown"))
            {
                System.Diagnostics.Process.Start("shutdown", "-s -f -t 100"); // Shutdown
                ProcessTweetSuccess();
            }
            else if (tweetText.Contains("logoff"))
            {
                System.Diagnostics.Process.Start("shutdown", "-l -f -t 100"); // Logoff
                ProcessTweetSuccess();
            }
            else if (tweetText.Contains("restart"))
            {
                System.Diagnostics.Process.Start("shutdown", "-r -f -t 100"); // Restart
                ProcessTweetSuccess();
            }
        }

        private void LogError(String log)
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
    }
}
