using BankServerApi.Models;
using BLL.Classes;
using Core;

namespace BankServerApi.DataObjects.Responses.CreditPayment
{
    public class SuccessfulCreditPaymentResponse : ResponseBase
    {
        ShortCreditPayment CreditPayment { get; set; }
    }
}