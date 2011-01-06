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

            if (File.Exists(accesstokenFileDir))
            {
                // User authenticates using the previous Access Token
                Byte[] data = File.ReadAllBytes(accesstokenFileDir);
                access = (OAuthAccessToken)helper.ByteArrayToObject(data);
                service.AuthenticateWith(access.Token, access.TokenSecret);
            }
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
                    IEnumerable<TwitterStatus> tweetsByUser = service.ListTweetsOnUserTimeline(1);
                    foreach (var tweet in tweetsByUser)
                    {
                        txtResult.Text = tweet.Text;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logFileDir, "\n" + ex.Message);
                    lblStatus.Text = "Error: Failed to Login to Twitter!";
                }
            }
            else
            {
                return;
            }
        }

        private void ProcessTweet(String Tweet)
        {
            if (Tweet == "shutdown")
            {
                System.Diagnostics.Process.Start("shutdown", "-s -f -t 100"); // Shutdown
            }
            else if (Tweet == "logoff")
            {
                System.Diagnostics.Process.Start("shutdown", "-l -f -t 100"); // Logoff
            }
            else if (Tweet == "restart")
            {
                System.Diagnostics.Process.Start("shutdown", "-r -f -t 100"); // Restart
            }
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
            if (Properties.Settings.Default.AutomaticStart == true) chkStartAutomatic.Checked = true;
            if (Properties.Settings.Default.Running == true)
            {
                txtVerfier.Text = Properties.Settings.Default.PIN;
                btnStart.Text = "Stop Tweet Shutdown!";
            }
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void frmTweetMyPc_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            if (txtVerfier.Text == "")
            {
                lblStatus.Text = "* Please Click Authorize and get PIN ! *";
            }
            if (Properties.Settings.Default.Running == false)
            {
                // Step 3 - Exchange the Request Token for an Access Token
                string verifier = txtVerfier.Text; // <-- This is input into your application by your user
                access = service.GetAccessToken(requestToken, verifier);

                // Step 4 - User authenticates using the Access Token
                service.AuthenticateWith(access.Token, access.TokenSecret);

                 
                File.Delete(accesstokenFileDir);
                Byte[] data = helper.ObjectToByteArray(access);
                File.WriteAllBytes(accesstokenFileDir, data);
                
                Properties.Settings.Default.PIN = txtVerfier.Text;
                Properties.Settings.Default.Running = true;
                btnStart.Text = "Stop Tweet Shutdown!";

                tmrTweet.Enabled = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.Running == true)
            {
                Properties.Settings.Default.Running = false;
                btnStart.Text = "Start Tweet Shutdown!";
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
    }
}
