using Core.Enums;
using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainCreditRequest : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public int MonthCount { get; set; }
        public Currency Currency { get; set; }
        public double IncomeSum { get; set; }
        public double OtherCreditPayments { get; set; }
        public double UtilitiesPayments { get; set; }
        public double OtherPayments { get; set; }
        public string MilitaryIdPath { get; set; }
        public string IncomeCertificatePath { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public DomainCustomer Customer { get; set; }
        public DomainCredit Credit { get; set; }
        public IList<DomainCreditRequestStatus> CreditRequestStatuses { get; set; }
        public IList<DomainCustomerCredit> CustomerCredits { get; set; }
    }
}
