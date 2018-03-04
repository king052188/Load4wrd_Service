using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;

using l4dhelper.Data.MySqlClient;


namespace l4dhelper
{
    public class Request
    {
        public delegate void StatusEventHandler(object sender, StatusArgs e);

        public event StatusEventHandler Status_Event;

        public static bool Die { get; set; }

        public static bool KeepAlive { get; set; }

        public static bool EnableSMSCommand { get; set; }

        public static string error_message { get; set; }

        public static Int64 total_process { get; set; }

        internal MySqlQuery mysqlQuery;

        internal Notification notification;

        internal Thread thread_command;

        internal Thread thread_sms;

        internal Thread thread_wallet;

        internal static bool IsRunning { get; set;}

        public Request(MySqlClient mysqlClient, string api_webhook, string api_access_token)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);

            notification = new Notification(mysqlClient);

            thread_command = new Thread(process_sms_commmand);

            thread_sms = new Thread(process_sms);

            //thread_wallet = new Thread(monitor_wallet);

            Json.ApiUrl = api_webhook;

            Json.AccessToken = api_access_token;

            Die = false;

            KeepAlive = false;
        }

        public void Start()
        {
            if(KeepAlive)
            {
                return;
            }

            KeepAlive = true;
            IsRunning = false;
            if (EnableSMSCommand)
            {
                thread_command.Start();
            }
            thread_sms.Start();
            //thread_wallet.Start();
        }

        public void Stop()
        {
            if (!KeepAlive)
            {
                return;
            }

            KeepAlive = false;
            IsRunning = false;
            if (EnableSMSCommand)
            {
                thread_command.Abort();
            }
            thread_sms.Abort();
            //thread_wallet.Abort();
        }

        // sms command
        private void process_sms_commmand()
        {
            try
            {
                while (KeepAlive)
                {
                    if (EnableSMSCommand)
                    {
                        System.Threading.Thread.Sleep(1000);

                        sms_queue_command_init();
                    }

                    //if(!IsRunning)
                    //{
                    //    IsRunning = true;
                    //    Logs(200, "Running");
                    //}
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Exception message: {0}", e.Message);
                Console.WriteLine("Thread Abort Exception - resetting.");

                if(error_message != null)
                {
                    Logs(500, error_message);
                }
                else
                {
                    Logs(-200, "Service disconnected - Forced Stopped");
                }
                
                Thread.ResetAbort();
            }
        }

        private void sms_queue_command_init()
        {
            var sw = Stopwatch.StartNew();

            List<Cache> requests = get_request();

            foreach(Cache cache in requests)
            {
                CMDTransaction cmd = Json.Send(cache.from, cache.command);
                
                Queued queue = new Queued()
                {
                    company_uid = cache.company_uid,
                    mobile = cmd.account,
                    message = cmd.message,
                    status = 1
                };

                bool save_notification = Notification.Send(queue);

                bool delete_cache = MySqlQuery.execute("DELETE FROM ptxt_cache WHERE Id = " + cache.Id + ";");

                total_process++;

                sw.Stop();

                decimal total_time_in_seconds = sw.ElapsedMilliseconds / 60;

                Console.WriteLine("Batch: {0} and Time Executed {1} seconds", total_process, total_time_in_seconds);

                Logs(200, string.Format("Batch: {0} and Time Executed {1} seconds", total_process, total_time_in_seconds));
            }

            //Logs(200, "Running....");
        }

        // sms forward or send it
        private void process_sms()
        {
            try
            {
                while (KeepAlive)
                {
                    System.Threading.Thread.Sleep(5000);

                    sms_queue_init();

                    //if (!IsRunning)
                    //{
                    //    IsRunning = true;
                    //    Logs(200, "Running");
                    //}
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Exception message: {0}", e.Message);
                Console.WriteLine("Thread Abort Exception - resetting.");

                if (error_message != null)
                {
                    Logs(500, error_message);
                }
                else
                {
                    Logs(-200, "Service disconnected - Forced Stopped");
                }

                Thread.ResetAbort();
            }
        }

        public void sms_queue_init()
        {
            SMSQ getQueued = Json.Get();

            if (getQueued == null)
            {
                return;
            }

            if (getQueued.count == 0)
            {
                return;
            }

            int total_sent = 0;
            foreach (SMSQueues objSMS in getQueued.data)
            {
                Console.WriteLine("{0} SMS Processing... " + objSMS.mobile);
                
                Queued queue = new Queued()
                {
                    company_uid = 0,
                    mobile = objSMS.mobile,
                    message = objSMS.message,
                    status = 1
                };
                bool result = Notification.Send(queue);

                if (result)
                {
                    Logs(200, string.Format("{0} | Message Sent to {1}", objSMS.Id, objSMS.mobile));

                    System.Threading.Thread.Sleep(1500);

                    JSON_Response json_r = Json.Update(objSMS.Id);
                    
                    //if (json_r.status == 200)
                    //{
                    //    System.Threading.Thread.Sleep(100);
                    //    string message = "NOTE: Please do not reply to dis mobile#. Unfortunately, we are unable to respond to inquiries sent to dis mobile#.";
                    //    queue = new Queued()
                    //    {
                    //        company_uid = 0,
                    //        mobile = objSMS.mobile,
                    //        message = message,
                    //        status = 1
                    //    };
                    //    result = Notification.Send(queue);
                    //    Logs(200, string.Format("Message part 2 sent to {0}", objSMS.mobile));
                    //}

                    string status = json_r.status == 200 ? "Successful." : "Not successful.";

                    Console.WriteLine(status);
                }
            }
        }

        private List<Cache> get_request()
        {
            List<Cache> caches = new List<Cache>();
            try
            {
                DataTable dt = MySqlQuery.select_dt("SELECT * FROM ptxt_cache WHERE status_ = 1;");
                if (dt != null)
                {
                    DataRow[] datarows = dt.Select();
                    foreach (DataRow dr in datarows)
                    {
                        Cache cache = new Cache()
                        {
                            Id = Convert.ToInt64(dr["Id"]),
                            company_uid = Convert.ToInt64(dr["company_uid_"]),
                            from = dr["from_"].ToString(),
                            command = dr["message_"].ToString(),
                            status = Convert.ToInt16(dr["status_"]),
                            updated_at = Convert.ToDateTime(dr["updated_at"]),
                            created_at = Convert.ToDateTime(dr["created_at"])
                        };

                        caches.Add(cache);
                    }
                }
            }
            catch(Exception ex)
            {
                error_message = "**Forced Stopped - Error**\r\n" + ex.Message;

                Logs(500, ex.Message);
            }

            return caches;
        }

        // monitor wallet
        private void monitor_wallet()
        {
            try
            {
                while (KeepAlive)
                {
                    System.Threading.Thread.Sleep(5000);

                    MotherWallet mw = Json.Wallet();
                    Status.smart = "SMART: " + mw.Smart.smartBalance.ToString("n2");
                    Status.globe = "GLOBE: " + mw.Globe.globeBalance.ToString("n2");
                    Status.sunxx = "SUNXX: " + mw.Sun.sunBalance.ToString("n2");
                    Logs(220, "");
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Exception message: {0}", e.Message);
                Console.WriteLine("Thread Abort Exception - resetting.");
                if (error_message != null)
                {
                    Logs(500, error_message);
                }
                else
                {
                    Logs(-200, "Service disconnected - Forced Stopped");
                }
                Thread.ResetAbort();
            }
        }


        // logs update
        public void Logs(int code, string message)
        {
            Status.code = code;
            Status.message = message;
            Status_Event(this, new StatusArgs());
        }

        // check prefix
        public static string prefixes(string number)
        {
            string net = "INVALID";

            if (number.Length == 11)
            {
                switch (number.Substring(0, 4))
                {
                    case "0900":
                    case "0907":
                    case "0908":
                    case "0909":
                    case "0910":
                    case "0911":
                    case "0912":
                    case "0913":
                    case "0914":
                    case "0918":
                    case "0919":
                    case "0920":
                    case "0921":
                    case "0928":
                    case "0929":
                    case "0930":
                    case "0931":
                    case "0938":
                    case "0939":
                    case "0940":
                    case "0946":
                    case "0947":
                    case "0948":
                    case "0949":
                    case "0950":
                    case "0971":
                    case "0980":
                    case "0989":
                    case "0998":
                    case "0999":
                    case "0813":
                        net = "SMART";
                        break;
                    case "0905":
                    case "0906":
                    case "0915":
                    case "0916":
                    case "0917":
                    case "0926":
                    case "0927":
                    case "0935":
                    case "0936":
                    case "0937":
                    case "0975":
                    case "0977":
                    case "0978":
                    case "0979":
                    case "0994":
                    case "0995":
                    case "0996":
                    case "0997":
                    case "0817":
                        net = "GLOBE";
                        break;
                    case "0922":
                    case "0923":
                    case "0924":
                    case "0925":
                    case "0932":
                    case "0933":
                    case "0934":
                    case "0942":
                    case "0943":
                    case "0944":
                        net = "SUN";
                        break;
                    default:
                        net = "INVALID";
                        break;
                }
            }
            return net;
        }
    }

    public class Cache
    {
        public Int64 Id { get; set; }
        public Int64 company_uid { get; set; }
        public string from { get; set; }
        public string command { get; set; }
        public int status { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }

    

    public class Status
    {
        public static int code { get; set; }
        public static string message { get; set; }
        public static string smart { get; set; }
        public static string globe { get; set; }
        public static string sunxx { get; set; }
    }
}


