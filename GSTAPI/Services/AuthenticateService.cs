using System;
using Newtonsoft.Json;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    //class to call all authentication based api 
    public static class AuthenticateService
    {
        //initiate OTP For EVC api 
        public static Response InitiateOTPForEVC(Request userInfo, string gstin, string pan = "", string formType = "")
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            var queryString = new NameValueCollection
            {
                { "action", "EVCOTP" },
                { "gstin", gstin }
            };
            if (!string.IsNullOrEmpty(pan))
                queryString.Add("pan", pan);
            if (!string.IsNullOrEmpty(formType))
                queryString.Add("formType", formType);

            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v1_0, modName.authenticate);
            return handler.Get(url, queryString);
        }
        //request for OTP api 
        public static Response RequestForOtp(Request userInfo)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new
                {
                    action = "OTPREQUEST",
                    app_key =  CipherHandler.EncryptTextWithGSTNPublicKey(userInfo.Keys.GSTNAppKey),
                    username = userInfo.Header.Username 
                });
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_2, modName.authenticate);
            return handler.DecryptPostResponse(url, "OTPREQUEST", payload);
        }
        //request for authentication token api 
        public static AuthResponse RequestForAuthToken(Request userInfo, string otp)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.AuthErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new 
                {
                    action =  "AUTHTOKEN",
                    app_key = CipherHandler.EncryptTextWithGSTNPublicKey(userInfo.Keys.GSTNAppKey),
                    username = userInfo.Header.Username,
                    otp = CipherHandler.EncryptData(otp, userInfo.Keys.GSTNAppKey) 
                });
            }
            catch (Exception)
            {
                return RequestHandler.AuthErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            return handler.PostAuthResponse(payload);
        }
        //request for extension of authentication token api 
        public static Response RequestForExtensionOfAuthToken(Request userInfo)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new 
                {
                    action = "REFRESHTOKEN",
                    app_key = CipherHandler.EncryptTextWithGSTNPublicKey(userInfo.Keys.GSTNAppKey),
                    username = userInfo.Header.Username,
                    auth_token = userInfo.Header.AuthToken 
                });
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_1, modName.authenticate);
            return handler.DecryptPostResponse(url, "REFRESHTOKEN", payload);
        }
        //log out api 
        public static Response Logout(Request userInfo)
        {
            if (!RequestHandler.IsRequestNull(userInfo, out string message))
                return RequestHandler.ErrorResponse("GSP121", message);

            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new 
                {
                    action = "LOGOUT",
                    app_key = CipherHandler.EncryptTextWithGSTNPublicKey(userInfo.Keys.GSTNAppKey),
                    username = userInfo.Header.Username,
                    auth_token = userInfo.Header.AuthToken
                });
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler(userInfo);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_2, modName.authenticate);
            return handler.Post(url, "LOGOUT", payload);
        }
    }
}
