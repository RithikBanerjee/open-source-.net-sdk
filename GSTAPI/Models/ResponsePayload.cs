using Newtonsoft.Json;

namespace GSTAPI.Models
{
    //portal basic response 
    public class ResponsePayload
    {
        [JsonProperty("status_cd")]
        public int StatusCode { get; set; }
        [JsonProperty("data")]
        public string EncryptedData { get; set; }
        [JsonProperty("rek")]
        public string ResponseKey { get; set; }
        [JsonProperty("hmac")]
        public string HMACData { get; set; }

    }
    //portal authentication response 
    public class AuthResponsePayload
    {
        [JsonProperty("status_cd")]
        public int StatusCode { get; set; }
        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }
        [JsonProperty("expiry")]
        public int ExpireDuration { get; set; }
        [JsonProperty("sek")]
        public string SessionKey { get; set; }
    }
    //portal error response 
    public class ErrorResponsePayload
    {
        public int status_cd { get; set; }
        public ErrorObject error { get; set; }
        public class ErrorObject
        {
            public string error_cd { get; set; }
            public string message { get; set; }
        }
    }
}
