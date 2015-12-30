using Core.Enums;

namespace BankServerApi.DataObjects.Requests.CreditRequest
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
