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
        internal MySqlQuery mysqlQuery;

        public Request(MySqlClient mysqlClient)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);
        } 

        public List<Cache> get_request()
        {
            List<Cache> caches = new List<Cache>();
            DataTable dt = MySqlQuery.select_dt("SELECT * FROM ptxt_cache WHERE status_ = 0;");
            if(dt != null)
            {
                DataRow[] datarows = dt.Select();
                foreach(DataRow dr in datarows)
                {
                    Cache cache = new Cache()
                    {
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
        public Int64 company_uid { get; set; }
        public string from { get; set; }
        public string command { get; set; }
        public int status { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }
}


