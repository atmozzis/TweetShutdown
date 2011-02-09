using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TweetShutdown
{
    [XmlRootAttribute()]
    public class Userdata
    {
        private String oauthToken, oauthSecret, username, pcname;
        private Boolean autostart, running;

        public String OAuthToken
        {
            get { return oauthToken; }
            set { oauthToken = value; }
        }

        public String OAuthSecret
        {
            get { return oauthSecret; }
            set { oauthSecret = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String PCname
        {
            get { return pcname; }
            set { pcname = value; }
        }

        public Boolean AutoStart
        {
            get { return autostart; }
            set { autostart = value; }
        }

        public Boolean Running
        {
            get { return running; }
            set { running = value; }
        }

        public Userdata()
        {
            oauthToken = "236756857-TFOCeLwHApOJ2CdaS66D3u7ZMArXBZz9YTaLvSHu";
            oauthSecret = "GdyRKinH9uBNipBXcEQHqXYa6IjHF2FmcYzdNX6mtQ8";
            username = "";
            pcname = "";
            autostart = false;
            running = false;

            //oauthToken = Properties.Settings.Default.oauthToken;
            //oauthSecret = Properties.Settings.Default.oauthSecret;
            //username = Properties.Settings.Default.username;
            //pcname = Properties.Settings.Default.pcname;
            //autostart = Properties.Settings.Default.autostart;
            //running = Properties.Settings.Default.running;
        }

        public Userdata(String loadpath)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Userdata));
                XmlReader xr = XmlReader.Create(loadpath);
                if (xs.CanDeserialize(xr))
                {
                    this.Copy((Userdata)xs.Deserialize(xr));
                }
                else
                {
                    this.Copy(new Userdata());
                }
                xr.Close();
            }
            catch (Exception ex)
            {
                String errorlogDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TweetShutdown\\" + "error.log";
                String Date = DateTime.Now.ToShortDateString();
                String Time = DateTime.Now.ToShortTimeString();
                File.AppendAllText(errorlogDir, "\r\n" + Date + " " + Time + "\t" + ex.Message);
            }
        }

        public String Save(String savepath)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Userdata));
                XmlWriter xw = XmlWriter.Create(savepath);
                xs.Serialize(xw, this);
                xw.Close();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void Copy(Userdata userdata)
        {
            oauthToken = userdata.oauthToken;
            oauthSecret = userdata.oauthSecret;
            username = userdata.username;
            pcname = userdata.pcname;
            autostart = userdata.autostart;
            running = userdata.running;
        }
    }
}
