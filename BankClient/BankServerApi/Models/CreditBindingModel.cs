using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using BankServerApi.CustomAttributes;
using BLL.Models;

namespace BankServerApi.Models
{
    public class CreditBindingModel
    {
        [Required]
        [Display(Name = "Название")]
        [StringLength(100, ErrorMessage = "Поле {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [StringLength(1000, ErrorMessage = "Поле {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Процентная ставка")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public double PercentRate { get; set; }

        [Required]
        [Display(Name = "Минимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public double MinSum { get; set; }

        [Required]
        [Display(Name = "Максимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        [GreaterThan("MinSum", "Максимальная сумма должна быть больше минимальной.")]
        public double MaxSum { get; set; }

        [Required]
        [Display(Name = "Минимальный период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public int MinMonthPeriod { get; set; }

        [Required]
        [Display(Name = "Максимальный период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        [GreaterThan("MinMonthPeriod", "Максимальный период должен быть больше минимального.")]
        public int MaxMonthPeriod { get; set; }

        [Required]
        [Display(Name = "ИД типа платежа")]
        public int PaymentTypeId { get; set; }
    }
}