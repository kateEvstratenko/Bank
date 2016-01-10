using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
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
        [BindRequired]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        [Display(Name = "ИД заявки на кредит")]
        public int CreditRequestId { get; set; }

        [BindRequired]
        [Display(Name = "Статус")]
        [DefaultValue(-1)]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        public CreditRequestStatusInfo CreditRequestStatusInfo { get; set; }

        [Display(Name = "Сообщение")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Message { get; set; }
    }
}
