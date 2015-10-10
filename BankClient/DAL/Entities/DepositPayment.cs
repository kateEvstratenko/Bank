using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DepositPayment : BaseEntity
    {
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerDepositId { get; set; }
        public int SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public virtual CustomerDeposit CustomerDeposit { get; set; }
        [InverseProperty("SourceDepositPayments")]
        public virtual Bill SourceBill { get; set; }
        [InverseProperty("DestinationDepositPayments")]
        public virtual Bill DestinationBill { get; set; }   
    }
}