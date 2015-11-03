using System.Collections.Generic;
using BLL.Models;
using DataObjects.Requests.CreditRequest;

namespace DataObjects.Responses.CreditRequest
{
    public class GetUnconfirmedCreditResponse: ResponseBase
    {
        public List<DomainCreditRequest> CreditRequests { get; set; }
    }
}
