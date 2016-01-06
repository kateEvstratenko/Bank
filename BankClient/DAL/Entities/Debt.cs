using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Debt : IBaseEntity
    {
        [Key, ForeignKey("CreditPaymentPlanItem")]
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public virtual CreditPaymentPlanItem CreditPaymentPlanItem { get; set; }
    }
}
