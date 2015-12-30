using BLL.Classes;
using BLL.Models;

namespace DataObjects.Responses.CreditRequest
{
    public class GetСonfirmedCreditResponse : ResponseBase
    {
        public CustomPagedList<DomainCreditRequest> CreditRequests { get; set; }
    }
}