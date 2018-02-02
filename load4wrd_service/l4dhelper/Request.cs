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

        public static string error_message { get; set; }

        public static Int64 total_process { get; set; }

        internal MySqlQuery mysqlQuery;

        internal Notification notification;

        internal Thread thread_a;
        
        public Request(MySqlClient mysqlClient, string api_webhook)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);

            notification = new Notification(mysqlClient);

            thread_a = new Thread(process);

            Json.ApiUrl = api_webhook;

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
            thread_a.Start();
        }

        public void Stop()
        {
            if (!KeepAlive)
            {
                return;
            }

            KeepAlive = false;
            thread_a.Abort();
        }

        private void process()
        {
            try
            {
                while (KeepAlive)
                {
                    System.Threading.Thread.Sleep(500);

                    init();
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
                    Logs(403, "Thread Abort Exception - Forced Stopped");
                }
                
                Thread.ResetAbort();
            }
        }

        private void init()
        {
            var sw = Stopwatch.StartNew();

            List<Cache> requests = get_request();

            foreach(Cache cache in requests)
            {
                string command = "LOAD " + cache.command;

                LoadTransaction tx = Json.Send("sms", cache.from, "SMART", command);
                
                Queued queue = new Queued()
                {
                    company_uid = cache.company_uid,
                    mobile = cache.from,
                    message = tx.message,
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

            Logs(200, "Running....");
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

        public void Logs(int code, string message)
        {
            Status.code = code;
            Status.message = message;
            Status_Event(this, new StatusArgs());
        }

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
    }
}


