using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr1 based api 
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
        //file with EVC api 
        /// <summary>File GSTR1 data with signed EVC data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="jsonData">GSTR1 data to file in Json string format</param>
        /// <param name="PAN">PAN number for EVC signature</param>
        /// <param name="OTP">OTP for EVC signature</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api 
        /// <summary>File GSTR1 with signed DSC data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="jsonData">GSTR1 data to file in Json string format</param>
        /// <param name="signature">DSC signature</param>
        /// <param name="PAN">PAN number for EVC signature</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //save api 
        /// <summary>Save GSTR1 with signed DSC data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="jsonData">GSTR1 data to save in Json string format</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.Save(url, jsonData);
        }
        //submit api 
        /// <summary>Submit GSTR1 with signed DSC data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="jsonData">GSTR1 data to submit in Json string format</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response Submit(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr1);
            return handler.Submit(url, jsonData);
        }
        //get return status api 
        /// <summary>Fetch return status of GSTR1 data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">Data of which retrun period of GSTR1</param>
        /// <param name="gstin">Data of which gstin of GSTR1</param>
        /// <param name="transactionId">Internal transaction Id of GSTR1 return</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
        //get added tax api
        /// <summary>Fetch GSTR1 AT data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">Data of which retrun period of GSTR1</param>
        /// <param name="gstin">Data of which gstin of GSTR1</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetAT(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "AT");
            return GetInvoices(userInfo, queryString);
        }
        //get added tax amendment api
        /// <summary>Fetch GSTR1 AT Amemdment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetATA(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "ATA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get B2B api
        /// <summary>Fetch GSTR1 B2B data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="actionRequired">GSTR1 data of which action(format: 'Y'/'N')</param>
        /// <param name="counterPartyGSTIN">GSTR1 data of which counter party gstin</param>
        /// <param name="fromWhichTime">GSTR1 data from which time period (format: 'MMyyyy')</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
        //get B2B amendment api
        /// <summary>Fetch GSTR1 B2B Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="actionRequired">GSTR1 data of which action(format: 'Y'/'N')</param>
        /// <param name="counterPartyGSTIN">GSTR1 data of which counter party gstin</param>
        /// <param name="fromWhichTime">GSTR1 data from which time period (format: 'MMyyyy')</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
        //get B2C large scale api
        /// <summary>Fetch GSTR1 B2CL data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="stateCode">GSTR1 data of which state code</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetB2CLInvoices(Request userInfo, string returnPeriod, string gstin, string stateCode)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CL");
            queryString.Add("state_cd", stateCode);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get B2C large scale amendment api
        /// <summary>Fetch GSTR1 B2CL Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="stateCode">GSTR1 data of which state code</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetB2CLAInvoices(Request userInfo, string returnPeriod, string gstin, string stateCode)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CLA");
            queryString.Add("state_cd", stateCode);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get B2C small scale api
        /// <summary>Fetch GSTR1 B2CS data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetB2CSInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get B2C small scale amendment api
        /// <summary>Fetch GSTR1 B2CS Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetB2CSAInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "B2CSA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get credit debit note from registered api
        /// <summary>Fetch GSTR1 CDNR data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="actionRequired">GSTR1 data of which action(format: 'Y'/'N')</param>
        /// <param name="fromWhichTime">GSTR1 data from which time period (format: 'MMyyyy')</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
        //get credit debit note from registered amendment api
        /// <summary>Fetch GSTR1 CDNR Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <param name="actionRequired">GSTR1 data of which action(format: 'Y'/'N')</param>
        /// <param name="fromWhichTime">GSTR1 data from which time period (format: 'MMyyyy')</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
        //get credit debit note from unregistered api
        /// <summary>Fetch GSTR1 CDNUR data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetCDNURInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNUR");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get credit debit note from unregistered amendment api
        /// <summary>Fetch GSTR1 CDNUR Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetCDNURAInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "CDNURA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get issued document api
        /// <summary>Fetch GSTR1 documents issued data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetDocIssued(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "DOCISS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get export api
        /// <summary>Fetch GSTR1 EXP data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetEXP(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "EXP");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get export amendment api
        /// <summary>Fetch GSTR1 EXP Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetEXPA(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "EXPA");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get summary api
        /// <summary>Fetch GSTR1 summary</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "RETSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get HSN summary api
        /// <summary>Fetch GSTR1 HSN summary</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetHSNSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "HSNSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get nil rated api
        /// <summary>Fetch GSTR1 Nil rated summary</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetNilRatedSummary(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "NIL");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get taxpayer data api
        /// <summary>Fetch GSTR1 TXP data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
        public static Response GetTXP(Request userInfo, string returnPeriod, string gstin)
        {
            var queryString = new NameValueCollection();
            queryString.Add("action", "TXP");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            return GetInvoices(userInfo, queryString);
        }
        //get taxpayer amendment data api
        /// <summary>Fetch GSTR1 TXP Amendment data</summary>
        /// <param name="userInfo">User details in GSTAPI.Models.Request Model</param>
        /// <param name="returnPeriod">GSTR1 data of which retrun period (format: 'MMyyyy')</param>
        /// <param name="gstin">GSTR1 data of which gstin</param>
        /// <returns>GSTAPI.Models.Response after decryption of GST Portal's response</returns>
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
