using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Threading;
using l4dhelper;

using load4wrd_service.Properties;
using l4dhelper.Data.MySqlClient;

namespace load4wrd_service
{
    public partial class frmLogs : Form
    {
        internal Settings st;

        internal MySqlClient mysqlClient;

        internal MySqlQuery mysqlQuery;

        internal Request myRequest;

        public frmLogs()
        {
            InitializeComponent();
        }

        public bool mysql_init()
        {
            st = new Settings();
            mysqlClient = new MySqlClient(
                st.db_host,
                st.db_port,
                st.db_username,
                st.db_password,
                st.db_database
            );

            bool is_true = mysqlClient.Test_Connection();
            if (!is_true)
            {
                Console.WriteLine("MySQL connection is incorrect.");
                return false;
            }

            Console.WriteLine("MySQL connection is good.");
            mysqlQuery = new MySqlQuery(mysqlClient);
            myRequest = new Request(mysqlClient, st.api_webhook);
            myRequest.Start();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mysql_init();

            Thread.Sleep(1000);
            Console.WriteLine("Main thread ({0}) exiting...",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myRequest.Stop();
        }
    }
}
