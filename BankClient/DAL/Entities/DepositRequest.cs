namespace DAL.Entities
{
    public class DepositRequest: BaseEntity
    {
        public double Sum { get; set; }
        public Currency Currency { get; set; }

        public int PersonId { get; set; }
        public int DepositId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Deposit Deposit { get; set; }
    }
}