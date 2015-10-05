using System.Collections.Generic;

namespace DAL.Entities
{
    public class Deposit: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int InterestRate { get; set; }
        public ICollection<DepositRequest> DepositRequests { get; set; }
        public ICollection<PersonDeposit> PersonDeposits { get; set; }
    }
}