using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR7Service
    {
        private static string version = "v1.1";
        private static string returnType = "GSTR7";
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr7/file", jsonData, version, returnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr7/file", jsonData, version, returnType, PAN, signature);
        }
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.Save("http://localhost:11599/api/returns/gstr7/save", jsonData);
        }
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
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr7", queryString);
        }
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
