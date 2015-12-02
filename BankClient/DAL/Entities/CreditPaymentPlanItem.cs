using System;
using Core.Enums;

namespace DAL.Entities
{
    public class CreditPaymentPlanItem : IBaseEntity
    {
        public CreditPaymentPlanItem() { }
        public CreditPaymentPlanItem(double mainSum, double percentSum, double delaySum, Currency currency, DateTime startDate)
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
        public bool IsPaid { get; set; }

        public int CustomerCreditId { get; set; }
        public virtual CustomerCredit CustomerCredit { get; set; }
        //ICollection<Payment>
    }
}