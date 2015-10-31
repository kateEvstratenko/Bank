using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainDeposit : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InterestRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public TimeSpan MinPeriod { get; set; }
        public TimeSpan MaxPeriod { get; set; }
        public ICollection<DomainDepositRequest> DepositRequests { get; set; }
        public ICollection<DomainCustomerDeposit> CustomerDeposits { get; set; }
    }
}
