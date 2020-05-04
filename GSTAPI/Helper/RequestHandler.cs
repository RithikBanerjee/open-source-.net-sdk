using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using GSTAPI.Models;
using System.Collections.Specialized;

namespace GSTAPI.Helper
{
    internal class RequestHandler
    {
        public Request UserRequest;
        public RequestHandler()
        {
            UserRequest = new Request()
            {
                Header = new Header(),
                Keys = new CipherKeys()
            };
        }
        public RequestHandler(Request info)
        {
            UserRequest = info;
        }

        public static string FetchNullFields(object subject)
        {
            var subjectType = subject.GetType();
            if (subject == null)
                return subjectType.Name;

            var properties = subjectType.GetProperties();
            var nullFeilds = new string[] { };
            foreach (var property in properties)
            {
                if (string.Equals(property.Name, "authtoken", StringComparison.InvariantCultureIgnoreCase) || string.Equals(property.Name, "sessionkey", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                if (property.GetValue(subject) == null)
                    nullFeilds.SetValue(property.Name, nullFeilds.Length);
            }
            return string.Join(",", nullFeilds);
        }


        public static bool IsRequestNull(Request info, out string message)
        {
            message = "Request is null";
            if (info == null)
                return false;

            var nullmessage = FetchNullFields(info.Header);
            message = $"{nullmessage} in Request Header is null";
            if (!string.IsNullOrEmpty(nullmessage))
                return false;

            nullmessage = FetchNullFields(info.Keys);
            message = $"{nullmessage} in Request Keys is null";
            if (!string.IsNullOrEmpty(nullmessage))
                return false;

            return true;
        }

        public static string GetExternalPublicIP()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return new WebClient().DownloadString("https://ipv4.icanhazip.com/");
        }
        

        private WebClient FetchWebClient(NameValueCollection queryString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var gstnHeader = new WebHeaderCollection();
            var properties = typeof(Header).GetProperties();
            foreach(var property in properties)
            {
                var propertyValue = property.GetValue(UserRequest.Header);
                if (propertyValue == null)
                    continue;

                dynamic attribute = property.GetCustomAttributes(false).First();
                gstnHeader.Add(attribute.DisplayName, propertyValue.ToString());
            }
            gstnHeader.Add(HttpRequestHeader.ContentType, "application/json");
            // mail me for demo client id and client secret
            gstnHeader.Add("clientid", "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            gstnHeader.Add("client-secret", "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            return new WebClient()
            {
                Headers = gstnHeader,
                Encoding = System.Text.Encoding.UTF8,
                QueryString = queryString
            };
        }


        
        private Response ScanResponse(string responseString, bool decryptData = false)
        {
            var response = JsonConvert.DeserializeObject<ResponsePayload>(responseString);
            var responseObject = new Response
            {
                OutcomeTransaction = $"txn: {UserRequest.Header.TransactionId}"
            };

            if (Equals(response.StatusCode, 0))
            {
                var erroPayload = JsonConvert.DeserializeObject<ErrorResponsePayload>(responseString);
                responseObject.OutcomeTransaction = $"{responseObject.OutcomeTransaction} error_cd:{erroPayload.error.error_cd} message:{erroPayload.error.message}";
                return responseObject;
            }

            responseObject.Status = true;
            if (response.EncryptedData == null)
                return responseObject;

            responseObject.Data = decryptData ? CipherHandler.DecryptResponseData(response.EncryptedData, response.ResponseKey, UserRequest.Keys) : response.EncryptedData;
            return responseObject;
        }
        public static Response ErrorResponse(string code, string message)
        {
            return new Response()
            {
                Status = false,
                OutcomeTransaction = $"{code} {message}"
            };
        }
        public static AuthResponse AuthErrorResponse(string code, string message)
        {
            return new AuthResponse()
            {
                Status = false,
                OutcomeTransaction = $"{code} {message}"
            };
        }




        public Response DecryptGetResponse(string address, NameValueCollection queryString)
        {
            var client = FetchWebClient(queryString);
            try
            {
                var responseString = client.DownloadString(address);
                return ScanResponse(responseString, true);
            }
            catch (Exception)
            {
                return ErrorResponse("GSP161", "Error processing request");
            }
        }

        public Response Get(string address, NameValueCollection queryString)
        {
            var client = FetchWebClient(queryString);
            try
            {
                var responseString = client.DownloadString(address);
                return ScanResponse(responseString);
            }
            catch (Exception)
            {
                return ErrorResponse("GSP161", "Error processing request");
            }
        }


        public AuthResponse PostAuthResponse(string requestPayload)
        {
            var queryString = new NameValueCollection
            {
                { "action", "AUTHTOKEN" }
            };
            var client = FetchWebClient(queryString);
            var url = UrlHandler.Route(accessGroup.taxpayerapi, version.v0_2, modName.authenticate);
            try
            {
                var responseString = client.UploadString(url, "Post", requestPayload);
                var response = JsonConvert.DeserializeObject<AuthResponsePayload>(responseString);
                var responseObject = new AuthResponse
                {
                    OutcomeTransaction = $"txn: {UserRequest.Header.TransactionId}"
                };

                if (Equals(response.StatusCode, 0))
                {
                    var erroPayload = JsonConvert.DeserializeObject<ErrorResponsePayload>(responseString);
                    responseObject.OutcomeTransaction = $"{responseObject.OutcomeTransaction} error_cd:{erroPayload.error.error_cd} message:{erroPayload.error.message}";
                    return responseObject;
                }
                responseObject.Status = true;
                responseObject.AuthToken = response.AuthToken;
                responseObject.SessionKey = response.SessionKey;
                return responseObject;
            }
            catch (Exception)
            {
                return AuthErrorResponse("GSP161", "Error processing request");
            }
        }


        public Response DecryptPostResponse(string address, string action, string requestPayload)
        {
            var queryString = new NameValueCollection
            {
                { "action", action }
            };
            var client = FetchWebClient(queryString);
            try
            {
                var responseString = client.UploadString(address, "Post", requestPayload);
                return ScanResponse(responseString, true);
            }
            catch (Exception)
            {
                return ErrorResponse("GSP161", "Error processing request");
            }
        }
        public Response Post(string address, string action, string requestPayload)
        {
            var queryString = new NameValueCollection
            {
                { "action", action }
            };
            var client = FetchWebClient(queryString);
            try
            {
                var responseString = client.UploadString(address, "Post", requestPayload);
                return ScanResponse(responseString);
            }
            catch (Exception)
            {
                return ErrorResponse("GSP161", "Error processing request");
            }
        }

        


        public Response Put(string address, string action, string requestPayload)
        {
            var queryString = new NameValueCollection
            {
                { "action", action }
            };
            var client = FetchWebClient(queryString);
            try
            {
                var responseString = client.UploadString(address, "Put", requestPayload);
                return ScanResponse(responseString, true);
            }
            catch (Exception)
            {
                return ErrorResponse("GSP161", "Error processing request");
            }
        }







        public Response Offset(string address, string jsonData)
        {
            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayload()
                {
                    APIAction = "RETOFFSET",
                    EncryptedData = CipherHandler.EncryptTextWithSessionKey(jsonData, UserRequest.Keys),
                    HAMCData = CipherHandler.Hmac(jsonData, UserRequest.Keys)
                });
            }
            catch (Exception)
            {
                return ErrorResponse("GSP141", "Error encrypting payload");
            }
            return Put(address, "RETOFFSET", payload);
        }




