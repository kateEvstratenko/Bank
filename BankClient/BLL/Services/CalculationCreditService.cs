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

        public IEnumerable<DomainCreditPaymentPlanItem> CalculatePaymentPlan(double sum, 
            double percentRate, int monthPeriod, DateTime startDate)
        {
            var payments = new List<DomainCreditPaymentPlanItem>();
            var paymentSum = CalculatePaymentSum(sum, percentRate, monthPeriod);
            var firstMainDept = CalculateFirstMainDebt(paymentSum, percentRate, monthPeriod);

            var firstCreditPayment = new DomainCreditPaymentPlanItem(firstMainDept, paymentSum - firstMainDept,
                Currency.Blr, startDate);
            payments.Add(firstCreditPayment);

            for (var i = 0; i < monthPeriod - 1; i++)
            {
                var nextMainDept = CalculateNextMainDebt(payments.ElementAt(i).MainSum, percentRate);
                var nextCreditPayment = new DomainCreditPaymentPlanItem(nextMainDept, paymentSum - nextMainDept,
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
                solvency = solvencyRate <= ProjectConstants.MaxSolvencyRate
            };
        }

        public double CalculateMaxCreditSum(double percentRate, int monthPeriod, 
            double incomeSum, double otherCreditPayments, double utilitiesPayments, double otherPayments)
        {
            var netIncomeSum = incomeSum - utilitiesPayments - otherPayments;
            var maxMonthlyPayment = netIncomeSum * ProjectConstants.MaxSolvencyRate - otherCreditPayments;
            var maxCreditSum = maxMonthlyPayment / CalculateAnnuityRate(percentRate, monthPeriod);
            return maxCreditSum;
        }

        public double CalculateIncomeForCredit(double sum, double percentRate, int monthPeriod,
            double otherCreditPayments, double utilitiesPayments, double otherPayments)
        {
            var paymentSum = CalculatePaymentSum(sum, percentRate, monthPeriod);
            var incomeSum = (paymentSum + otherCreditPayments) / ProjectConstants.MaxSolvencyRate + utilitiesPayments + otherPayments;
            return incomeSum;
        }
    }
}
