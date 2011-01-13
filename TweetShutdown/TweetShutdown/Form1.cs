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
    public partial class frmMain : Form
    {
        TwitterService service;
        OAuthRequestToken requestToken;
        OAuthAccessToken access;
        
        string consumerKey = "Ya7DuTqdm59e1xOZTZBS2A";
        string consumerSecret = "aZvUPnFHsvoJAEflRfdNtRp2RcOa4TuBk1YkwHZdV3g";

        string exeDir;
        string errorlogDir;
        DateTime StartTime;

        public frmMain()
        {
            InitializeComponent();

            InitDir();

            InitTwitterService();

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

        private void InitMainForm()
        {
            lblStatus.Text = "";
            if (Properties.Settings.Default.running == true)
                HideForm();
        }

        private void ShowForm()
        {
        }

        private void HideForm()
        {
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

        private void tmrTweet_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.running == true)
            {
                try
                {
                    IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsMentioningMe(200);
                    //IEnumerable<TwitterDirectMessage> DMstoUser = service.ListDirectMessagesReceived(800);
                    foreach (var tweet in tweetsByUser)
                    {
                        if (tweet.User.ScreenName == txtUsername.Text)
                        {
                            txtResult.Text = tweet.User + "\n" + tweet.Text + "\n" + tweet.CreatedDate.ToLongTimeString();
                            //ProcessTweet(tweet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(errorlogDir, "\n" + ex.Message);
                    lblStatus.Text = "Error: Failed to access Twitter!";
                    Properties.Settings.Default.running = false;
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


        private void ShowHideWindows()
        {
            if (Properties.Settings.Default.running == true)
            {
                btnRun.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";
            }
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void frmTweetMyPc_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.notifyIcon.Visible = false;
            this.Refresh();
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
            if (txtVerfier.Text == "" && Properties.Settings.Default.running == false)
            {
                lblStatus.Text = "* Please Click Authorize and get PIN ! *";
            }
            else if (Properties.Settings.Default.running == false)
            {
                

                // Step 3 - Exchange the Request Token for an Access Token
                string verifier = txtVerfier.Text; // <-- This is input into your application by your user
                access = service.GetAccessToken(requestToken, verifier);

                // Step 4 - User authenticates using the Access Token
                service.AuthenticateWith(access.Token, access.TokenSecret);

                Properties.Settings.Default.oauthToken = access.Token;
                Properties.Settings.Default.oauthSecret = access.TokenSecret;
                Properties.Settings.Default.running = true;
                btnRun.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";

                tmrClearStatus.Enabled = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.running == true)
            {
                Properties.Settings.Default.running = false;
                btnRun.Text = "Start Tweet Shutdown!";
                btnStart.Text = "ReStart Tweet Shutdown!";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.running == true) tmrClearStatus.Enabled = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEasyStart_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.running == false)
            {
                Properties.Settings.Default.running = true;
                btnRun.Text = "Stop Tweet Shutdown!";
                btnStart.Text = "Stop Tweet Shutdown!";

                tmrClearStatus.Enabled = true;
            }
            else if (Properties.Settings.Default.running == true)
            {
                Properties.Settings.Default.running = false;
                btnRun.Text = "Start Tweet Shutdown!";
                btnStart.Text = "ReStart Tweet Shutdown!";
            }
        }
        
        private void btnAdvanced_Click(object sender, EventArgs e)
        {

        }
    }
}
