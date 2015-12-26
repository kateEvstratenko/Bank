using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using BankServerApi.CustomAttributes;
using BLL.Models;
using Core.Enums;

namespace BankServerApi.Models
{
    public class ShortCustomerCredit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int BillId { get; set; }
        //        public DomainBill Bill { get; set; }
        //        public List<DomainCreditPaymentPlanItem> CreditPaymentPlanItems { get; set; }   
    }

    public class CreditBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        public double PercentRate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double MinSum { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [GreaterThan("MinSum", "Max sum must exceed min sum")]
        public double MaxSum { get; set; }

        [Required]
        public int MinMonthPeriod { get; set; }

        [Required]
        [GreaterThan("MinMonthPeriod", "Max period must exceed min period")]
        public int MaxMonthPeriod { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }
    }
}