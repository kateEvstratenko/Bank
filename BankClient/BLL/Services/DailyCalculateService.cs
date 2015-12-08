using System;
using System.Linq;
using System.Threading;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class DailyCalculateService
    {
        private const double Penalty = 0.1;
        private Timer _timer;
        private static DailyCalculateService _instance;
        private static readonly object SyncRoot =  new object();

        private DailyCalculateService()
        {
            Init();
        }

        public static DailyCalculateService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        return _instance ?? (_instance = new DailyCalculateService());
                    }
                }
                return _instance;
            }
        }

        public void InitService(){ }

        private void Init()
        {
            var dateNow = DateTime.Now;
            var mitnight = dateNow.AddDays(1).Date;
            var dueTime = (mitnight - dateNow).Ticks;
            _timer = new Timer(CheckPayments, null, new TimeSpan(dueTime), TimeSpan.FromDays(1));
        }

        private void CheckPayments(object state)
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

        private void ReCalculatePenalty(CreditPaymentPlanItem payment)
        {
            //payment.DelaySum += payment.MainSum * Penalty;
        }
    }
}