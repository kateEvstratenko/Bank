using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Deposit: IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double InterestRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
        public ICollection<DepositRequest> DepositRequests { get; set; }
        public ICollection<CustomerDeposit> CustomerDeposits { get; set; }
    }
}