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
    public partial class frmSettings : Form
    {
        int maxThreads;
        int maxVids;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void nudThreads_ValueChanged(object sender, EventArgs e)
        {
            maxThreads = (int)nudThreads.Value;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            maxThreads = Form1.pd.maxThreads;
            maxVids = Form1.pd.maxVids;
            nudThreads.Value = Form1.pd.maxThreads;
            nudMaxVids.Value = Form1.pd.maxVids;
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudMaxVids_ValueChanged(object sender, EventArgs e)
        {
            maxVids = (int)nudMaxVids.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Form1.pd.maxThreads = maxThreads;
            Form1.pd.maxVids = maxVids;
            this.Close();
        }
    }
}
