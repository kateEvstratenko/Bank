using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Interfaces;
using Core;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public static class GlobalValues
    {
        public static DateTime BankDateTime { get; set; }
    }

    public class CalculationDepositPercentService : BaseService, ICalculationDepositPercentService
    {
        private const double _fineForMainDebt = ProjectConstants.FineForMainDebt / 100;
        private const double _fineForPercentDebt = ProjectConstants.FineForPercentDebt / 100;
        private ICalculationDepositService _iCalculationDepositService;

        public CalculationDepositPercentService(IUnitOfWork uow,
            ICalculationDepositService iCalculationDepositService) : base(uow)
        {
            _iCalculationDepositService = iCalculationDepositService;
        }

        public void AddPercents()
        {
            var dateNow = DateTime.Now;
            var deposits = Uow.CustomerDepositRepository.GetAll().Include(x => x.DepositPayments).ToList();
            foreach (var deposit in deposits)
            {
                var date = deposit.DepositPayments.Last().DateTime;
                var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                var nextPayment = date.AddDays(daysInMonth);
                if (nextPayment.Date == GlobalValues.BankDateTime.Date)
                {
//                    deposit.Bill.Sum
                }
            }
        }

        public void CheckPayments()
        {
            var overdueCredits = GetOverdueCredits();
            foreach (var credit in overdueCredits)
            {
                var overduePayments = GetOverduePayments(credit);
                var overdueMainSum = 0.0;
                var overduePercentSum = 0.0;
                foreach (var item in overduePayments)
                {
                    var paidMainSum = item.CreditPayments.Sum(x => x.MainSum);
                    var paidPercentSum = item.CreditPayments.Sum(x => x.PercentSum);
                    var paidDelayMainSum = item.CreditPayments.Sum(x => x.DelayMainSum);
                    var paidDelayPercentSum = item.CreditPayments.Sum(x => x.DelayPercentSum);

                    overdueMainSum += (item.MainSum - paidMainSum) * _fineForMainDebt;
                    overduePercentSum += (item.PercentSum - paidPercentSum) * _fineForPercentDebt;

                    item.Debt = item.Debt ?? new Debt();
                    item.Debt.MainSum += overdueMainSum - paidDelayMainSum;
                    item.Debt.PercentSum += overduePercentSum - paidDelayPercentSum;
                }
            }
            Uow.SaveChanges();
        }

        private IEnumerable<CustomerCredit> GetOverdueCredits()
        {
            var now = DateTime.Now;
            return Uow.CustomerCreditRepository
                .GetAll()
                .Where(x => x.CreditPaymentPlanItems.Any(item => !item.IsPaid && now > item.StartDate))
                .ToList();
        }

        private IEnumerable<CreditPaymentPlanItem> GetOverduePayments(CustomerCredit credit)
        {
            var now = DateTime.Now;
            return credit.CreditPaymentPlanItems
                .Where(item => !item.IsPaid && now > item.StartDate)
                .ToList();
        }
    }
}