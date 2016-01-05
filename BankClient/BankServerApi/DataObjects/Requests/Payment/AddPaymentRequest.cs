using System.ComponentModel.DataAnnotations;

namespace BankServerApi.DataObjects.Requests.Payment
{
    public class AddPaymentRequest
    {
        [Required]
        [Display(Name = "Номер договора")]
        public string ContractNumber { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public double Sum { get; set; }
    }
}