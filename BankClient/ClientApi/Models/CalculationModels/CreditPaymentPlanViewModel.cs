using System;
using Core.Enums;

namespace ClientApi.Models.CalculationModels
{
    public class CreditPaymentPlanViewModel
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelaySum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
    }
}