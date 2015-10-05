using System.Collections.Generic;

namespace DAL.Entities
{
    public class PersonDeposit: BaseEntity
    {
        public string ContractNumber { get; set; }
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }

        public int PersonId { get; set; }
        public int DepositId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Deposit Deposit { get; set; }
        public virtual ICollection<DepositPayment> DepositPayments { get; set; } 
    }
}