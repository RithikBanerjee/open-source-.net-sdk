
namespace GSTAPI.Models
{
    //basic response given to user
    public class Response
    {
        public bool Status { get; set; }
        public string OutcomeTransaction { get; set; }
        public string Data { get; set; }
    }
    //authentication response given to user
    public class AuthResponse
    {
        public bool Status { get; set; }
        public string OutcomeTransaction { get; set; }
        public string AuthToken { get; set; }
        public string SessionKey { get; set; }
    }
}
