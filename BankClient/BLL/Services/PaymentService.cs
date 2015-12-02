using System;
using System.Linq;
using BLL.Interfaces;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PaymentService : IPaymentService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public PaymentService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        //1.Пеня. 2.Просроченные проценты. 3. Срочные проценты. 4.Просроченный основной долг. 5.Срочный основной долг
        public void Add(string contractNumber, double sum)
        {
            var customerCredit = _iUnitOfWork.CustomerCreditRepository.GetByContractNumber(contractNumber);
            customerCredit.CreditPaymentPlanItems.Where(x => !x.IsPaid).ToList();
            var payment = new CreditPayment()
            {
                Currency = Currency.Blr,
                DateTime = DateTime.Now
            };
//            customerCredit.Payments.Add();
        }

        private void CalculateSum(CustomerCredit customerCredit, double incomeSum)
        {
            //оплачено по основному долгу
            var mainSumPaid = customerCredit.Payments.Sum(p => p.MainSum);
            //осталось по основному долгу
            var remain = customerCredit.CreditSum - mainSumPaid;
        }
    }
}