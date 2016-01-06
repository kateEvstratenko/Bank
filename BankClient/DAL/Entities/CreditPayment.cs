using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace DAL.Entities
{
    public class CreditPayment : IBaseEntity
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelayMainSum { get; set; }
        public double DelayPercentSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CreditPaymentPlanItemId { get; set; }
        public int SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public virtual CreditPaymentPlanItem CreditPaymentPlanItem { get; set; }
        [InverseProperty("SourceCreditPayments")]
        public virtual Bill SourceBill { get; set; }
        [InverseProperty("DestinationCreditPayments")]
        public virtual Bill DestinationBill { get; set; }
    }
}