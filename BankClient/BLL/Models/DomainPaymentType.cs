using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainPaymentType : DomainBaseEntity
    {
        public string Name { get; set; }
        public ICollection<DomainCredit> Credits { get; set; }
    }
}
