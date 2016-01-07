using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainBill : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public double Sum { get; set; }
        public int? CustomerId { get; set; }
        public DomainCustomer Customer { get; set; }
        public ICollection<DomainCustomerCredit> CustomerCredits { get; set; }
        public ICollection<DomainCustomerDeposit> CustomerDeposits { get; set; }
        public ICollection<DomainCreditPayment> SourceCreditPayments { get; set; }
        public ICollection<DomainCreditPayment> DestinationCreditPayments { get; set; }
        public ICollection<DomainDepositPayment> SourceDepositPayments { get; set; }
        public ICollection<DomainDepositPayment> DestinationDepositPayments { get; set; } 
    }
}
