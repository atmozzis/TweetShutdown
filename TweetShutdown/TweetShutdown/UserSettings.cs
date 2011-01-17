using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace TweetShutdown
{
    [XmlRootAttribute("PurchaseOrder", Namespace = "http://www.github.com/aungthumoe5/Tweet-Shutdown", IsNullable = false)]
    public class UserSettings
    {
        public string oauthToken, oauthSecret, username, pcname;
        public bool autostart, running;
        string settingsDir;

        public UserSettings()
        {
            oauthToken = oauthSecret = username = pcname = "";

            settingsDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TweetShutdown\\";
            if (!Directory.Exists(settingsDir)) Directory.CreateDirectory(settingsDir);

            settingsDir += Environment.UserName + "-settings.log";
            if (!File.Exists(settingsDir))
            {
                Stream sw = File.Create(settingsDir);
                sw.Close();
            }
        }

        public void Save()
        {
            // Using a FileStream, create an XmlTextReader.
            Stream fs = new FileStream(settingsDir, FileMode.OpenOrCreate , FileAccess.Write);
            XmlSerializer xl = new XmlSerializer(this.GetType());

            xl.Serialize(fs, this);
            fs.Close();
        }

        public void Load()
        {
            Stream fs = new FileStream(settingsDir, FileMode.OpenOrCreate, FileAccess.Read);
            XmlReader xr = new XmlTextReader(fs);
            XmlSerializer xl = new XmlSerializer(this.GetType());

            try
            {
                if (xl.CanDeserialize(xr))
                {
                    this.LoadSettingsFrom((UserSettings)xl.Deserialize(xr));
                }
                else
                {
                    this.InitSettingsToDefault();
                }
            }
            catch (XmlException ex)
            {
                String Date = DateTime.Now.ToShortDateString();
                String Time = DateTime.Now.ToShortTimeString();
                String errorlogDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TweetShutdown\\error.log";
                File.AppendAllText(errorlogDir, "\n" + Date + " " + Time + "\t" + ex.Message);
                this.InitSettingsToDefault();
            }
            fs.Close();
        }

        private void InitSettingsToDefault()
        {
            oauthToken = "236756857 - TFOCeLwHApOJ2CdaS66D3u7ZMArXBZz9YTaLvSHu";
            oauthSecret = "GdyRKinH9uBNipBXcEQHqXYa6IjHF2FmcYzdNX6mtQ8";
            username = "";
            pcname = "";
            autostart = false;
            running = false;
        }

        private void LoadSettingsFrom(UserSettings settings)
        {
            oauthToken = settings.oauthToken;
            oauthSecret = settings.oauthSecret;
            username = settings.username;
            pcname = settings.pcname;

            autostart = settings.autostart;
            running = settings.running;
        }
    }
}
