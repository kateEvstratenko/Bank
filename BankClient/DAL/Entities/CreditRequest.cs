namespace DAL.Entities
{
    public class CreditRequest : BaseEntity
    {
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public Currency Currency { get; set; }

        public int PersonId { get; set; }
        public int CreditId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Credit Credit { get; set; }
    }
}