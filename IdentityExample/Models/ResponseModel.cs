using System.Net;

namespace IdentityExample.Models
{
    public class ResponseModel
    {
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }
        public dynamic responseData { get; set; }
    }
}
