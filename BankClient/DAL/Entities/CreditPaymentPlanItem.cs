using System;
using Core.Enums;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class CreditPaymentPlanItem : IBaseEntity
    {
        public CreditPaymentPlanItem() { }
        public CreditPaymentPlanItem(double mainSum, double percentSum, Currency currency, DateTime startDate)
        {
            MainSum = mainSum;
            PercentSum = percentSum;
            Currency = currency;
            StartDate = startDate;
        }
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsPaid { get; set; }
        public int CustomerCreditId { get; set; }
        public virtual CustomerCredit CustomerCredit { get; set; }
        public virtual Debt Debt { get; set; }
        public virtual ICollection<CreditPayment> CreditPayments { get; set; }
    }
}