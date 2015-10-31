using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCustomer : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public DomainAddress Address { get; set; }
        public DomainBankClient BankClient { get; set; }
        public ICollection<DomainBill> Bills { get; set; }
        public ICollection<DomainCreditRequest> CreditRequests { get; set; }
        public ICollection<DomainCustomerCredit> CustomerCredits { get; set; }
        public ICollection<DomainCustomerDeposit> CustomerDeposits { get; set; } 
    }
}
