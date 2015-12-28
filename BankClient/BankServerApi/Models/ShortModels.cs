using System;
using Core.Enums;

namespace BankServerApi.Models
{
    public class ShortCustomerCredit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int BillId { get; set; }
        //        public DomainBill Bill { get; set; }
        //        public List<DomainCreditPaymentPlanItem> CreditPaymentPlanItems { get; set; }   
    }

    public class ShortCredit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PercentRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
        public int PaymentTypeId { get; set; }
    }

    public class ShortDeposit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double InterestRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
    }
}