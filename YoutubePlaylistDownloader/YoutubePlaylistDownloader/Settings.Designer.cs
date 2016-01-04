namespace YoutubePlaylistDownloader
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.nudMaxVids = new System.Windows.Forms.NumericUpDown();
            this.nudThreads = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVids)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // nudMaxVids
            // 
            this.nudMaxVids.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxVids.Location = new System.Drawing.Point(156, 77);
            this.nudMaxVids.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.nudMaxVids.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxVids.Name = "nudMaxVids";
            this.nudMaxVids.ReadOnly = true;
            this.nudMaxVids.Size = new System.Drawing.Size(53, 20);
            this.nudMaxVids.TabIndex = 16;
            this.nudMaxVids.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxVids.ValueChanged += new System.EventHandler(this.nudMaxVids_ValueChanged);
            // 
            // nudThreads
            // 
            this.nudThreads.Location = new System.Drawing.Point(156, 51);
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
            this.nudThreads.TabIndex = 17;
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
            this.label2.Location = new System.Drawing.Point(53, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Number of threads:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Max videos:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(56, 186);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(156, 186);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(75, 23);
            this.btnDiscard.TabIndex = 21;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudThreads);
            this.Controls.Add(this.nudMaxVids);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVids)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMaxVids;
        private System.Windows.Forms.NumericUpDown nudThreads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDiscard;
    }
}