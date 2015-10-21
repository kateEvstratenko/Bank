using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Deposit: IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InterestRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public TimeSpan MinPeriod { get; set; }
        public TimeSpan MaxPeriod { get; set; }
        public ICollection<DepositRequest> DepositRequests { get; set; }
        public ICollection<CustomerDeposit> CustomerDeposits { get; set; }
    }
}