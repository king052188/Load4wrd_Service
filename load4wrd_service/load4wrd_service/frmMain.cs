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
        
        RestartEvent restart;

        public static bool IsPowerOn { get; set; }

        public void mysql_init()
        {
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
                Console.WriteLine("MySQL connection is incorrect.");
                return;
           }

           Console.WriteLine("MySQL connection is good.");
           mysqlQuery = new MySqlQuery(mysqlClient);

           Request r = new Request(mysqlClient);

           r.get_request();
        }
        
        public frmMain()
        {
            InitializeComponent();
            IsPowerOn = false;

            mysql_init();

            restart = new RestartEvent();
            restart.RestartProceed += new RestartEvent.RestartEventHandler(restart_service);
        }

        public void restart_service(object sender, RestartArgs e)
        {
            if (e.IsRestart)
            {

            }
            power();
        }

        public void power(bool isRestart = false)
        {
            if (!IsPowerOn)
            {
                mysql_init();
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
