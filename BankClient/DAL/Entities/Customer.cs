using System;
using System.Collections.Generic;
using Core.Enums;

namespace DAL.Entities
{
    public class Customer : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string IdentificationNumber { get; set; } 
        public virtual Address Address { get; set; }
        public virtual BankClient BankClient { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<CreditRequest> CreditRequests { get; set; }
        public virtual ICollection<CustomerCredit> CustomerCredits { get; set; } 
        public virtual ICollection<CustomerDeposit> CustomerDeposits { get; set; } 
    }
}
