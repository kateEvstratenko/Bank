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
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public ICollection<CreditPayment> SourceCreditPayments { get; set; } 
        public ICollection<CreditPayment> DestinationCreditPayments { get; set; }
        public ICollection<DepositPayment> SourceDepositPayments { get; set; }
        public ICollection<DepositPayment> DestinationDepositPayments { get; set; } 
    }
}