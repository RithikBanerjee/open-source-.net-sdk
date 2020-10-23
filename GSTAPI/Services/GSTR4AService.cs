using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr4a based api 
    public static class GSTR4AService
    {
        private static Response GetInvoices(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr4a);
            return handler.DecryptGetResponse(url, queryString);
        }
        //get B2B amendment api
        public static Response GetB2BAmendment(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN ="")
        {
            var queryString = new NameValueCollection();
            queryString.Add("sec_num", "B2BA");
            queryString.Add("action", "B2BA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            return GetInvoices(userInfo, queryString);
        }
        //get B2B api
        public static Response GetB2BInvoices(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("sec_num", "B2B");
            queryString.Add("action", "B2B");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);

            return GetInvoices(userInfo, queryString);
        }
        //get credit debit note for registered api
        public static Response GetCDNR(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            return GetInvoices(userInfo, queryString);
        }
        //get credit debit note for registered amendment api
        public static Response GetCDNRAmendment(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNRA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            return GetInvoices(userInfo, queryString);
        }
        //get TDS api
        public static Response GetTDSDetails(Request userInfo, string returnPeriod, string gstin, string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TDS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            return GetInvoices(userInfo, queryString);
        }
    }
}
