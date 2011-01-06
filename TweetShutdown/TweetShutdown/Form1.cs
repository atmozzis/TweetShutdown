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
using Yedda;

namespace TweetShutdown
{
    public partial class frmTweetMyPc : Form
    {
        public frmTweetMyPc()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                lblStatus.Text = "* Please Enter Twitter Username *";
                txtUserName.Focus();
                return;
            }
            else if (txtPassword.Text.Trim() == "")
            {
                lblStatus.Text = "* Please Enter Twitter Password *";
                txtPassword.Focus();
                return;
            }
            else
            {
                lblStatus.Text = "";
            }

            // Check for Valid Username and Password and then Save Settings
            Twitter objTwitter = new Twitter();
            XmlDocument Updates = new XmlDocument();

            try // Try logging in
            {
                Updates = objTwitter.GetUserTimelineAsXML(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                Properties.Settings.Default.UserName = txtUserName.Text.Trim();
                Properties.Settings.Default.Password = txtPassword.Text.Trim();
                this.WindowState = FormWindowState.Maximized;
                this.ShowInTaskbar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Login to Twitter with the values supplied. Please check your login details. "+ex.Message);
                txtUserName.Focus();
                return;
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
            if (Properties.Settings.Default.UserName == "")
            {
                return;
            }
            else
            {
                Twitter objTwitter = new Twitter();
                XmlDocument Updates = new XmlDocument();

                try // Try logging in
                {
                    Updates = objTwitter.GetUserTimelineAsXML(Properties.Settings.Default.UserName.Trim(),
                        Properties.Settings.Default.Password.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : Failed to Login to Twitter with the values supplied. Please check your login details.");
                    return;
                }

                try
                {
                    XmlNode node = Updates.SelectSingleNode("/statuses/status/id");
                    if(node.InnerText.Trim() != Properties.Settings.Default.LastID.Trim())
                    {
                        // Compare the Tweet ID to check for new tweets
                        Properties.Settings.Default.LastID = node.InnerText.Trim();
                        node = Updates.SelectSingleNode("/statuses/status/text");
                        ProcessTweet(node.InnerText.Trim());
                    }
                }
                catch(Exception ex)
                {
                    return;
                }
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
            tmrTweet.Enabled = false;
            txtUserName.Text = Properties.Settings.Default.UserName.Trim();
            txtPassword.Text = Properties.Settings.Default.Password.Trim();
            if (Properties.Settings.Default.AutomaticStart == true) chkStartAutomatic.Checked = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
    }
}
