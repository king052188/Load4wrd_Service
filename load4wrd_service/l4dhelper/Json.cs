using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace l4dhelper
{
    public class Json
    {
        public static string ApiUrl { get; set; }
        
        public static LoadTransaction Send(string request_type, string mobile, string network, string command)
        {
            string url_path = ApiUrl + "/api/v1/load/command";
            if (request_type == "sms")
            {
                url_path = url_path + "/sms";
            }

            LoadTransaction trans = new LoadTransaction();
            string url = url_path + "?mobile=" + mobile + "&network=" + network + "&command=" + command.Replace(" ", "%20");

            try
            {
                WebClient client = new WebClient();
                string value = client.DownloadString(url);
                trans = JsonConvert.DeserializeObject<LoadTransaction>(value);
            }
            catch (Exception ex)
            { }
            return trans;
        }

        public static SMSQ Get()
        {
            string url = ApiUrl + "/api/v1/sms/get";
            SMSQ smsq = new SMSQ();

            try
            {
                WebClient client = new WebClient();
                string value = client.DownloadString(url);
                smsq = JsonConvert.DeserializeObject<SMSQ>(value);
            }
            catch (Exception ex)
            { }
            return smsq;
        }

        public static JSON_Response Update(Int64 id)
        {
            string url = ApiUrl + "/api/v1/sms/update/" + id;
            JSON_Response json = new JSON_Response();

            try
            {
                WebClient client = new WebClient();
                string value = client.DownloadString(url);
                json = JsonConvert.DeserializeObject<JSON_Response>(value);
            }
            catch (Exception ex)
            { }
            return json;
        }
    }

    public class LoadTransaction
    {
        public int status { get; set; }
        public string message { get; set; }
        public string facebook_id { get; set; }
        public string reference_number { get; set; }
        public string target_mobile { get; set; }
        public string product_code { get; set; }
        public decimal load_amount { get; set; }
    }

    public class SmartSun
    {
        public int status { get; set; }
        public string message { get; set; }
        public LoadCommitted committed { get; set; }
        public LoadVerified verified { get; set; }
    }

    public class LoadCommitted
    {
        public string topupResultCode { get; set; }
        public string topupResultCodeDesc { get; set; }
        public string topupSessionID { get; set; }

        public string commitResultCode { get; set; }
        public string commitResultCodeDesc { get; set; }
        public string commitSessionID { get; set; }
    }

    public class LoadVerified
    {
        public string ResultCode { get; set; }
        public string TransactionStatus { get; set; }
        public string ReferenceNo { get; set; }
        public string TargetNo { get; set; }
        public decimal Amount { get; set; }
        public string ProductCode { get; set; }
        public string SessionID { get; set; }
    }

    public class SMSQ
    {
        public int status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<SMSQueues> data { get; set; }
    }

    public class SMSQueues
    {
        public Int64 Id { get; set; }
        public Int64 company_uid { get; set; }
        public string user_id { get; set; }
        public string user_ip_address { get; set; }
        public string mobile { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }

    public class JSON_Response
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
