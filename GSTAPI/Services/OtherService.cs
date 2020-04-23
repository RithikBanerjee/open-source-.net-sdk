using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class OtherService
    {
        private static Response Get(Request userInfo, NameValueCollection queryString, string actionName)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse($"http://localhost:11599/api/returns/{actionName}", queryString);
        }
        public static Response GetFileDetails(Request userInfo, string returnPeriod, string gstin, string token)
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("token", token);

            return Get(userInfo, queryString, "getfile");
        }
        public static Response GetReturnStatus(Request userInfo, string returnPeriod, string gstin, string referenceId)
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("ref_id", referenceId);

            return Get(userInfo, queryString, "getreturnstatus");
        }
        public static Response TrackReturnStatus(Request userInfo, string returnPeriod, string gstin, string returnType = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);
            if (!string.IsNullOrEmpty(returnType))
                queryString.Add("type", returnType);

            return Get(userInfo, queryString, "trackreturnstatus");
        }
        public static Response LateFee(Request userInfo, string returnPeriod, string gstin, string returnType = "")
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("ret_period", returnPeriod);
            if (!string.IsNullOrEmpty(returnType))
                queryString.Add("type", returnType);

            return Get(userInfo, queryString, "latefee");
        }
        public static Response ProceedToFile(Request userInfo, string returnPeriod, string gstin)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var payload = JsonConvert.SerializeObject(new 
            {
                gstin = gstin,
                ret_period = returnPeriod 
            });

            var handler = new RequestHandler(userInfo);
            return handler.DecryptPostResponse($"http://localhost:11599/api/returns/proceedtofile", payload);
        }
        public static Response GetDocumentStatus(Request userInfo, string documentId)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("doc_id", documentId);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse($"http://localhost:11599/api/document/getdocstatus", queryString);
        }
        public static Response DownloadDocument(Request userInfo, string documentId)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("doc_id", documentId);

            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse($"http://localhost:11599/api/document/download", queryString);
        }
        public static Response UploadDocument(Request userInfo, string contentType, string data, string documentName)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var payload = JsonConvert.SerializeObject(new
            {
                ct = contentType,
                data = data,
                doc_nam = documentName 
            });
            var handler = new RequestHandler(userInfo);
            return handler.DecryptPostResponse($"http://localhost:11599/api/returns/upload", payload);
        }
    }
}
