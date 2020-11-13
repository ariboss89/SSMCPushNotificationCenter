using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSMCPushNotificationCenter.Services
{
    public class ApiService
    {
        string url = "http://192.168.43.62/api/notif/";
        //string url = "http://localhost:56932/api/notif/";

        public string Url()
        {
            return url;
        }

        public string GetData()
        {
            return url + "GetData";
        }

        public string PostNotification()
        {
            return url + "PostNotif";
        }

        public string PutNotification()
        {
            return url + "PutNotification";
        }

        public string GetDataById(int Id)
        {
            return url + "GetDataById?Id="+Id;
        }
    }
}