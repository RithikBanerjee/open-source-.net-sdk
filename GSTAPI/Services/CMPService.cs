using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class CMPService
    {
        private static string version = "v1.1";
        private static string returnType = "CMP";
        public static Response GetDetails(Request userInfo, string gstin, string returnPeriod)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse("http://localhost:11599/api/cmp/get", queryString);
        }

        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/cmp/file", jsonData, version, returnType, $"{PAN}|{OTP}");
        }

        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/cmp/file", jsonData, version, returnType, PAN, signature);
        }

        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.Save("http://localhost:11599/api/cmp/save", jsonData);
        }
    }
}
