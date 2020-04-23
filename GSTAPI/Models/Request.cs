
using GSTAPI.Helper;
using System;
using System.ComponentModel;

namespace GSTAPI.Models
{
    public class Request
    {
        public CipherKeys Keys { get; set; }
        public Header Header { get; set; }
    }
    public class CipherKeys
    {
        public string SessionKey { get; set; }
        public string GSTNAppKey { get; set; }
    }
    public class Header
    {
        [DisplayName("username")]
        public string Username { get; set; }
        [DisplayName("gstin")]
        public string GSTN { get; set; }
        [DisplayName("state-cd")]
        public string StateCode { get; set; }
        [DisplayName("ret_period")]
        public string ReturnPeriod { get; set; }
        [DisplayName("auth-token")]
        public string AuthToken { get; set; }
        [DisplayName("ip-usr")]
        public string IPAddress { get { return RequestHandler.GetExternalPublicIP(); } }
        [DisplayName("txn")]
        public string TransactionId { get { return DateTime.Now.Ticks.ToString(); } }
    }
}
