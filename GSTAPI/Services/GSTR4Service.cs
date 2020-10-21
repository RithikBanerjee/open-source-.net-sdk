using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr4 based api 
    public static class GSTR4Service
    {
        private static readonly string ReturnType = "R4";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        private static Response GetInvoices(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr4);
            return handler.DecryptGetResponse(url, queryString);
        }
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr4);
            return handler.Save("http://localhost:11599/api/returns/gstr4/save", jsonData);
        }
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr4);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr4);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        public static Response GetAdvancesAdjusted(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXP");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetAdvancesAdjustedAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXPA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetAdvancesPaid(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "AT");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetPaidAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ATA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BAmendment(Request userInfo, string returnPeriod, string gstin, string actionRequired = "",string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2BA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BInvoices(Request userInfo, string returnPeriod, string gstin, string actionRequired = "", string counterPartyGSTIN = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2B");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BUnregisteredInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2BUR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BURAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2BURA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNR(Request userInfo, string returnPeriod, string gstin, string actionRequired = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNRAmendment(Request userInfo, string returnPeriod, string gstin, string actionRequired = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNRA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNUR(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNUR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNURAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNURA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "RETSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetImportsOfServices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "IMPS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetImportsOfServicesAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "IMPSA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTaxOnOutwardSupplies(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXOS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTXOSAmendment(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXOSA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return GetInvoices(userInfo, queryString);
        }
    }
}
