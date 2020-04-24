using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class ITC04Service
    {
        private static readonly string ReturnType = "ITC04";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.Save(url, jsonData);
        }
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_itc04);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
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
