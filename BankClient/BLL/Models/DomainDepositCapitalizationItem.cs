using Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainDepositCapitalizationItem : IDomainBaseEntity
    {
        public DomainDepositCapitalizationItem() { }

        public DomainDepositCapitalizationItem(double mainSum, double percentSum, Currency currency, DateTime startDate)
        {
            MainSum = mainSum;
            PercentSum = percentSum;
            Currency = currency;
            StartDate = startDate;
        }

        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
    }
}
