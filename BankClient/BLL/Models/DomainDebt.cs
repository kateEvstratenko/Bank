using System;

namespace BLL.Models
{
    public class DomainDebt : IDomainBaseEntity
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public DomainCreditPaymentPlanItem CreditPaymentPlanItem { get; set; }
    }
}
