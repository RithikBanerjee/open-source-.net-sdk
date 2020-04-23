using Newtonsoft.Json;

namespace GSTAPI.Models
{
    internal class RequestPayload
    {
        [JsonProperty("action")]
        public string APIAction { get; set; }
        [JsonProperty("data")]
        public string EncryptedData { get; set; }
        [JsonProperty("hmac")]
        public string HAMCData { get; set; }
    }
    internal class RequestPayloadForFiling
    {

        [JsonProperty("action")]
        public string APIAction { get; set; }
        [JsonProperty("data")]
        public string EncryptedData { get; set; }
        [JsonProperty("sign")]
        public string Signature { get; set; }
        [JsonProperty("st")]
        public string SignatureType { get; set; }
        [JsonProperty("sid")]
        public string SignatureId { get; set; }
        [JsonProperty("hdr")]
        public object HeaderJson { get; set; }
    }
}
