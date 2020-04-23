using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR4Service
    {
        private static string version = "v1.1";
        private static string returnType = "GSTR4";
        private static Response GetInvoices(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr4", queryString);
        }
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.Save("http://localhost:11599/api/returns/gstr4/save", jsonData);
        }
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr4/file", jsonData, version, returnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr4/file", jsonData, version, returnType, PAN, signature);
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
