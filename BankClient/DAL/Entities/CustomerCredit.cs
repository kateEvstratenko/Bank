using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CustomerCredit
    {
        [Key, ForeignKey("Customer")]
        public int BillId { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual Bill Bill { get; set; }
        public ICollection<CreditPayment> Payments { get; set; } 
    }
}