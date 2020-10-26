using System;
using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all gstr9c based api 
    public static class GSTR9CService
    {
        private static readonly string ReturnType = "R9C";
        private static readonly string Version = UrlHandler.GetVersion(version.v1_1);
        //save api
        public static Response Save(Request userInfo, string jsonData)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.Save(url, jsonData);
        }
        //get table 9 for filing 9c api
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
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.DecryptGetResponse(url, queryString);
        }
        //generate certificate api
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
        //file with EVC api
        public static Response FileWithEVC(Request userInfo, string jsonData, string PAN, string OTP)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.File(url, jsonData, Version, ReturnType, $"{PAN}|{OTP}");
        }
        //file with DSC api
        public static Response FileWithDSC(Request userInfo, string jsonData, string signature, string PAN)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_1, modName.returns_gstr9c);
            return handler.File(url, jsonData, Version, ReturnType, PAN, signature);
        }
        //generate hash api
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
