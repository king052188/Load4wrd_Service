using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using l4dhelper.Data.MySqlClient;

namespace l4dhelper
{
    public class Notification
    {
        internal MySqlQuery mysqlQuery;

        public string date_time_format(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public DateTime date_time_now()
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //Set the time zone information to Taipei Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //Print out the date and time
            return dateTime;
        }

        public Notification(MySqlClient mysqlClient)
        {
            mysqlQuery = new MySqlQuery(mysqlClient);
        }

        public bool Send(Queued queued)
        {
            string dt = date_time_format(date_time_now());

            string message = Encryption.Do(queued.message);

            string sql_string = "INSERT INTO ptxt_queued (Company_uid, UserId, UserIp, ToNumber, ToMessage, Status, updated_at, created_at) VALUES " +
                                         "(" + queued.company_uid + ", '" + queued.mobile + "', 'N/A', '" + queued.mobile + "', '" + message + "', " + queued.status +
                                         ", DATE_FORMAT('" + dt + "','%Y-%m-%d %H:%i:%s'), " +
                                         "DATE_FORMAT('" + dt + "','%Y-%m-%d %H:%i:%s'));";

            bool r = MySqlQuery.execute(sql_string);

            return r;
        }
    }

    public class Queued
    {
        public Int64 company_uid { get; set; }
        public string mobile { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }
}
