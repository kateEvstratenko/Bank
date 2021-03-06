﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DomainCredit : IDomainBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PercentRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
        public int PaymentTypeId { get; set; }
        public DomainPaymentType PaymentType { get; set; }
        public ICollection<DomainCreditRequest> CreditRequests { get; set; }
        public ICollection<DomainCustomerCredit> CustomerCredits { get; set; }
    }
}
