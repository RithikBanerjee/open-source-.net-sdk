using System;
using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class GSTR9CService
    {
        private static readonly string ReturnType = "R9C";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.Save(url, jsonData);
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
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.DecryptGetResponse(url, queryString);
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
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.DecryptGetResponse(url, queryString);
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
                    EncryptedData = CipherHandler.EncryptData(jsonData, userInfo.Keys.SessionKey),
                    HAMCData = CipherHandler.Hmac(jsonData, userInfo.Keys)
                });
                
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.Put(url, "RETGENCERT", payload);
        }
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        public static Response HashGenerator(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayload()
                {
                    APIAction = "RETGENHASH",
                    EncryptedData = CipherHandler.EncryptData(jsonData, userInfo.Keys.SessionKey),
                    HAMCData = CipherHandler.Hmac(jsonData, userInfo.Keys)
                });

            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.Put(url, "RETGENHASH", payload);
        }
    }
}
