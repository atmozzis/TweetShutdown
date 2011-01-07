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

namespace TweetShutdown
{
    public partial class frmTweetMyPc : Form
    {
        TwitterService service;
        OAuthRequestToken requestToken;
        OAuthAccessToken access;

        string atmdir;
        string logFileDir;
        string accesstokenFileDir;

        public frmTweetMyPc()
        {
            InitializeComponent();

            atmdir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            atmdir = atmdir.Remove(0, 6);
            atmdir += "\\";
            logFileDir = atmdir + "logs.txt";
            accesstokenFileDir = atmdir + "acc.tkt";

            // Pass your credentials to the service
            service = new TwitterService("245BuFYM4biAh8XAjF4A", "Fb3vuG7hKVYR0OCEHMWaictTkWQTjw1V7lLfN5ok");

            // User authenticates using the previous Access Token
            access = new OAuthAccessToken();
            access.ScreenName = Properties.Settings.Default.OAuthScreenName;
            access.Token = Properties.Settings.Default.OAuthToken;
            access.TokenSecret = Properties.Settings.Default.OauthTokenSecret;
            access.UserId = Properties.Settings.Default.OauthUserId;
            service.AuthenticateWith(access.Token, access.TokenSecret);

            if (Properties.Settings.Default.Running == false) ShowHideWindows();
        }

        private void chkStartAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStartAutomatic.Checked == true)
            {
                Properties.Settings.Default.AutomaticStart = true;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.SetValue(Application.ProductName, Application.ExecutablePath);
                regKey.Close();   
            }
            else
            {
                Properties.Settings.Default.AutomaticStart = false;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.DeleteValue(Application.ProductName);
                regKey.Close();
            }
        }

        private void tmrTweet_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Running == true)
            {
                try
                {
                    IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsMentioningMe(200);
                    //IEnumerable<TwitterDirectMessage> DMstoUser = service.ListDirectMessagesReceived(800);
                    foreach (var tweet in tweetsByUser)
                    {
                        if (tweet.User.ScreenName == txtMention.Text)
                        {
                            txtResult.Text = tweet.User + "\n" + tweet.Text + "\n" + tweet.CreatedDate.ToLongTimeString();
                            //ProcessTweet(tweet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logFileDir, "\n" + ex.Message);
                    lblStatus.Text = "Error: Failed to access Twitter!";
                    Properties.Settings.Default.Running = false;
                }
            }
            else
            {
                return;
            }
        }

        private void ProcessTweet(TwitterStatus Tweet)
        {
            String tweetText = Tweet.Text.Trim().ToLower();
            if (tweetText.Contains("shutdown"))
            {
                System.Diagnostics.Process.Start("shutdown", "-s -f -t 100"); // Shutdown
            }
            else if (tweetText.Contains("logoff"))
            {
                System.Diagnostics.Process.Start("shutdown", "-l -f -t 100"); // Logoff
            }
            else if (tweetText.Contains("restart"))
            {
                System.Diagnostics.Process.Start("shutdown", "-r -f -t 100"); // Restart
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void SaveShutdownTime(TwitterStatus Tweet)
        {
            Properties.Settings.Default.LastShutdownTime = Tweet.CreatedDate;
            //Properties.Settings.Default.LastTweetID = Tweet.Id;
        }

        private void editSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideWindows();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowHideWindows();
        }

        private void ShowHideWindows()
        {
            txtMention.Text = Properties.Settings.Default.UserName;
            if (Properties.Settings.Default.AutomaticStart == true) chkStartAutomatic.Checked = true;
            if (Properties.Settings.Default.Running == true)
            {
                btnEasyStart.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";
            }
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void frmTweetMyPc_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.Refresh();
            Properties.Settings.Default.Save();
        }

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            // Step 1 - Retrieve an OAuth Request Token
            requestToken = service.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtVerfier.Text == "" && Properties.Settings.Default.Running == false)
            {
                lblStatus.Text = "* Please Click Authorize and get PIN ! *";
            }
            else if (Properties.Settings.Default.Running == false)
            {
                // Step 3 - Exchange the Request Token for an Access Token
                string verifier = txtVerfier.Text; // <-- This is input into your application by your user
                access = service.GetAccessToken(requestToken, verifier);

                // Step 4 - User authenticates using the Access Token
                service.AuthenticateWith(access.Token, access.TokenSecret);


                Properties.Settings.Default.OAuthScreenName = access.ScreenName;
                Properties.Settings.Default.OAuthToken = access.Token;
                Properties.Settings.Default.OauthTokenSecret = access.TokenSecret;
                Properties.Settings.Default.OauthUserId = access.UserId;
                Properties.Settings.Default.Running = true;
                btnEasyStart.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";

                tmrTweet.Enabled = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.Running == true)
            {
                Properties.Settings.Default.Running = false;
                btnEasyStart.Text = "Start Tweet Shutdown!";
                btnStart.Text = "ReStart Tweet Shutdown!";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.Running == true) tmrTweet.Enabled = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEasyStart_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Running == false)
            {
                Properties.Settings.Default.Running = true;
                btnEasyStart.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";

                tmrTweet.Enabled = true;
            }
            else if (Properties.Settings.Default.Running == true)
            {
                Properties.Settings.Default.Running = false;
                btnEasyStart.Text = "Start Tweet Shutdown!";
                btnStart.Text = "ReStart Tweet Shutdown!";
            }
        }

        private void btnChangeUsername_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserName = txtMention.Text;
        }
    }
}
