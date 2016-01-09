using System;
using System.Collections.Generic;
using Core.Enums;

namespace DAL.Entities
{
    public class CustomerCredit : IBaseEntity
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }

        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int CreditRequestId { get; set; }
        public int BillId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual CreditRequest CreditRequest { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual ICollection<CreditPaymentPlanItem> CreditPaymentPlanItems { get; set; }
    }
}