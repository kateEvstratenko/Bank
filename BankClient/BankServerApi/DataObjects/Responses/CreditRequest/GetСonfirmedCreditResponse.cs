using BankServerApi.Models;
using BLL.Classes;

namespace BankServerApi.DataObjects.Responses.CreditRequest
{
    public class GetСonfirmedCreditResponse : ResponseBase
    {
        public CustomPagedList<ShortCreditRequest> CreditRequests { get; set; }
    }
}