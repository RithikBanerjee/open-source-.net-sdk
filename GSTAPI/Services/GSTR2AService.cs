using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR2AService
    {
        private static Response GetInvoices(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr2a", queryString);
        }
        public static Response GetB2BInvoices(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "", string fromWhichTime = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2B");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            if (!string.IsNullOrEmpty(fromWhichTime))
                queryString.Add("from_time", fromWhichTime);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BAInvoices(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2BA");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNInvoices(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "", string fromWhichTime = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDN");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            if (!string.IsNullOrEmpty(fromWhichTime))
                queryString.Add("from_time", fromWhichTime);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNAInvoices(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNA");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetISDCredit(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ISD");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetISDACredit(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ISDA");
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTCSCredit(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TCS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTDSCredit(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TDS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTDSACredit(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TDSA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
    }
}
