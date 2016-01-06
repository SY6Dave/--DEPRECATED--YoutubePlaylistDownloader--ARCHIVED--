namespace YoutubePlaylistDownloader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaylistInput = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkIncrement = new System.Windows.Forms.CheckBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.progGlobal = new System.Windows.Forms.ProgressBar();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.ssInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstVideos = new YoutubePlaylistDownloader.CustomCheckList();
            this.ssInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(46, 94);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter a YouTube URL or search term:";
            // 
            // txtPlaylistInput
            // 
            this.txtPlaylistInput.Location = new System.Drawing.Point(237, 66);
            this.txtPlaylistInput.Name = "txtPlaylistInput";
            this.txtPlaylistInput.Size = new System.Drawing.Size(336, 20);
            this.txtPlaylistInput.TabIndex = 3;
            this.txtPlaylistInput.TextChanged += new System.EventHandler(this.txtPlaylistInput_TextChanged);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(579, 64);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "Get Videos";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(46, 123);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(46, 32);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(137, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse Destination Path";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkIncrement
            // 
            this.chkIncrement.AutoSize = true;
            this.chkIncrement.Checked = true;
            this.chkIncrement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrement.Location = new System.Drawing.Point(390, 39);
            this.chkIncrement.Name = "chkIncrement";
            this.chkIncrement.Size = new System.Drawing.Size(137, 17);
            this.chkIncrement.TabIndex = 9;
            this.chkIncrement.Text = "Incremental file names?";
            this.chkIncrement.UseVisualStyleBackColor = true;
            this.chkIncrement.CheckedChanged += new System.EventHandler(this.chkIncrement_CheckedChanged);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(569, 123);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(79, 13);
            this.lblSelected.TabIndex = 10;
            this.lblSelected.Text = "0 items queued";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(519, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Created by: David Mortiboy";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(530, 465);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.davidmortiboy.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(533, 35);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(121, 23);
            this.btnSettings.TabIndex = 15;
            this.btnSettings.Text = "Advanced Options";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // progGlobal
            // 
            this.progGlobal.Location = new System.Drawing.Point(46, 426);
            this.progGlobal.MarqueeAnimationSpeed = 0;
            this.progGlobal.Name = "progGlobal";
            this.progGlobal.Size = new System.Drawing.Size(606, 13);
            this.progGlobal.Step = 1;
            this.progGlobal.TabIndex = 17;
            // 
            // ssInfo
            // 
            this.ssInfo.AllowMerge = false;
            this.ssInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssInfoLabel});
            this.ssInfo.Location = new System.Drawing.Point(0, 489);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(717, 22);
            this.ssInfo.SizingGrip = false;
            this.ssInfo.TabIndex = 18;
            // 
            // ssInfoLabel
            // 
            this.ssInfoLabel.Name = "ssInfoLabel";
            this.ssInfoLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // lstVideos
            // 
            this.lstVideos.CheckOnClick = true;
            this.lstVideos.FormattingEnabled = true;
            this.lstVideos.Location = new System.Drawing.Point(46, 146);
            this.lstVideos.Name = "lstVideos";
            this.lstVideos.Size = new System.Drawing.Size(606, 274);
            this.lstVideos.TabIndex = 16;
            this.lstVideos.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstVideos_ItemCheck);
            this.lstVideos.SelectedIndexChanged += new System.EventHandler(this.lstVideos_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnGet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 511);
            this.Controls.Add(this.ssInfo);
            this.Controls.Add(this.progGlobal);
            this.Controls.Add(this.lstVideos);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.chkIncrement);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.txtPlaylistInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Youtube Playlist Downloader v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ssInfo.ResumeLayout(false);
            this.ssInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaylistInput;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkIncrement;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnSettings;
        private CustomCheckList lstVideos;
        private System.Windows.Forms.ProgressBar progGlobal;
        private System.Windows.Forms.StatusStrip ssInfo;
        public System.Windows.Forms.ToolStripStatusLabel ssInfoLabel;
    }
}

