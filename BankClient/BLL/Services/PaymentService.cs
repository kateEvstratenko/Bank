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

        //платеж по кредиту
        public void Add(string contractNumber, double sum)
        {
            var startPay = GlobalValues.BankDateTime.AddDays(ProjectConstants.DayCountForStartPay);
            var customerCredit = _iUnitOfWork.CustomerCreditRepository.GetByContractNumber(contractNumber);
            var currentPaymentPlan = customerCredit
                .CreditPaymentPlanItems
                .FirstOrDefault(x => !x.IsPaid && startPay > x.StartDate);

            if (currentPaymentPlan == null)
            {
                throw BankClientException.ThrowNotPayment();
            }

            var destinationBill = _iUnitOfWork.BillRepository
                .GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber"));
            destinationBill.Sum += sum;
            var payment = CalculatePayment(currentPaymentPlan, sum);
            payment.DestinationBillId = destinationBill.Id;

            _iUnitOfWork.CreditPaymentRepository.Add(payment);

            currentPaymentPlan.IsPaid = isPaid(currentPaymentPlan);

            _iUnitOfWork.SaveChanges();

        }

        private CreditPayment CalculatePayment(CreditPaymentPlanItem plannedPayment, double sum)
        {
            var mainSum = 0.0;
            var mainDebtSum = 0.0;
            var percentSum = 0.0;
            var percentDebtSum = 0.0; 

            var remainderMainSum = plannedPayment.MainSum - plannedPayment.CreditPayments.Sum(x => x.MainSum);
            CalculateSum(remainderMainSum, out mainSum, ref sum);

            if (plannedPayment.Debt != null)
            {
                var remainderMainDebtSum = plannedPayment.Debt.MainSum - plannedPayment.CreditPayments.Sum(x => x.DelayMainSum);
                CalculateSum(remainderMainDebtSum, out mainDebtSum, ref sum);
            }

            var remainderPercentSum = plannedPayment.PercentSum - plannedPayment.CreditPayments.Sum(x => x.PercentSum);
            CalculateSum(remainderPercentSum, out percentSum, ref sum);

            if (plannedPayment.Debt != null)
            {
                var remainderPercentDebtSum = plannedPayment.Debt.PercentSum - plannedPayment.CreditPayments.Sum(x => x.DelayPercentSum);
                CalculateSum(remainderPercentDebtSum, out percentDebtSum, ref sum);
            }

            return new CreditPayment()
            {
                MainSum = mainSum,
                DelayMainSum = mainDebtSum,
                PercentSum = percentSum,
                DelayPercentSum = percentDebtSum,
                CreditPaymentPlanItemId = plannedPayment.Id,
                Currency = Currency.Blr,
                DateTime = GlobalValues.BankDateTime
            };
        }

        private bool isPaid(CreditPaymentPlanItem plannedPayment)
        {
            var isPaidMainSum = plannedPayment.MainSum <= plannedPayment.CreditPayments.Sum(x => x.MainSum) &&
                    plannedPayment.PercentSum == plannedPayment.CreditPayments.Sum(x => x.PercentSum);
            var isPaidDebtSum = true;

            if (plannedPayment.Debt != null)
            {
                isPaidDebtSum = plannedPayment.Debt.MainSum == plannedPayment.CreditPayments.Sum(x => x.DelayMainSum) &&
                    plannedPayment.Debt.PercentSum == plannedPayment.CreditPayments.Sum(x => x.DelayPercentSum);
            }

            return isPaidMainSum && isPaidDebtSum;
        }

        private void CalculateSum(double plannedSum, out double currentSum, ref double sum)
        {
            currentSum = sum >= plannedSum ? plannedSum : sum;
            sum = UnsignedSub(sum, plannedSum);
        }

        private double UnsignedSub(double a, double b)
        {
            return Math.Max(a - b, 0);
        }
    }
}