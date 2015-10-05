using System.Collections.Generic;

namespace DAL.Entities
{
    public class PersonCredit : BaseEntity
    {
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }

        public int PersonId { get; set; }
        public int CreditId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Credit Credit { get; set; }
        public ICollection<CreditPayment> Payments { get; set; } 
    }
}