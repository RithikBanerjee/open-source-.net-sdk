using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr3b based api 
    public static class GSTR3BService
    {
        private static readonly string ReturnType = "R3B";
        private static readonly string Version = UrlHandler.GetVersion(version.v0_3);
        //get data api
        public static Response GetDetails(Request userInfo, string returnPeriod, string gstin)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "RETSUM");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.DecryptGetResponse(url, queryString);
        }
        //file with EVC api
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //save api
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.Save(url, jsonData);
        }
        //submit api
        public static Response Submit(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.Submit(url, jsonData);
        }
        //offset api
        public static Response Offset(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns_gstr3b);
            return handler.Offset(url, jsonData);
        }
    }
}
