using Core.Enums;

namespace DAL.Entities
{
    public class CreditRequest : BaseEntity
    {
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public Currency Currency { get; set; }

        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Credit Credit { get; set; }
    }
}