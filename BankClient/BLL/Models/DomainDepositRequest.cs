using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainDepositRequest : DomainBaseEntity
    {
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public int CustomerId { get; set; }
        public int DepositId { get; set; }
        public virtual DomainCustomer Customer { get; set; }
        public virtual DomainDeposit Deposit { get; set; }
    }
}
