using Core.Enums;

namespace DAL.Entities
{
    public class DepositRequest: BaseEntity
    {
        public double Sum { get; set; }
        public Currency Currency { get; set; }

        public int CustomerId { get; set; }
        public int DepositId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Deposit Deposit { get; set; }
    }
}