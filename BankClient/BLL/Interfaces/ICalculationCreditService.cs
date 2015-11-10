using BLL.Models;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICalculationCreditService
    {
        IEnumerable<DomainCreditPaymentPlan> CalculatePaymentPlan(double sum,
            double percentRate, int monthPeriod, DateTime startDate);
        object CalculateSolvencyRate(double sum, double percentRate, int monthPeriod,
            double incomeSum, double otherCreditPayments, double utilitiesPayments, double otherPayments);

        double CalculateMaxCreditSum(double percentRate, int monthPeriod, double incomeSum, 
            double otherCreditPayments, double utilitiesPayments, double otherPayments);

        double CalculateIncomeForCredit(double sum, double percentRate, int monthPeriod,
            double otherCreditPayments, double utilitiesPayments, double otherPayments);
    }
}
