using Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainCreditPaymentPlanItem: IDomainBaseEntity
    {
        public DomainCreditPaymentPlanItem() { }
        public DomainCreditPaymentPlanItem(double mainSum, double percentSum, Currency currency, DateTime startDate)
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
        public DomainDebt Debt { get; set; }
        public DomainCustomerCredit CustomerCredit { get; set; }
        public ICollection<DomainCreditPayment> CreditPayments { get; set; }
    }
}
