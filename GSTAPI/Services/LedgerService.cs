using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class LedgerService
    {
        private static Response Get(Request userInfo, NameValueCollection queryString, string actionName)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse($"http://localhost:11599/api/ledger/{actionName}", queryString);
        }
        public static Response GetAdvanceTax(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_time", toTime);
            queryString.Add("from_time", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getadvancetax");
        }
        public static Response GetCashITCBalance(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getcashitcbalance");
        }
        public static Response GetCashLedger(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_dt", toTime);
            queryString.Add("fr_dt", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getcashledger");
        }
        public static Response GetITCLedger(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_dt", toTime);
            queryString.Add("fr_dt", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getitcledger");
        }
        public static Response GetLiabilityLedgerDetailsForReturnLiability(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getliabilityledgerdetailsforreturnliability");
        }
        public static Response GetNegativeLiability(Request userInfo, string returnPeriod, string gstin, string fromDate, string toDate, string financialYear)
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_date", toDate);
            queryString.Add("from_date", fromDate);
            queryString.Add("finyear", financialYear);
            queryString.Add("rtnprd", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getnegativeliability");
        }
        public static Response GetOtherThanReturnLedger(Request userInfo, string gstin, string fromDate, string toDate, string demandId = "", string stayStatus = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_date", toDate);
            queryString.Add("from_date", fromDate);
            if(!string.IsNullOrEmpty(demandId))
                queryString.Add("demid", demandId);
            if(!string.IsNullOrEmpty(stayStatus))
                queryString.Add("stayStatus", stayStatus);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getotherthanreturnledger");
        }
        public static Response GetProvisionalTaxCredit(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("to_time", toTime);
            queryString.Add("from_time", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString, "getprovisionaltaxcredit");
        }
        public static Response GetReturnRelatedLiabilityBalance(Request userInfo, string returnPeriod, string gstin, string returnType)
        {
            var queryString = new NameValueCollection();
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            queryString.Add("ret_type", returnType);

            return Get(userInfo, queryString, "getreturnrelatedliabilitybalance");
        }
    }
}
