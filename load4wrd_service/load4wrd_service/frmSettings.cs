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
    public partial class frmSettings : Form
    {
        public delegate void RestartEventHandler(object sender, RestartArgs e);

        public event RestartEventHandler Restart_Service;
        
        internal Settings st;

        public frmSettings()
        {
            InitializeComponent();
            
            st = new Settings();
            txtHost.Text = st.db_host;
            txtPort.Text = st.db_port;
            txtUsername.Text = st.db_username;
            txtPassword.Text = st.db_password;
            txtDatabase.Text = st.db_database;
            txtWebhook.Text = st.api_webhook;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

        private void frmSettings_Closing(object sender, FormClosingEventArgs e)
        {
            Restart.Proceed = false;
            if(st.db_host != txtHost.Text)
            {
                do_(true);
                return;
            }

            if (st.db_port != txtPort.Text)
            {
                do_(true);
                return;
            }

            if (st.db_username != txtUsername.Text)
            {
                do_(true);
                return;
            }

            if (st.db_password != txtPassword.Text)
            {
                do_(true);
                return;
            }

            if (st.db_database != txtDatabase.Text)
            {
                do_(true);
                return;
            }

            if (st.api_webhook != txtWebhook.Text)
            {
                do_(true);
                return;
            }

            do_(false);
        }

        public void do_(bool is_restart)
        {
            Restart.Proceed = is_restart;
            if (is_restart)
            {
                st = new Settings();
                st.db_host = txtHost.Text;
                st.db_port = txtPort.Text;
                st.db_username = txtUsername.Text;
                st.db_password = txtPassword.Text;
                st.db_database = txtDatabase.Text;
                st.api_webhook = txtWebhook.Text;
                st.Save();
            }
            this.Dispose();
            this.Close();
            Restart_Service(this, new RestartArgs());
        }
    }

    public class Restart
    {
        public static bool Proceed { get; set; }
    }
}
