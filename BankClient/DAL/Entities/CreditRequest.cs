using System.Collections.Generic;
using Core.Enums;

namespace DAL.Entities
{
    public class CreditRequest : IBaseEntity
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
        public virtual Customer Customer { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual ICollection<CreditRequestStatus> CreditRequestStatuses { get; set; }
        public virtual ICollection<CustomerCredit> CustomerCredits { get; set; }
    }
}