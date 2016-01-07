using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCreditPayment : IDomainBaseEntity
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelayMainSum { get; set; }
        public double DelayPercentSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CreditPaymentPlanItemId { get; set; }
        public int? SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public DomainCreditPaymentPlanItem CreditPaymentPlanItem { get; set; }
        public DomainBill SourceBill { get; set; }
        public DomainBill DestinationBill { get; set; }
    }
}
