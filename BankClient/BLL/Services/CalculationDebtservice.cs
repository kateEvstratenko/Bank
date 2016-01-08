using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;
using Core;

namespace BLL.Services
{
    public class CalculationDebtService : BaseService, ICalculationDebtService
    {
        private const double _fineForMainDebt = ProjectConstants.FineForMainDebt / 100;
        private const double _fineForPercentDebt = ProjectConstants.FineForPercentDebt / 100;
        public CalculationDebtService(IUnitOfWork uow) : base(uow) { }

        public void CheckPayments()
        {
            var overdueCredits = GetOverdueCredits();
            foreach(var credit in overdueCredits)
            {
                var overduePayments = GetOverduePayments(credit);
                var overdueMainSum = 0.0;
                var overduePercentSum = 0.0;
                foreach(var item in overduePayments)
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
            var now = GlobalValues.BankDateTime;
            return Uow.CustomerCreditRepository
                .GetAll()
                .Where(x => x.CreditPaymentPlanItems.Any(item => !item.IsPaid && now > item.StartDate))
                .ToList();
        }

        private IEnumerable<CreditPaymentPlanItem> GetOverduePayments(CustomerCredit credit)
        {
            var now = GlobalValues.BankDateTime;
            return credit.CreditPaymentPlanItems
                .Where(item => !item.IsPaid && now > item.StartDate)
                .ToList();
        }
    }
}
