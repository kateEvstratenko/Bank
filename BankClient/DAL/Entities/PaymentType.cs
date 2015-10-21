using System.Collections.Generic;

namespace DAL.Entities
{
    public class PaymentType : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}