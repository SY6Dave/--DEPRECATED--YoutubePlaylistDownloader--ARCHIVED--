using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubePlaylistDownloader
{
    public partial class Form1 : Form
    {
        PlaylistDownloader pd;
        object consolelock = new object();
        bool isdownloading = false;

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
            if (isdownloading) return;
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

                btnDownload.Enabled = false;
                btnGet.Enabled = false;
                btnBrowse.Enabled = false;
                txtPlaylistInput.Enabled = false;
                nudThreads.Enabled = false;
                chkIncrement.Enabled = false;
                chkSelectAll.Enabled = false;
                lstVideos.Enabled = false;
                isdownloading = true;
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
            if (isdownloading) return;
            GetVideos();
        }

        void GetVideos()
        {
            lstVideos.Items.Clear();
            string getID = pd.GetPlaylistIDFromURL(txtPlaylistInput.Text);
            /*if (getID == null)
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid playlist url");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                txtPlaylistInput.BackColor = Color.Red;
                return;


            }*/

            List<Downloadable> videos = pd.GetVideosByPlaylist(getID);
            if (videos == null)
            {
                /*lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid playlist url");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                txtPlaylistInput.BackColor = Color.Red;
                return;*/

                videos = pd.GetVideosByQuery(txtPlaylistInput.Text);
            }

            if (videos == null || videos.Count == 0)
            {
                lock (consolelock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.CursorTop = pd.consoleLog.Count;
                    Console.CursorLeft = 0;
                    Console.WriteLine("No playlist or search results found");
                    pd.ConsoleWrittenTo("No playlist or search results found");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                txtPlaylistInput.BackColor = Color.Red;
                return;
            }


            foreach (Downloadable d in videos)
            {
                lstVideos.Items.Add(d);
            }

            lstVideos.DisplayMember = "Name";
        }

        private void txtPlaylistInput_TextChanged(object sender, EventArgs e)
        {
            if (isdownloading) return;
            txtPlaylistInput.BackColor = Color.Empty;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (isdownloading) return;
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
            if (isdownloading) return;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                pd.savePath = folderBrowser.SelectedPath;
                lblPath.Text = "Download location: " + folderBrowser.SelectedPath;
            }
        }

        private void nudThreads_ValueChanged(object sender, EventArgs e)
        {
            if (isdownloading) return;
            pd.maxThreads = (int)nudThreads.Value;
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

                isdownloading = false;
                downloadIsComplete d = new downloadIsComplete(DownloadComplete);
                Invoke(d);
            }
        }

        void DownloadComplete()
        {
            btnDownload.Enabled = true;
            btnGet.Enabled = true;
            btnBrowse.Enabled = true;
            txtPlaylistInput.Enabled = true;
            nudThreads.Enabled = true;
            chkIncrement.Enabled = true;
            chkSelectAll.Enabled = true;
            lstVideos.Enabled = true;
            GetVideos();
            lblSelected.Text = "0 items queued";
        }

        private void chkIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (isdownloading) return;
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
            if (isdownloading) return;
            lblSelected.Text = lstVideos.CheckedItems.Count.ToString() + " items queued";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.davidmortiboy.com");
        }
    }
}
