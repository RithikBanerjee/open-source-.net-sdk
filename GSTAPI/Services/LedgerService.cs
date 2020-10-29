using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all ledger based api 
    public static class LedgerService
    {
        private static Response Get(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.ledgers);
            return handler.DecryptGetResponse(url, queryString);
        }
        //get advance tax api
        public static Response GetAdvanceTax(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ADVTAXGET");
            queryString.Add("to_time", toTime);
            queryString.Add("from_time", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get cash ITC balance api
        public static Response GetCashITCBalance(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "BAL");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get cash ledger api
        public static Response GetCashLedger(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CASH");
            queryString.Add("to_dt", toTime);
            queryString.Add("fr_dt", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get itc ledger api
        public static Response GetITCLedger(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ITC");
            queryString.Add("to_dt", toTime);
            queryString.Add("fr_dt", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get liability ledger details for return liability api
        public static Response GetLiabilityLedgerDetailsForReturnLiability(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TAX");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get negative liability api
        public static Response GetNegativeLiability(Request userInfo, string returnPeriod, string gstin, string fromDate, string toDate, string financialYear)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "NEGLIABGET");
            queryString.Add("to_date", toDate);
            queryString.Add("from_date", fromDate);
            queryString.Add("finyear", financialYear);
            queryString.Add("rtnprd", returnPeriod);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get data other than returns ledger api
        public static Response GetOtherThanReturnLedger(Request userInfo, string gstin, string fromDate, string toDate, string demandId = "", string stayStatus = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "NRTN");
            queryString.Add("to_date", toDate);
            queryString.Add("from_date", fromDate);
            if(!string.IsNullOrEmpty(demandId))
                queryString.Add("demid", demandId);
            if(!string.IsNullOrEmpty(stayStatus))
                queryString.Add("stayStatus", stayStatus);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get provisional tax credit api
        public static Response GetProvisionalTaxCredit(Request userInfo, string gstin, string fromTime, string toTime)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "PROVITCGET");
            queryString.Add("to_time", toTime);
            queryString.Add("from_time", fromTime);
            queryString.Add("gstin", gstin);

            return Get(userInfo, queryString);
        }
        //get return related liability balance api
        public static Response GetReturnRelatedLiabilityBalance(Request userInfo, string returnPeriod, string gstin, string returnType)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TAXPAYABLE");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            queryString.Add("ret_type", returnType);

            return Get(userInfo, queryString);
        }
    }
}
