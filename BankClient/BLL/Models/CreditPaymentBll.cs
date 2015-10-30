using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;
using DAL.Entities;

namespace BLL.Models
{
    public class CreditPaymentBll : IBaseEntityBll
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelaySum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerCreditId { get; set; }
        public int? SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
        public virtual CustomerCredit CustomerCredit { get; set; }
        [InverseProperty("SourceCreditPayments")]
        public virtual Bill SourceBill { get; set; }
        [InverseProperty("DestinationCreditPayments")]
        public virtual Bill DestinationBill { get; set; }
    }
}