using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCreditPayment : DomainBaseEntity
    {
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelaySum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerCreditId { get; set; }
        public int? SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public DomainCustomerCredit CustomerCredit { get; set; }
        public DomainBill SourceBill { get; set; }
        public DomainBill DestinationBill { get; set; }
    }
}
