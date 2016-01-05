﻿using System;
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

            for(int i = 0; i < lstVideos.CheckedItems.Count; i=i+1)
            {
                Downloadable d = (Downloadable)lstVideos.CheckedItems[i];
                d.Index = i;
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
            GetVideos();
        }

        void GetVideos()
        {
            Console.Clear();
            pd.consoleLog.Clear();
            chkSelectAll.Checked = false;
            progGlobal.Value = 0;

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
            pd.videos.Clear();
            lstVideos.Items.Clear();
            lstVideos.Refresh();
            txtPlaylistInput.Text = "";
            lblSelected.Text = "0 items queued";
            pd.globalPercentage = 0;
            progGlobal.Value = 100;
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
    }
}
