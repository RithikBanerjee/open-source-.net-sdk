using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GSTAPI.Helper;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Services
{
    public static class CommonService
    {
        public static Response AuthRequest(string username, string password, string appKey)
        {
            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new JObject()
                {
                    { "action", "ACCESSTOKEN" },
                    { "username", username },
                    { "password", Cryptography.EncryptData(password, appKey) },
                    { "app_key", Cryptography.EncryptTextWithGSTNPublicKey(appKey) }
                });
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler();
            return handler.Post("http://localhost:11599/api/commonapi/authrequest", payload);
        }
        public static Response Logout(string username, string appKey, string authToken)
        {
            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new JObject()
                {
                    { "action", "LOGOUT" },
                    { "username", username },
                    { "authtoken", authToken },
                    { "app_key", Cryptography.EncryptTextWithGSTNPublicKey(appKey) }
                });
            }
            catch (Exception)
            {
                return RequestHandler.ErrorResponse("GSP141", "Error encrypting payload");
            }
            var handler = new RequestHandler();
            handler.UserRequest.Header.Username = username;
            handler.UserRequest.Header.AuthToken = authToken;

            return handler.Post("http://localhost:11599/api/commonapi/logout", payload);
        }
        public static Response SearchTaxpayer(string username, string authToken, string gstinToSearch)
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstinToSearch);

            var handler = new RequestHandler();
            handler.UserRequest.Header.Username = username;
            handler.UserRequest.Header.AuthToken = authToken;

            return handler.Get("http://localhost:11599/api/commonapi/search", queryString);
        }
        public static Response TrackReturnStatus(string username, string authToken, CipherKeys keys, string gstin, string returnPeriod, string returnType)
        {
            var queryString = new NameValueCollection();
            queryString.Add("gstin", gstin);
            queryString.Add("fy", returnPeriod);
            queryString.Add("type", returnType);

            var handler = new RequestHandler();
            handler.UserRequest.Keys = keys;
            handler.UserRequest.Header.Username = username;
            handler.UserRequest.Header.AuthToken = authToken;

            return handler.DecryptGetResponse("http://localhost:11599/api/commonapi/trackreturnstatus", queryString);
        } 
    }
}
