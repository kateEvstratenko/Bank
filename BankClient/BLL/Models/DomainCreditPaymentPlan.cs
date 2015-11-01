using Core.Enums;
using System;

namespace BLL.Models
{
    public class DomainCreditPaymentPlan : IDomainBaseEntity
    {
        public DomainCreditPaymentPlan() { }
        public DomainCreditPaymentPlan(double mainSum, double percentSum, double delaySum, Currency currency, DateTime startDate)
        {
            MainSum = mainSum;
            PercentSum = percentSum;
            DelaySum = delaySum;
            Currency = currency;
            StartDate = startDate;
        }
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelaySum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
    }
}
