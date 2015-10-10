using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CustomerDeposit
    {
        [Key, ForeignKey("Customer")]
        public int BillId { get; set; }
        public string ContractNumber { get; set; }
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CustomerId { get; set; }
        public int DepositId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Deposit Deposit { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual ICollection<DepositPayment> DepositPayments { get; set; } 
    }
}