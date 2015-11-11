using System;
using System.Linq;
using System.Threading;
using DAL;

namespace BLL.Services
{
    public static class DailyCalculateService
    {
        private static Timer _timer;
        public static void Init()
        {
            var dateNow = DateTime.Now;
            var mitnight = dateNow.AddDays(1).Date;
            var dueTime = (mitnight - dateNow).Ticks;
            _timer = new Timer(ReCalculate, null, new TimeSpan(dueTime), TimeSpan.FromDays(1));
        }

        private static void ReCalculate(object state)
        {
            var uow = new UnitOfWork(new BankContext());
            var credits = uow.CustomerCreditRepository.GetAll().ToList();
            foreach (var credit in credits)
            {
//                credit.
            }
        }
    }
}