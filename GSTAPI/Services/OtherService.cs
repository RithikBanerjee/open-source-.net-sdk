using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class OtherService
    {
        private static Response Get(Request userInfo, NameValueCollection queryString)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns);
            return handler.DecryptGetResponse(url, queryString);
        }
        public static Response GetFileDetails(Request userInfo, string returnPeriod, string gstin, string token)
        {
            var queryString = new NameValueCollection
            {
                { "action", "FILEDET" },
                { "gstin", gstin },
                { "ret_period", returnPeriod },
                { "token", token }
            };

            return Get(userInfo, queryString);
        }
        public static Response GetReturnStatus(Request userInfo, string returnPeriod, string gstin, string referenceId)
        {
            var queryString = new NameValueCollection
            {
                { "action", "RETSTATUS" },
                { "gstin", gstin },
                { "ret_period", returnPeriod },
                { "ref_id", referenceId }
            };

            return Get(userInfo, queryString);
        }
        public static Response TrackReturnStatus(Request userInfo, string returnPeriod, string gstin, string returnType = "")
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection
            {
                { "action", "RETTRACK" },
                { "gstin", gstin },
                { "ret_period", returnPeriod }
            };
            if (!string.IsNullOrEmpty(returnType))
                queryString.Add("type", returnType);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_3, modName.returns);
            return handler.DecryptGetResponse(url, queryString);
        }
        public static Response LateFee(Request userInfo, string returnPeriod, string gstin, string returnType = "")
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection
            {
                { "gstin", gstin },
                { "ret_period", returnPeriod }
            };
            if (!string.IsNullOrEmpty(returnType))
                queryString.Add("type", returnType);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_0, modName.returns_gstr);
            return handler.DecryptGetResponse(url, queryString);
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
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr);
            return handler.DecryptPostResponse(url, "PROCEEDFILE", payload);
        }
        public static Response GetDocumentStatus(Request userInfo, string documentId)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection();
            queryString.Add("action", "DOCSTATUS");
            queryString.Add("doc_id", documentId);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.document);
            return handler.DecryptGetResponse(url, queryString);
        }
        public static Response DownloadDocument(Request userInfo, string documentId)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection
            {
                { "action", "DOCDOWNLOAD" },
                { "doc_id", documentId }
            };
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.document);
            return handler.DecryptGetResponse(url, queryString);
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
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.document);
            return handler.DecryptPostResponse(url, "DOCUPLOAD", payload);
        }
    }
}
