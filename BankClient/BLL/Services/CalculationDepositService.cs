using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CalculationDepositService: ICalculationDepositService
    {
        public double CalculatePercentSum(double sum, double percent, DateTime date)
        {
            var daysInYear = GetDaysInYear(date);
            var daysInMonth = GetDurationTimePeriod(date);
            var percentSum = sum * percent * daysInMonth / daysInYear / 100;

            if(date.Month == 12)
            {
                daysInYear = GetDaysInYear(date.AddYears(1));
                daysInMonth = GetRemainingDays(date);
                percentSum += sum * percent * daysInMonth / daysInYear / 100;
            }

            return Math.Round(percentSum / 10) * 10;
        }

        public double AddPercentToMainSum(double mainSum, double percentSum)
        {
            return mainSum + percentSum;
        }

        public IEnumerable<DomainDepositCapitalizationItem> CalculateCapitalizationPlan(
            double sum, 
            double percentRate, 
            int monthPeriod, 
            DateTime startDate)
        {
            var mainSum = sum;
            var date = startDate;
            var capitalizations = new List<DomainDepositCapitalizationItem>();

            for (var i = 0; i < monthPeriod; i++)
            {
                var percentSum = CalculatePercentSum(mainSum, percentRate, date);
                mainSum = AddPercentToMainSum(mainSum, percentSum);

                date = GetNextTimePeriod(date);

                capitalizations.Add(new DomainDepositCapitalizationItem(mainSum, percentSum, Core.Enums.Currency.Blr, date));
            }

            return capitalizations;
        }

        private DateTime GetNextTimePeriod(DateTime date)
        {
            var daysInMonth = GetDaysInMonth(date);
            return date.AddDays(daysInMonth);
        }

        private int GetDaysInYear(DateTime date)
        {
            return DateTime.IsLeapYear(date.Year) ? 366 : 365;
        }

        private int GetDurationTimePeriod(DateTime date)
        {
            var month = date.Month;

            return month == 12 ? GetDaysBeforeEndOfMonth(date) : GetDaysInMonth(date);
        }

        private int GetDaysInMonth(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;

            return DateTime.DaysInMonth(year, month);
        }

        private int GetDaysBeforeEndOfMonth(DateTime date)
        {
            var daysInMonth = GetDaysInMonth(date);
            var currentDay = date.Day;

            return daysInMonth - currentDay + 1;
        }

        private int GetRemainingDays(DateTime date)
        {
            return date.Day - 1;
        }
    }
}
