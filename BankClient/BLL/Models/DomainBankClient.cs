using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainBankClient
    {
        public int CustomerId { get; set; }
        public string Phone { get; set; }
        public virtual DomainCustomer Customer { get; set; }
    }
}
