using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace YoutubePlaylistDownloader
{
    public partial class Form1 : Form
    {
        public static PlaylistDownloader pd;
        object consolelock = new object();
        object disablelock = new object();
        bool isdisabled = false;
        bool listEnabled = true;
        public int remaining = 0;
        public int errors = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pd = new PlaylistDownloader();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (isdisabled) return;
            if (pd.savePath == "")
            {
                ssInfoLabel.Text = "Please choose a download location";
                return;
            }

            List<Downloadable> downloading = new List<Downloadable>();

            for(int i = 0; i < lstVideos.CheckedItems.Count; i=i+1)
            {
                Downloadable d = (Downloadable)lstVideos.CheckedItems[i];
                d.Index = i;
                downloading.Add(d);
            }

            if (downloading.Count > 0)
            {
                remaining = downloading.Count;

                lstVideos.Items.Clear();
                for (int i = 0; i < downloading.Count; i=i+1)
                {
                    lstVideos.Items.Add(downloading[i]);
                    lstVideos.SetItemChecked(i, true);
                }

                pd.StartThreads(downloading);

                DisableControls();
                isdisabled = true;

                Thread updatethread = new Thread(UpdateListThread);
                updatethread.Start();
            }
            else
            {
                return;
            }
        }

        delegate void myDel();

        void UpdateListThread()
        {
            while(isdisabled)
            {
                myDel ud = new myDel(UpdateList);
                Invoke(ud);
                Thread.Sleep(50);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (isdisabled) return;

            setListEnabled(true);
            btnDownload.Enabled = true;
            chkSelectAll.Enabled = true;
            pd.videos = null;
            GetVideos();
        }

        void GetVideos()
        {
            chkSelectAll.Checked = false;
            progGlobal.Value = 0;

            ssInfoLabel.Text = "Attempting to retrieve videos...";

            lstVideos.Items.Clear();

            string getID = pd.GetPlaylistIDFromURL(txtPlaylistInput.Text);

            List<Downloadable> videos = pd.GetVideosByPlaylist(getID);
            if (videos == null)
            {
                //no such playlist, try channel

                ssInfoLabel.Text = "No playlist found, looking for channels...";

                string channelPlaylist = pd.GetPlaylistIDFromChannel(txtPlaylistInput.Text);
                if (channelPlaylist != null)
                {
                    ssInfoLabel.Text = "Channel found! Retrieving uploads...";

                    videos = pd.GetVideosByPlaylist(channelPlaylist);

                    if (videos == null || videos.Count == 0)
                    {
                        ssInfoLabel.Text = "Channel has no uploads, querying search...";
                    }
                }
                else
                {
                    //no uploads
                    ssInfoLabel.Text = "No channel found, querying search...";
                    videos = pd.GetVideosByQuery(txtPlaylistInput.Text);
                }

            }
            else
            {
                ssInfoLabel.Text = "Playlist found! Retrieving videos...";
            }

            if (videos == null || videos.Count == 0)
            {
                ssInfoLabel.Text = "No videos found!";

                txtPlaylistInput.BackColor = Color.Red;
                return;
            }
            else
            {
                ssInfoLabel.Text = "Got result! Populating list...";
            }


            foreach (Downloadable d in videos)
            {
                lstVideos.Items.Add(d);
            }

            lstVideos.DisplayMember = "Name";
            lstVideos.Refresh();
            //EnableControls();
        }

        private void txtPlaylistInput_TextChanged(object sender, EventArgs e)
        {
            if (isdisabled) return;
            txtPlaylistInput.BackColor = Color.Empty;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (isdisabled) return;
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < lstVideos.Items.Count; i = i + 1)
                {
                    lstVideos.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < lstVideos.Items.Count; i = i + 1)
                {
                    lstVideos.SetItemChecked(i, false);
                }
            }

            lblSelected.Text = lstVideos.CheckedItems.Count.ToString() + " items queued";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (isdisabled) return;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                pd.savePath = folderBrowser.SelectedPath;
                ssInfoLabel.Text = "Download location: " + folderBrowser.SelectedPath;
            }
        }

        public void ThreadSetLabel(string text)
        {
            SetLabel sl = new SetLabel(SetLabelText);
            object[] pms = new object[1];
            pms[0] = text;
            Invoke(sl, pms);
        }

        delegate void SetLabel(string text);

        void SetLabelText(string text)
        {
            ssInfoLabel.Text = text;
        }

        delegate void downloadIsComplete();

        public void threadsComplete()
        {
            lock (consolelock)
            {
                if(errors == 0)
                {
                    ssInfoLabel.Text = "All videos downloaded!";
                }
                else if(errors > 0)
                {
                    ssInfoLabel.Text = "Download finished with " + errors + " errors.";
                }

                errors = 0;

                downloadIsComplete d = new downloadIsComplete(DownloadComplete);
                Invoke(d);
            }
        }

        void DownloadComplete()
        {
            EnableControls();
            //GetVideos();
            //pd.videos.Clear();
            //lstVideos.Items.Clear();
            lstVideos.Refresh();
            //txtPlaylistInput.Text = "";
            //lblSelected.Text = "0 items queued";
            pd.globalPercentage = 0;
            progGlobal.Value = 100;

            setListEnabled(false);
            btnDownload.Enabled = false;
            chkSelectAll.Enabled = false;
        }

        void DisableControls()
        {
            isdisabled = true;
            btnSettings.Enabled = false;
            btnDownload.Enabled = false;
            btnGet.Enabled = false;
            btnBrowse.Enabled = false;
            txtPlaylistInput.Enabled = false;
            chkIncrement.Enabled = false;
            chkSelectAll.Enabled = false;
            setListEnabled(false);
        }
        void EnableControls()
        {
            isdisabled = false;
            btnSettings.Enabled = true;
            btnDownload.Enabled = true;
            btnGet.Enabled = true;
            btnBrowse.Enabled = true;
            txtPlaylistInput.Enabled = true;
            chkIncrement.Enabled = true;
            chkSelectAll.Enabled = true;
            setListEnabled(true);
        }

        private void chkIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (isdisabled) return;
            if (chkIncrement.Checked)
            {
                pd.incremental = true;
            }
            else
            {
                pd.incremental = false;
            }
        }

        private void lstVideos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isdisabled) return;

            lblSelected.Text = lstVideos.CheckedItems.Count.ToString() + " items queued";
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.davidmortiboy.com");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings Settings = new frmSettings();
            Settings.ShowDialog();
        }

        object lockList = new object();

        public void UpdateList()
        {
            //Downloadable newD = (Downloadable)lstVideos.Items[d.Index];

            lock(lockList)
            {
                lstVideos.Refresh();
                try
                {
                    progGlobal.Value = (int)pd.globalPercentage;
                    ssInfoLabel.Text = "Downloading " + remaining + " videos: " + Math.Round(pd.globalPercentage, 2) + "%";
                }
                catch { }
            }
        }

        private void lstVideos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(!listEnabled)
            {
                e.NewValue = e.CurrentValue;
            }
        }

        private void setListEnabled(bool enabled)
        {
            listEnabled = enabled;
            if (!listEnabled)
            {
                //lstVideos.BackColor = Color.FromKnownColor(KnownColor.Window);
                //lstVideos.Color
                lstVideos.SelectionMode = SelectionMode.None;
            }
            else
            {
               // lstVideos.BackColor = Color.FromKnownColor(KnownColor.Control);
                lstVideos.SelectionMode = SelectionMode.One;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
