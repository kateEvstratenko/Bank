using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainPaymentType : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<DomainCredit> Credits { get; set; }
    }
}
