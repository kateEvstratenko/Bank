using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public static class DailyCalculateService
    {
        private const double Penalty = 0.1;
        private static Timer _timer;
        public static void Init()
        {
            var dateNow = DateTime.Now;
            var mitnight = dateNow.AddDays(1).Date;
            var dueTime = (mitnight - dateNow).Ticks;
            _timer = new Timer(CheckPayments, null, new TimeSpan(dueTime), TimeSpan.FromDays(1));
        }

        private static void CheckPayments(object state)
        {
            var currentDate = DateTime.Now;
            var uow = new UnitOfWork(new BankContext());
            var credits = uow.CustomerCreditRepository.GetAll().ToList();
            foreach (var credit in credits)
            {
                //неоплаченные просроченные платежи
                var unpaidExpiredItems = credit.CreditPaymentPlanItems.Where(pi => pi.IsPaid == false && currentDate > pi.StartDate);
                foreach (var item in unpaidExpiredItems)
                {
                    ReCalculatePenalty(item);
                }
            }
        }

        private static void ReCalculatePenalty(CreditPaymentPlanItem payment)
        {
            payment.DelaySum += payment.MainSum * Penalty;
        }
    }
}