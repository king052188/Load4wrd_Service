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
        public static bool Die { get; set; }

        public static bool KeepAlive { get; set; }

        public static Int64 BatchProcessPerDay { get; set; }

        internal MySqlQuery mysqlQuery;

        internal Thread thread_a;
        
        public Request(MySqlClient mysqlClient, string api_webhook)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);

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

                bool save_notification = MySqlQuery.execute("DELETE FROM ptxt_cache WHERE Id = " + cache.Id + ";");

                bool delete_cache = MySqlQuery.execute("DELETE FROM ptxt_cache WHERE Id = " + cache.Id + ";");
            }

            sw.Stop();

            BatchProcessPerDay++;

            decimal total_time_in_seconds = sw.ElapsedMilliseconds / 60;

            Console.WriteLine("Batch: {0} and Time Executed {1} seconds", BatchProcessPerDay, total_time_in_seconds);
        }

        private List<Cache> get_request()
        {
            List<Cache> caches = new List<Cache>();
            DataTable dt = MySqlQuery.select_dt("SELECT * FROM ptxt_cache WHERE status_ = 1;");
            if(dt != null)
            {
                DataRow[] datarows = dt.Select();
                foreach(DataRow dr in datarows)
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

            return caches;
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
}


