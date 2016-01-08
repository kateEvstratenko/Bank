using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCustomerDeposit : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }

        public int CustomerId { get; set; }
        public int DepositId { get; set; }
        public int BillId { get; set; }
        public DomainCustomer Customer { get; set; }
        public DomainDeposit Deposit { get; set; }
        public DomainBill Bill { get; set; }
        public ICollection<DomainDepositPayment> DepositPayments { get; set; } 
    }
}