        public Response Save(string address, string jsonData)
        {
            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayload()
                {
                    APIAction = "RETSAVE",
                    EncryptedData = CipherHandler.EncryptTextWithSessionKey(jsonData, UserRequest.Keys),
                    HAMCData = CipherHandler.Hmac(jsonData, UserRequest.Keys)
                });
            }
            catch (Exception)
            {
                return ErrorResponse("GSP141", "Error encrypting payload");
            }
            return Put(address, "RETSAVE", payload);
        }



        public Response Submit(string address, string jsonData)
        {
            string payload;
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayload()
                {
                    APIAction = "RETSUBMIT",
                    EncryptedData = CipherHandler.EncryptTextWithSessionKey(jsonData, UserRequest.Keys),
                    HAMCData = CipherHandler.Hmac(jsonData, UserRequest.Keys)
                });
            }
            catch (Exception)
            {
                return ErrorResponse("GSP141", "Error encrypting payload");
            }
            return Post(address, "RETSUBMIT", payload);
        }






        public Response File(string address, string jsonData, string version, string returnType, string signatureId, string signature = "")
        {
            string payload;
            var signatureType = string.IsNullOrEmpty(signature) ? "DSC" : "EVC";
            try
            {
                payload = JsonConvert.SerializeObject(new RequestPayloadForFiling()
                {
                    APIAction = "RETFILE",
                    EncryptedData = CipherHandler.EncryptTextWithSessionKey(jsonData, UserRequest.Keys),
                    Signature = signature,
                    SignatureId = signatureId,
                    SignatureType = signatureType,
                    HeaderJson = DefaultHeaderJson(version, returnType)
                });
            }
            catch (Exception)
            {
                return ErrorResponse("GSP141", "Error encrypting payload");
            }
            return Put(address, "RETFILE", payload);
        }



        private object DefaultHeaderJson(string version, string returnType)
        {
            return new
            {
                username = UserRequest.Header.Username,
                state_cd = UserRequest.Header.StateCode,
                gstin = UserRequest.Header.GSTN,
                auth_token = UserRequest.Header.AuthToken,
                ip_usr = UserRequest.Header.IPAddress,
                Txn = UserRequest.Header.TransactionId,
                api_version = version,
                rtn_typ = returnType,
                user_role = "Client",
                ret_period = UserRequest.Header.ReturnPeriod,
                client_id = "17xx449c021341dd4bebb9290cc7ea013877"
            };
        }
    }
}
