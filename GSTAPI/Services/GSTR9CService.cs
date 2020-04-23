using System;
using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR9CService
    {
        private static string version = "v1.1";
        private static string returnType = "GSTR9C";
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.Save("http://localhost:11599/api/returns/gstr9c/save", jsonData);
        }
        public static Response Get9RecordsFor9C(Request userInfo, string returnPeriod, string gstin)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);
            
            var queryString = new NameValueCollection();
            queryString.Add("action", "RECDS");
            queryString.Add("ret_period", returnPeriod);
            queryString.Add("gstin", gstin);
            
            var handler = new RequestHandler(userInfo);
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr9c", queryString);
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
            return handler.DecryptGetResponse("http://localhost:11599/api/returns/gstr9c", queryString);
        }
        public static Response GenerateCertificate(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayload()
                {
                    APIAction = "RETGENCERT",
                    EncryptedData = Cryptography.EncryptData(jsonData, userInfo.Keys.SessionKey),
                    HAMCData = Cryptography.Hmac(jsonData, userInfo.Keys)
                });
                
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            return handler.Put("http://localhost:11599/api/returns/gstr9c/generate", payload);
        }
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr9c/file", jsonData, version, returnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            return handler.File("http://localhost:11599/api/returns/gstr9c/file", jsonData, version, returnType, PAN, signature);
        }
    }
}
