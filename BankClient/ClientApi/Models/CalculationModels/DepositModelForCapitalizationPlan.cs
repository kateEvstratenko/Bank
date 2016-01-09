using System;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models.CalculationModels
{
    public class DepositModelForCapitalizationPlan
    {
        [Required]
        [Display(Name = "Сумма")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double Sum { get; set; }

        [Required]
        [Display(Name = "Процентная ставка")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public double PercentRate { get; set; }

        [Required]
        [Display(Name = "Период, мес")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}")]
        public int MonthPeriod { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
    }
}