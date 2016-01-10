using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankServerApi.Models.CalculationModels
{
    public class CalculationCreditModelForPaymentPlan
    {
        [Required]
        [Display(Name = "Сумма")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double Sum { get; set; }

        [Required]
        [Display(Name = "Период, мес")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public int MonthCount { get; set; }

        [Required]
        [Display(Name = "ИД кредита")]
        public int CreditId { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
    }
}