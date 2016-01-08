using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Interfaces;
using Core;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public static class GlobalValues
    {
        public static DateTime BankDateTime = DateTime.Now.Date;
    }

    public class CalculationDepositPercentService : BaseService, ICalculationDepositPercentService
    {
        private readonly ICalculationDepositService _iCalculationDepositService;

        public CalculationDepositPercentService(IUnitOfWork uow,
            ICalculationDepositService iCalculationDepositService)
            : base(uow)
        {
            _iCalculationDepositService = iCalculationDepositService;
        }

        public void AddPercents()
        {
            var deposits = Uow.CustomerDepositRepository.GetAll().Where(x => !x.IsPaid).Include(x => x.DepositPayments).ToList();
            foreach (var customerDeposit in deposits)
            {
                var date = customerDeposit.DepositPayments.Last().DateTime;
                var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                var nextPayment = date.AddDays(daysInMonth);
                if (date == customerDeposit.EndDate)
                {
                    customerDeposit.IsPaid = true;
                    continue;
                }
                if (nextPayment.Date == GlobalValues.BankDateTime.Date)
                {
                    var percentSum = _iCalculationDepositService.CalculatePercentSum(customerDeposit.Bill.Sum, customerDeposit.Deposit.InterestRate, date);
                    customerDeposit.Bill.Sum = _iCalculationDepositService.AddPercentToMainSum(customerDeposit.Bill.Sum, percentSum);
                    customerDeposit.DepositPayments.Add(new DepositPayment()
                    {
                        Currency = Currency.Blr,
                        DateTime = GlobalValues.BankDateTime,
                        DestinationBillId = customerDeposit.BillId,
                        Sum = percentSum
                    });
                }
            }
            Uow.SaveChanges();
        }
    }
}