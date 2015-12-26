using System.Collections.Generic;
using BLL.Models;

namespace DataObjects.Responses.CreditRequest
{
    public class GetСonfirmedCreditResponse : ResponseBase
    {
        public List<DomainCreditRequest> CreditRequests { get; set; }
    }
}