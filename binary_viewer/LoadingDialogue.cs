using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace binary_viewer
{
    public partial class LoadingDialogue : Form
    {
        public int CooldownTime { get; set; }
        
        public int TimerInterval
        {
            get { return screenTimer.Interval; }
            set { screenTimer.Interval = value; }
        }

        public string LoadingText
        {
            get { return loadingLabel.Text; }
            set { loadingLabel.Text = value; this.Invalidate(); }
        }

        private bool m_closeTriggered = false;
        private int m_currentTime = 0;

        public LoadingDialogue()
        {
            InitializeComponent();

            CooldownTime = 200;
        }

        public void TriggerCloseLoading()
        {
            m_closeTriggered = true;
            m_currentTime = 0;
            screenTimer.Start();
        }

        public void CancelCloseLoading()
        {
            screenTimer.Stop();
            m_closeTriggered = false;
            m_currentTime = 0;
        }

        private void cooldownTimer_Tick(object sender, EventArgs e)
        {
            if( m_closeTriggered )
            {
                m_currentTime += screenTimer.Interval;

                if(m_currentTime >= CooldownTime)
                {
                    // we're done with the loading dialog, so close it
                    Close();
                }
            }
            else
            {
                m_currentTime = 0;
            }
        }
    }
}
