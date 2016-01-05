using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServerApi.Models.CalculationModels
{
    public class CalculationIncomeSumModel
    {
        [Required]
        [Display(Name = "Сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double Sum { get; set; }

        [Required]
        [Display(Name = "Период, мес")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public int MonthPeriod { get; set; }

        [Required]
        [Display(Name = "ИД кредита")]
        public int CreditId { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Платежи по другим кредитам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double OtherCreditPayments { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Коммунальные платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double UtilitiesPayments { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Прочие платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double OtherPayments { get; set; } 
    }
}