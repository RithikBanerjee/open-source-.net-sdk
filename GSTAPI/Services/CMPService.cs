using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all CMP08 based api 
    public static class CMPService
    {
        private static readonly string ReturnType = "CMP08";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        //get api 
        public static Response GetDetails(Request userInfo, string gstin, string returnPeriod)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "RECORDS");
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.cmp);
            return handler.DecryptGetResponse(url, queryString);
        }
        //file with EVC api 
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.cmp);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api 
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.cmp);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //save api 
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.cmp);
            return handler.Save(url, jsonData);
        }
    }
}
