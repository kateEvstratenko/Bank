using BLL.Models;
using Core.Enums;
using Newtonsoft.Json;

namespace DataObjects.Requests.CreditRequest
{
//    public class AuthenticatedRequest
//    {
//        public string Token { get; set; }
//
//        [JsonIgnore]
//        public DomainToken TokenObj { get; set; }
//    }

    public class SetStatusRequest
    {
        public int CreditRequestId { get; set; }
        public CreditRequestStatusInfo CreditRequestStatusInfo { get; set; }
        public string Message { get; set; }
    }
}
