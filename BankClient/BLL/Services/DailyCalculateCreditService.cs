using System;
using System.Threading;
using BLL.Interfaces;

namespace BLL.Services
{
    public class DailyCalculateCreditService
    {
        private Timer _timer;

        public DailyCalculateCreditService()
        {
            var dateNow = DateTime.Now;
            var mitnight = dateNow.AddDays(1).Date;
            var dueTime = (mitnight - dateNow).Ticks;
            _timer = new Timer(CheckPayments, null, new TimeSpan(dueTime), TimeSpan.FromDays(1));
        }

        private void CheckPayments(object state)
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var calculationDebtService = scope.GetService(typeof (ICalculationDebtService)) as ICalculationDebtService;
                if (calculationDebtService != null)
                {
                    calculationDebtService.CheckPayments();
                }
            }
        }
    }
}