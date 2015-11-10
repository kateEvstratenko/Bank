using BankServerApi.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankServerApi.Models.CalculationModels
{
    public class CalculationCreditModelForPaymentPlan
    {
        [Required]
        [SumRange("CreditId")]
        public double Sum { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MonthPeriod { get; set; }

        [Required]
        public int CreditId { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}