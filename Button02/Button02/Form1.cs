using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Button02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            Start += StartShowMessage;
            Pause += PauseShowMessage;
            Stop += StopShowMessage;

            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;

            lbPercentage.Text = progressBar.Value.ToString();
        }

        #region delegate
        public delegate void StartEventHandler(object sender, StartEventArgs e);

        public event StartEventHandler Start;

        public class StartEventArgs : EventArgs
        {
            public int percent;
            public StartEventArgs(int percent)
            {
                this.percent = percent;
            }
        }

        public delegate void PauseEventHandler(object sender, PauseEventArgs e);

        public event PauseEventHandler Pause;

        public class PauseEventArgs : EventArgs
        {
            public int percent;
            public PauseEventArgs(int percent)
            {
                this.percent = percent;
            }
        }

        public delegate void StopEventHandler(object sender, StopEventArgs e);

        public event StopEventHandler Stop;

        public class StopEventArgs : EventArgs
        {
            public int percent;
            public StopEventArgs(int percent)
            {
                this.percent = percent;
            }
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start(this, new StartEventArgs(progressBar.Value));
            tmr.Enabled = true;
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            tmr.Enabled = false;
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            Pause(this, new PauseEventArgs(progressBar.Value));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmr.Enabled = false;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            btnPause.Enabled = true;
            Stop(this, new StopEventArgs(progressBar.Value));
            progressBar.Value = 0;
            lbPercentage.Text = progressBar.Value.ToString();
        }

        public void StartShowMessage(object sender, StartEventArgs e)
        {
            MessageBox.Show("Download Starts, Initial Percentage is " + e.percent);
        }

        public void PauseShowMessage(object sender, PauseEventArgs e)
        {
            MessageBox.Show("Download Pauses, Current Percentage is " + e.percent);
        }

        public void StopShowMessage(object sender, StopEventArgs e)
        {
            MessageBox.Show("Download Stops, Final Percentage is " + e.percent);
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < progressBar.Maximum)
                progressBar.Value++;
            lbPercentage.Text = progressBar.Value.ToString();
            if (progressBar.Value == 100)
            {
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
                tmr.Enabled = false;
                Stop(this, new StopEventArgs(progressBar.Value));
                progressBar.Value = 0;
                lbPercentage.Text = progressBar.Value.ToString();

            }
        }

       
    }
}
