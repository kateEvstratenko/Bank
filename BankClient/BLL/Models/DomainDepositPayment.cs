using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainDepositPayment : DomainBaseEntity
    {
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerDepositId { get; set; }
        public int SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public DomainCustomerDeposit CustomerDeposit { get; set; }
        public DomainBill SourceBill { get; set; }
        public DomainBill DestinationBill { get; set; }  
    }
}
