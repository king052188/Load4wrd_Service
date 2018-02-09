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


using l4dhelper;
using l4dhelper.Data.MySqlClient;

namespace load4wrd_service
{
    public partial class frmMain : Form
    {
        internal Settings st;

        internal MySqlClient mysqlClient;

        internal MySqlQuery mysqlQuery;

        internal Request myRequest;

        public static bool IsPowerOn { get; set; }

        public static bool IsRestarting { get; set; }

        public bool mysql_init()
        {

            lblLabelStatus.Text = "Service connecting...";
            st = new Settings();
            mysqlClient = new MySqlClient(
                st.db_host,
                st.db_port,
                st.db_username,
                st.db_password,
                st.db_database
            );

           bool is_true =  mysqlClient.Test_Connection();
           if(!is_true)
           {
                Console.WriteLine("Service connection is incorrect.");
                lblLabelStatus.Text = "Service connection is incorrect.";
                return false;
           }

            Console.WriteLine("Service connection is good.");
            lblLabelStatus.Text = "Service Looks Good.";
            mysqlQuery = new MySqlQuery(mysqlClient);
            myRequest = new Request(mysqlClient, st.api_webhook);
            myRequest.Status_Event += new Request.StatusEventHandler(Status_Logs);
            myRequest.Start();
            return true;
        }

        public void Status_Logs(object send, StatusArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            if(e.Code == -200)
            {
                if(IsRestarting)
                {
                    IsRestarting = false;
                    System.Threading.Thread.Sleep(1000);
                    power();
                    return;
                }
            }
            if (e.Code == 500)
            {
                System.Threading.Thread.Sleep(1000);
                power();
                return;
            }
            lblLabelStatus.Invoke((MethodInvoker)(() =>
            {
                lblLabelStatus.Text = e.Message;
            }));
            Control.CheckForIllegalCrossThreadCalls = true;
        }
        
        public frmMain()
        {
            InitializeComponent();
            lblLabelStatus.Text = "";
            IsPowerOn = false;
            IsRestarting = false;
        }
        
        public void power()
        {
            if (!IsPowerOn)
            {
                lblLabelStatus.Text = "Service connecting...";
                Request.EnableSMSCommand = chkSMSCommand.Checked;
                btnPower.Image = Resources.switch_on;
                pbServiceStatus.Image = Resources.power_button_on;
                lblServiceLabel.Text = "Stop Service";
                IsPowerOn = true;

                if (!mysql_init())
                {
                    System.Threading.Thread.Sleep(300);
                    btnPower.Image = Resources.switch_off;
                    pbServiceStatus.Image = Resources.power_button_off;
                    lblServiceLabel.Text = "Start Service";
                    IsPowerOn = false;
                }
            }
            else
            {
                myRequest.Stop();
                lblLabelStatus.Text = "Service disconnecting...";
                System.Threading.Thread.Sleep(500);
                if (mysqlClient.IsOpen )
                {
                    mysqlClient.Close();
                }
                System.Threading.Thread.Sleep(300);

                btnPower.Image = Resources.switch_off;
                pbServiceStatus.Image = Resources.power_button_off;
                lblServiceLabel.Text = "Start Service";
                IsPowerOn = false;
            }
        }

        public void restart(object send, RestartArgs e)
        {
            if(e.IsRestart)
            {
                if (IsPowerOn)
                {
                    IsRestarting = e.IsRestart;
                    power();
                }
            }
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            power();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.Restart_Service += new frmSettings.RestartEventHandler(restart);
            settings.ShowDialog();
        }

        private void chkSMSCommand_CheckedChanged(object sender, EventArgs e)
        {
            Request.EnableSMSCommand = chkSMSCommand.Checked;
        }
        
    }
}
