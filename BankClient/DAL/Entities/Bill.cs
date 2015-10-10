using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Bill: BaseEntity
    {
        [Index]
        [StringLength(200)]
        public string Number { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerCredit> CustomerCredits { get; set; }
        public virtual ICollection<CustomerDeposit> CustomerDeposits { get; set; }
        public virtual ICollection<CreditPayment> SourceCreditPayments { get; set; }
        public virtual ICollection<CreditPayment> DestinationCreditPayments { get; set; }
        public virtual ICollection<DepositPayment> SourceDepositPayments { get; set; }
        public virtual ICollection<DepositPayment> DestinationDepositPayments { get; set; } 
    }
}