using BLL.Interfaces;
using BLL.Models;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Core;

namespace BLL.Services
{
    public class CalculationCreditService : ICalculationCreditService
    {
        private double CalculateAnnuityRate(double percentRate, int monthPeriod)
        {
            const int monthCount = ProjectConstants.MonthsInYear;
            var monthRate = (percentRate / 100) / monthCount;
            var factor = GetFactor(percentRate, monthPeriod);
            return monthRate * factor / (factor - 1);
        }

        private double CalculatePaymentSum(double sum, double percentRate, int monthPeriod)
        {
            var annuityRate = CalculateAnnuityRate(percentRate, monthPeriod);
            return sum * annuityRate;
        }

        private double CalculateFirstMainDebt(double paymentSum, double percentRate, int monthPeriod)
        {
            var factor = GetFactor(percentRate, monthPeriod);
            return paymentSum / factor;
        }

        private double CalculateNextMainDebt(double prevSum, double percentRate)
        {
            var monthlyPercentRateCoefficient = GetMonthlyPercentRateCoefficient(percentRate);
            return prevSum * monthlyPercentRateCoefficient;
        }

        private double GetMonthlyPercentRateCoefficient(double percentRate)
        {
            const int monthCount = ProjectConstants.MonthsInYear;
            return (percentRate / 100) / monthCount + 1;
        }

        private double GetFactor(double percentRate, int monthPeriod)
        {
            var monthlyPercentRateCoefficient = GetMonthlyPercentRateCoefficient(percentRate);
            return Math.Pow(monthlyPercentRateCoefficient, monthPeriod);
        }

        public IEnumerable<DomainCreditPaymentPlan> CalculatePaymentPlan(double sum, 
            double percentRate, int monthPeriod, DateTime startDate)
        {
            var payments = new List<DomainCreditPaymentPlan>();
            var delaySum = 0;
            var paymentSum = CalculatePaymentSum(sum, percentRate, monthPeriod);
            var firstMainDept = CalculateFirstMainDebt(paymentSum, percentRate, monthPeriod);

            var firstCreditPayment = new DomainCreditPaymentPlan(firstMainDept, paymentSum - firstMainDept, delaySum,
                Currency.Blr, startDate);
            payments.Add(firstCreditPayment);

            for (var i = 0; i < monthPeriod - 1; i++)
            {
                var nextMainDept = CalculateNextMainDebt(payments.ElementAt(i).MainSum, percentRate);
                var nextCreditPayment = new DomainCreditPaymentPlan(nextMainDept, paymentSum - nextMainDept, delaySum,
                    Currency.Blr, payments.ElementAt(i).StartDate.AddMonths(1));
                payments.Add(nextCreditPayment);
            }

            return payments;
        }

        public object CalculateSolvencyRate(double sum, double percentRate, int monthPeriod, 
            double incomeSum, double otherCreditPayments, double utilitiesPayments, double otherPayments)
        {
            var paymentSum = CalculatePaymentSum(sum, percentRate, monthPeriod);
            var solvencyRate = (paymentSum + otherCreditPayments) / (incomeSum - utilitiesPayments - otherPayments);
            return new
            {
                solvencyRate = solvencyRate,
                solvency = solvencyRate < ProjectConstants.MaxSolvencyRate
            };
        }
    }
}
