using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Person : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public DocumentType DocumentType { get; set; }
        public string IdenticationNumber { get; set; } 
        public virtual Address Address { get; set; }
        public virtual BankClient BankClient { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<CreditRequest> CreditRequests { get; set; }
        public ICollection<PersonCredit> UserCredits { get; set; } 
    }
}
