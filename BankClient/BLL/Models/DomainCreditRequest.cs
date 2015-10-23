using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCreditRequest : DomainBaseEntity
    {
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public DomainCustomer Customer { get; set; }
        public DomainCredit Credit { get; set; }
    }
}
