using BankServerApi.Models;
using BLL.Classes;
using Core;

namespace BankServerApi.DataObjects.Responses.CreditPayment
{
    public class UnsuccessfulCreditPaymentResponse : ResponseBase
    {
        ShortCreditPayment CreditPayment { get; set; }
    }
}