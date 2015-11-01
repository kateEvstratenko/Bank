using System;
using System.ComponentModel.DataAnnotations;

namespace BankServerApi.Models.CalculationModels
{
    public class CalculationCreditModelForPaymentPlan
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Sum { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MonthPeriod { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double PercentRate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}