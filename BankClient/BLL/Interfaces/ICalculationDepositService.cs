using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICalculationDepositService
    {
        double CalculatePercentSum(double sum, double percent, DateTime date);
        double AddPercentToMainSum(double mainSum, double percentSum);
        IEnumerable<DomainDepositCapitalizationItem> CalculateCapitalizationPlan(
            double sum,
            double percentRate,
            int monthPeriod,
            DateTime startDate);
    }
}
