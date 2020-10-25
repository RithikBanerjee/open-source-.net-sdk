using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr7 based api 
    public static class GSTR7Service
    {
        private static readonly string ReturnType = "R7";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        //file with EVC api
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr7);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr7);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //save api
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr7);
            return handler.Save(url, jsonData);
        }
        //get TDS checksum api
        public static Response GetTDSChecksum(Request userInfo, string returnPeriod, string gstin, string fromTime = "", string recType = "")
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "CHECKSUM");
            if (!string.IsNullOrEmpty(fromTime))
                queryString.Add("from_time", fromTime);
            if (!string.IsNullOrEmpty(recType))
                queryString.Add("rec_type", recType);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr7);
            return handler.DecryptGetResponse(url, queryString);
        }
        //get TDS details api
        public static Response GetTDSDetails(Request userInfo, string returnPeriod, string gstin, string fromTime = "", string recType = "")
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "TDS");
            if (!string.IsNullOrEmpty(fromTime))
                queryString.Add("from_time", fromTime);
            if (!string.IsNullOrEmpty(recType))
                queryString.Add("rec_type", recType);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr7", queryString);
        }
    }
}
