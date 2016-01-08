using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCustomerCredit : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int CreditRequestId { get; set; }
        public int BillId { get; set; }
        public DomainCustomer Customer { get; set; }
        public DomainCredit Credit { get; set; }
        public DomainCreditRequest CreditRequest { get; set; }
        public DomainBill Bill { get; set; }
        public List<DomainCreditPaymentPlanItem> CreditPaymentPlanItems { get; set; }
    }
}
