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
            var startPay = DateTime.Now.AddDays(ProjectConstants.DayCountForStartPay);
            var customerCredit = _iUnitOfWork.CustomerCreditRepository.GetByContractNumber(contractNumber);
            var currentPaymentPlan = customerCredit
                .CreditPaymentPlanItems
                .FirstOrDefault(x => !x.IsPaid && startPay > x.StartDate);

            if (currentPaymentPlan == null)
            {
                throw BankClientException.ThrowNotPayment();
            }

//            var sourceBill = customerCredit.Bill;
            var destinationBill = _iUnitOfWork.BillRepository
                .GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber"));
            destinationBill.Sum += sum;
            var payment = CalculatePayment(currentPaymentPlan, sum);
//            payment.SourceBillId = sourceBill.Id;
            payment.DestinationBillId = destinationBill.Id;

            _iUnitOfWork.CreditPaymentRepository.Add(payment);

            currentPaymentPlan.IsPaid = isPaid(currentPaymentPlan, payment);

            _iUnitOfWork.SaveChanges();

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
                CreditPaymentPlanItemId = plannedPayment.Id,
                Currency = Currency.Blr,
                DateTime = DateTime.Now
            };
        }

        private bool isPaid(CreditPaymentPlanItem plannedPayment, CreditPayment realPayment)
        {
            var isPaidMainSum = plannedPayment.MainSum == realPayment.MainSum &&
                    plannedPayment.PercentSum == realPayment.PercentSum;
            var isPaidDebtSum = true;

            if (plannedPayment.Debt != null)
            {
                isPaidDebtSum = plannedPayment.Debt.MainSum == realPayment.DelayMainSum &&
                    plannedPayment.Debt.PercentSum == realPayment.DelayPercentSum;
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
            return a - b > 0 ? a - b : 0;
        }
    }
}