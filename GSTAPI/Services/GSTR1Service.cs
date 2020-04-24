using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR1Service
    {
        private static readonly string ReturnType = "R1";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        private static Response GetInvoices(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.DecryptGetResponse(url, queryString);
        }
        /// <summary>Files GSTR1 data signed with EVC signature.</summary>
        /// <param name="jsonData">The GSTR1 data to file in Json string format.</param>
        /// <param name="PAN">The PAN number of EVC signature with which the jsonData is to be signed.
        /// </param>
        /// <param name="OTP">The OTP got when requested for EVC signature.</param>
        /// <returns>A GSTAPI.Models.Response containing the response sent by the GSTIN server.
        /// </returns>
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.Save(url, jsonData);
        }
        public static Response Submit(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.Submit(url, jsonData);
        }
        public static Response GetReturnStatus(Request userInfo, string returnPeriod, string gstin, string transactionId)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var queryString = new NameValueCollection();
            queryString.Add("action", "RETSTATUS");
            queryString.Add("trans_id", transactionId);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_2, modName.returns_gstr1);
            return handler.DecryptGetResponse(url, queryString);
        }
        public static Response GetAT(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "AT");
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetATA(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ATA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BInvoices(Request userInfo, string returnPeriod, string gstin, string actionRequired = "", string counterPartyGSTIN = "", string fromWhichTime = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2B");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);
            if (!string.IsNullOrEmpty(counterPartyGSTIN))
                queryString.Add("ctin", counterPartyGSTIN);
            if (!string.IsNullOrEmpty(fromWhichTime))
                queryString.Add("from_time", fromWhichTime);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2BAInvoices(Request userInfo, string returnPeriod, string gstin, string actionRequired = "", string counterPartyGSTIN = "")
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
        public static Response GetB2CLInvoices(Request userInfo, string returnPeriod, string gstin, string stateCode)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CL");
            queryString.Add("state_cd", stateCode);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2CLAInvoices(Request userInfo, string returnPeriod, string gstin, string stateCode)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CLA");
            queryString.Add("state_cd", stateCode);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2CSInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetB2CSAInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CSA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNRInvoices(Request userInfo, string returnPeriod, string gstin, string actionRequired = "", string fromWhichTime = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);
            if (!string.IsNullOrEmpty(fromWhichTime))
                queryString.Add("from_time", fromWhichTime);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNRAInvoices(Request userInfo, string returnPeriod, string gstin, string actionRequired = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNRA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            if (!string.IsNullOrEmpty(actionRequired))
                queryString.Add("action_required", actionRequired);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNURInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNUR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetCDNURAInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNURA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetDocIssued(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "DOCISS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetEXP(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "EXP");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetEXPA(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "EXPA");
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
        public static Response GetHSNSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "HSNSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetNilRatedSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "NIL");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTXP(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXP");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        public static Response GetTXPA(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXPA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
    }
}
