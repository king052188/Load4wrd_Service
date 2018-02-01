using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;

using l4dhelper.Data.MySqlClient;


namespace l4dhelper
{
    public class Request
    {
        public static bool Stop { get; set; }
        internal MySqlQuery mysqlQuery;

        
        public Request(MySqlClient mysqlClient, string api_webhook)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);

            Json.ApiUrl = api_webhook;

            Stop = false;
        }

        public void init()
        {
            List<Cache> requests = get_request();

            foreach(Cache cache in requests)
            {
                string command = "LOAD " + cache.command;
                LoadTransaction tx = Json.Send("sms", cache.from, "SMART", command);
            }

            System.Threading.Thread.Sleep(500);
            if(Stop)
            {
                return;
            }
            init();
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


