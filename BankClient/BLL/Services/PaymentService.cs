using System;
using System.Configuration;
using System.Linq;
using BLL.Interfaces;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;
using Core;

namespace BLL.Services
{
    public class PaymentService : IPaymentService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public PaymentService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public void Add(string contractNumber, double sum)
        {
            var startPay = DateTime.Now.AddDays(ProjectConstants.DayCountForStartPay);
            var customerCredit = _iUnitOfWork.CustomerCreditRepository.GetByContractNumber(contractNumber);
            var currentPaymentPlan = customerCredit
                .CreditPaymentPlanItems
                .FirstOrDefault(x => !x.IsPaid && startPay > x.StartDate);

            var payment = CalculatePayment(currentPaymentPlan, sum);
            payment.SourceBillId = customerCredit.BillId;
            payment.DestinationBillId = _iUnitOfWork.BillRepository
                .GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber")).Id;
            // TODO: payment.DestinationBillId = getBillByNumber(ProjectConstants.BankBill);
            //
            // We need to add same bills and a method for search a bill by its unique number.
            // Also we need to store the bill of bank somewhere, that we can get it quickly. For example in Project constants..

        }

        private CreditPayment CalculatePayment(CreditPaymentPlanItem plannedPayment, double sum)
        {
            var mainSum = 0.0;
            var mainDebtSum = 0.0;
            var percentSum = 0.0;
            var percentDebtSum = 0.0;

            CalculateSum(plannedPayment.MainSum, out mainSum, ref sum);

            if (plannedPayment.Debt != null)
            {
                CalculateSum(plannedPayment.Debt.MainSum, out mainDebtSum, ref sum);
            }

            CalculateSum(plannedPayment.PercentSum, out percentSum, ref sum);

            if (plannedPayment.Debt != null)
            {
                CalculateSum(plannedPayment.Debt.PercentSum, out percentDebtSum, ref sum);
            }

            return new CreditPayment()
            {
                MainSum = mainSum,
                DelayMainSum = mainDebtSum,
                PercentSum = percentSum,
                DelayPercentSum = percentDebtSum,
                Currency = Currency.Blr,
                DateTime = DateTime.Now
            };
        }

        private void CalculateSum(double plannedSum, out double currentSum, ref double sum)
        {
            currentSum = sum >= plannedSum ? plannedSum : sum;
            sum = UnsignedSub(sum, plannedSum);
        }

        private double UnsignedSub(double a, double b)
        {
            return a - b > 0 ? a - b : 0;
        }
    }
}