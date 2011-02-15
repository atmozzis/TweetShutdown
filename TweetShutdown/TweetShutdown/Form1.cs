using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TweetSharp;
using TweetShutdown.Properties;

namespace TweetShutdown
{
    public partial class frmMain : Form
    {
        TwitterService service;
        OAuthRequestToken requestToken;

        String consumerKey = "Ya7DuTqdm59e1xOZTZBS2A";
        String consumerSecret = "aZvUPnFHsvoJAEflRfdNtRp2RcOa4TuBk1YkwHZdV3g";

        Userdata userdata;

        String AppdataDir, errorlogDir, savelogDir;
        long since_ID;
        bool TweetShuttingDown = false;

        public frmMain()
        {
            InitializeComponent();

            InitDir();

            InitUserData();

            InitTwitterService();

            if (userdata.Running == true)
            {
                RunTweetShutdown();
            }

            InitMainForm();
        }

        private void InitDir()
        {
            AppdataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TweetShutdown\\";

            errorlogDir = AppdataDir + "error.log";
            savelogDir = AppdataDir + "userdata.log";
        }

        private void InitUserData()
        {
            userdata = new Userdata(savelogDir);
        }

        private void InitTwitterService()
        {
            service = new TwitterService(consumerKey, consumerSecret);
            service.AuthenticateWith(userdata.OAuthToken, userdata.OAuthSecret);
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

            this.txtUsername.DataBindings.Add("Text", userdata, "Username");
            this.txtPCname.DataBindings.Add("Text", userdata, "PCname");
            this.chkAutoStart.DataBindings.Add("Checked", userdata, "AutoStart");
        }

        private void Save()
        {
            String error = userdata.Save(savelogDir);

            if (!String.IsNullOrEmpty(error))
            {
                String Date = DateTime.Now.ToShortDateString();
                String Time = DateTime.Now.ToShortTimeString();
                File.AppendAllText(errorlogDir, "\r\n" + Date + " " + Time + "\t" + error);
                lblStatus.Text = "Error: " + error;
            }
        }

        private void ShowForm()
        {
            this.Show();
            this.Focus();
        }

        private void HideForm()
        {
            this.Hide();
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
            if (userdata.AutoStart == true)
            {
                Helper.SetAutoStart("TweetShutdown", Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                Helper.UnSetAutoStart("TweetShutdown");
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
            if (userdata.Username.Trim() == "")
            {
                lblStatus.Text = "Error: No Username Entered!";
                return;
            }

            if (userdata.Running == false)
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
            userdata.Running = true;
            Save();

            ChangeUIStarted();
            tmrCheckTweet.Enabled = true;

            InitSince_ID();
        }

        private void StopTweetShutdown()
        {
            userdata.Running = false;
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

            userdata.OAuthToken = access.Token;
            userdata.OAuthSecret = access.TokenSecret;
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
                if (tweet.User.ScreenName == userdata.Username)
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

            if (!tweetText.Contains(userdata.PCname))
            {
                return;
            }

            if (tweetText.Contains("shutdown"))
            {
                ProcessTweetShutdown("-s -f -t 30"); // Shutdown
            }
            else if (tweetText.Contains("restart"))
            {
                ProcessTweetShutdown("-r -f -t 30"); // Restart
            }
            else if (tweetText.Contains("logoff"))
            {
                ProcessTweetShutdown("-l -f -t 30"); // Logoff
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
            File.AppendAllText(errorlogDir, "\r\n" + Date + " " + Time + "\t" + log);
            lblStatus.Text = "Error: Failed to access Twitter!";
            StopTweetShutdown();
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            Save();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (userdata.Running == true)
            {
                ChangeUIStarted();
                HideForm();
            }
            else
            {
                ChangeUIStopped();
                //ShowForm();       // Initial State
            }
        }
    }
}
