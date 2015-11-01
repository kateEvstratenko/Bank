using BLL;
using BLL.Services;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculate = new CalculationCreditService();
            var payments = calculate.CalculatePaymentPlan(3000000, 38, 24, DateTime.Now);
            foreach(var i in payments)
            {
                Console.WriteLine(i.StartDate.ToShortDateString() + ' ' + 
                    (i.MainSum + i.PercentSum) + ' ' + i.MainSum + ' ' + i.PercentSum);
            }
        }
    }
}
