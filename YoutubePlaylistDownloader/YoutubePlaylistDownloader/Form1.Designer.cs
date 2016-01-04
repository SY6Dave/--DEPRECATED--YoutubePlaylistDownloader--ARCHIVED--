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
            this.lstVideos = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaylistInput = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.nudThreads = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIncrement = new System.Windows.Forms.CheckBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
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
            // lstVideos
            // 
            this.lstVideos.CheckOnClick = true;
            this.lstVideos.FormattingEnabled = true;
            this.lstVideos.Location = new System.Drawing.Point(46, 146);
            this.lstVideos.Name = "lstVideos";
            this.lstVideos.Size = new System.Drawing.Size(606, 274);
            this.lstVideos.TabIndex = 1;
            this.lstVideos.SelectedIndexChanged += new System.EventHandler(this.lstVideos_SelectedIndexChanged);
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
            // nudThreads
            // 
            this.nudThreads.Location = new System.Drawing.Point(395, 35);
            this.nudThreads.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreads.Name = "nudThreads";
            this.nudThreads.Size = new System.Drawing.Size(43, 20);
            this.nudThreads.TabIndex = 7;
            this.nudThreads.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudThreads.ValueChanged += new System.EventHandler(this.nudThreads_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Choose number of threads (advanced):";
            // 
            // chkIncrement
            // 
            this.chkIncrement.AutoSize = true;
            this.chkIncrement.Checked = true;
            this.chkIncrement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrement.Location = new System.Drawing.Point(456, 36);
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
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(43, 442);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(145, 13);
            this.lblPath.TabIndex = 11;
            this.lblPath.Text = "No download folder selected!";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(137, 99);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(271, 13);
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Text = "After clicking download, please see the console window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(519, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Created by: David Mortiboy";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(530, 459);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.davidmortiboy.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 481);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.chkIncrement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudThreads);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.txtPlaylistInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstVideos);
            this.Controls.Add(this.btnDownload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Youtube Playlist Downloader v1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        public System.Windows.Forms.CheckedListBox lstVideos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaylistInput;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.NumericUpDown nudThreads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIncrement;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

