using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Credit : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PercentRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public TimeSpan MinPeriod { get; set; }
        public TimeSpan MaxPeriod { get; set; }
        public TimeSpan LoanPeriod { get; set; }
        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public ICollection<CreditRequest> CreditRequests { get; set; }
        public ICollection<CustomerCredit> CustomerCredits { get; set; }
    }
}