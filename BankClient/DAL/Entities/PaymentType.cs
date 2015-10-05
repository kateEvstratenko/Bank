using System.Collections.Generic;

namespace DAL.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}