using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all itc04 based api 
    public static class ITC04Service
    {
        private static readonly string ReturnType = "ITC04";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        //save api
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.Save(url, jsonData);
        }
        //file with EVC api
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //get summary api
        public static Response GetSummary(Request userInfo, string returnPeriod, string gstin)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "RETSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.DecryptGetResponse(url, queryString);
        }
        //get data api
        public static Response GetInvoices(Request userInfo, string returnPeriod, string gstin)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "GET");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.DecryptGetResponse(url, queryString);
        }
    }
}
