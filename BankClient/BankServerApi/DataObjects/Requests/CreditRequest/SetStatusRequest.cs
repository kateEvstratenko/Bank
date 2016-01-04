using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Display(Name = "ИД заявки на кредит")]
        public int CreditRequestId { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public CreditRequestStatusInfo CreditRequestStatusInfo { get; set; }

        [Display(Name = "Сообщение")]
        public string Message { get; set; }
    }
}
