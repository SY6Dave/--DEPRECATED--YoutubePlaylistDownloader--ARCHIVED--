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

namespace YoutubePlaylistDownloader
{
    public partial class Form1 : Form
    {
        public static PlaylistDownloader pd;
        object consolelock = new object();
        object disablelock = new object();
        bool isdisabled = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pd = new PlaylistDownloader();
            Console.Title = "Youtube Playlist Downloader v1.0";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (isdisabled) return;
            if (pd.savePath == "")
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("Please choose a destination folder");
                    pd.ConsoleWrittenTo("Please choose a destination folder");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return;
            }

            List<Downloadable> downloading = new List<Downloadable>();

            foreach (Downloadable d in lstVideos.CheckedItems)
            {
                downloading.Add(d);
            }

            if (downloading.Count > 0)
            {
                Console.Clear();
                pd.consoleLog.Clear();
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine(downloading.Count + " items queued for download");
                    pd.ConsoleWrittenTo(downloading.Count + " items queued for download");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                pd.StartThreads(downloading);

                DisableControls();
                isdisabled = true;
            }
            else
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("No videos selected");
                    pd.ConsoleWrittenTo("No videos selected");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return;
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (isdisabled) return;
            GetVideos();
        }

        void GetVideos()
        {
            Console.Clear();
            pd.consoleLog.Clear();
            chkSelectAll.Checked = false;

            lock (consolelock)
            {
                Console.CursorTop = pd.consoleLog.Count;
                Console.CursorLeft = 0;
                Console.WriteLine("Attempting to retrieve videos...");
                pd.ConsoleWrittenTo("Attempting to retrieve videos...");
            }

            lstVideos.Items.Clear();
            string getID = pd.GetPlaylistIDFromURL(txtPlaylistInput.Text);

            List<Downloadable> videos = pd.GetVideosByPlaylist(getID);
            if (videos == null)
            {
                //no such playlist, try channel

                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("No playlist found, looking for channels...");
                    pd.ConsoleWrittenTo("No playlist found, looking for channels...");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                string channelPlaylist = pd.GetPlaylistIDFromChannel(txtPlaylistInput.Text);
                if (channelPlaylist != null)
                {
                    lock (consolelock)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.CursorTop = pd.consoleLog.Count;
                        Console.CursorLeft = 0;
                        Console.WriteLine("Channel found! Retrieving uploads...");
                        pd.ConsoleWrittenTo("Channel found! Retrieving uploads...");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    videos = pd.GetVideosByPlaylist(channelPlaylist);

                    if (videos == null || videos.Count == 0)
                    {
                        lock (consolelock)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.CursorTop = pd.consoleLog.Count;
                            Console.CursorLeft = 0;
                            Console.WriteLine("Channel has no uploads, querying search...");
                            pd.ConsoleWrittenTo("Channel has no uploads, querying search...");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
                else
                {
                    //no uploads
                    lock (consolelock)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.CursorTop = pd.consoleLog.Count;
                        Console.CursorLeft = 0;
                        Console.WriteLine("No channel found, querying search...");
                        pd.ConsoleWrittenTo("No channel found, querying search...");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    videos = pd.GetVideosByQuery(txtPlaylistInput.Text);
                }

            }
            else
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("Playlist found! Retrieving videos...");
                    pd.ConsoleWrittenTo("Playlist found! Retrieving videos...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            if (videos == null || videos.Count == 0)
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("No videos found!");
                    pd.ConsoleWrittenTo("No videos found!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                txtPlaylistInput.BackColor = Color.Red;
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.CursorTop = pd.consoleLog.Count;
                Console.CursorLeft = 0;
                Console.WriteLine("Got result! Populating list...");
                pd.ConsoleWrittenTo("Got result! Populating list...");
                Console.ForegroundColor = ConsoleColor.White;
            }


            foreach (Downloadable d in videos)
            {
                lstVideos.Items.Add(d);
            }

            lstVideos.DisplayMember = "Name";

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
                lblPath.Text = "Download location: " + folderBrowser.SelectedPath;
            }
        }

        

        delegate void downloadIsComplete();

        public void threadsComplete()
        {
            lock (consolelock)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorTop = pd.consoleLog.Count;
                Console.CursorLeft = 0;
                Console.WriteLine("All videos downloaded!");
                pd.ConsoleWrittenTo("All videos downloaded!");

                downloadIsComplete d = new downloadIsComplete(DownloadComplete);
                Invoke(d);
            }
        }

        void DownloadComplete()
        {
            EnableControls();
            //GetVideos();
            lblSelected.Text = "0 items queued";
        }

        void DisableControls()
        {
            isdisabled = true;
            btnDownload.Enabled = false;
            btnGet.Enabled = false;
            btnBrowse.Enabled = false;
            txtPlaylistInput.Enabled = false;
            chkIncrement.Enabled = false;
            chkSelectAll.Enabled = false;
            lstVideos.Enabled = false;
        }
        void EnableControls()
        {
            isdisabled = false;
            btnDownload.Enabled = true;
            btnGet.Enabled = true;
            btnBrowse.Enabled = true;
            txtPlaylistInput.Enabled = true;
            chkIncrement.Enabled = true;
            chkSelectAll.Enabled = true;
            lstVideos.Enabled = true;
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
    }
}
