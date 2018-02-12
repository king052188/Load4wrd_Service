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

        public static string AccessToken { get; set; }

        public static CMDTransaction Send(string account, string command)
        {
            CMDTransaction trans = new CMDTransaction();

            string url_path = ApiUrl + "/api/v1/load/command/sms/" + AccessToken + "?";

            string url = url_path + "account=" + account + "&command=" + command.Replace(" ", "%20");

            try
            {
                WebClient client = new WebClient();
                string value = client.DownloadString(url);
                trans = JsonConvert.DeserializeObject<CMDTransaction>(value);
            }
            catch (Exception ex)
            { }
            return trans;
        }

        public static MotherWallet Wallet()
        {
            MotherWallet mw = new MotherWallet();
        
            string url = "http://api-load4wrd.kpa.ph/balance.aspx";

            try
            {
                WebClient client = new WebClient();
                string value = client.DownloadString(url);
                mw = JsonConvert.DeserializeObject<MotherWallet>(value);
            }
            catch (Exception ex)
            { }
            return mw;
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

        public static JSON_Response Messenger_Send(string facebook_id, string message)
        {
            string url = ApiUrl + "/api/v1/messenger/send/" + facebook_id + "?message=" + message;  

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

    public class CMDTransaction
    {
        public int status { get; set; }
        public string account { get; set; }
        public string message { get; set; }
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

    public class MotherWallet
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string DateTime { get; set; }
        public SMART Smart { get; set; }
        public GLOBE Globe { get; set; }
        public SUNXX Sun { get; set; }
    }

    public class SMART
    {
        public int smartResultCode { get; set; }
        public decimal smartCredits { get; set; }
        public decimal smartDebits { get; set; }
        public decimal smartBalance { get; set; }
        public string smartResultCodeDesc { get; set; }
        public string smartSessionID { get; set; }
    }

    public class GLOBE
    {
        public int globeResultCode { get; set; }
        public string globeRequestStatus { get; set; }
        public string globeTransactionNo { get; set; }
        public decimal globeBalance { get; set; }
    }

    public class SUNXX
    {
        public int sunResultCode { get; set; }
        public decimal sunCredits { get; set; }
        public decimal sunDebits { get; set; }
        public decimal sunBalance { get; set; }
        public string sunResultCodeDesc { get; set; }
        public string sunSessionID { get; set; }
    }
}
