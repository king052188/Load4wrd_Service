using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using load4wrd_service.Properties;

namespace load4wrd_service
{
    public partial class frmMain : Form
    {
        internal Settings st;
        public static bool IsPowerOn { get; set; }

        public frmMain()
        {
            InitializeComponent();
            st = new Settings();

            IsPowerOn = false;
        }

        public void power()
        {
            if (!IsPowerOn)
            {
                btnPower.Image = Resources.switch_on;
                pbServiceStatus.Image = Resources.power_button_on;
                lblServiceLabel.Text = "Stop Service";
                lblServiceStatus.Text = "Running";
                IsPowerOn = true;
            }
            else
            {
                btnPower.Image = Resources.switch_off;
                pbServiceStatus.Image = Resources.power_button_off;
                lblServiceLabel.Text = "Start Service";
                lblServiceStatus.Text = "Stopped";
                IsPowerOn = false;
            }
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            power();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }
    }
}
