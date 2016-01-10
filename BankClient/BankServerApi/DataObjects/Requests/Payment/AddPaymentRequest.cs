using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace BankServerApi.DataObjects.Requests.Payment
{
    public class AddPaymentRequest
    {
        [Required]
        [Display(Name = "Номер договора")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string ContractNumber { get; set; }

        [BindRequired]
        [Display(Name = "Сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double Sum { get; set; }
    }
}