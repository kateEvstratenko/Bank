using System;
using System.Threading;
using BLL.Interfaces;

namespace BLL.Services
{
    public class DailyCalculateService
    {
        private Timer _timer;

        public DailyCalculateService()
        {
            var dateNow = DateTime.Now;
            var mitnight = dateNow.AddMinutes(1);
            var dueTime = (mitnight - dateNow).Ticks;
            _timer = new Timer(Check, null, new TimeSpan(dueTime), TimeSpan.FromMinutes(1));
        }

        private void Check(object state)
        {
            GlobalValues.BankDateTime = GlobalValues.BankDateTime.AddDays(1);
            CheckCreditPayments();
            CheckDepositPercents();
        }

        private void CheckCreditPayments()
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var calculationDebtService = scope.GetService(typeof(ICalculationDebtService)) as ICalculationDebtService;
                if (calculationDebtService != null)
                {
                    calculationDebtService.CheckPayments();
                }
            }
        }

        private void CheckDepositPercents()
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var calculationDepositPercentService =
                    scope.GetService(typeof (ICalculationDepositPercentService)) as ICalculationDepositPercentService;
                if (calculationDepositPercentService != null)
                {
                    calculationDepositPercentService.AddPercents();
                }
            }
        }
    }
}