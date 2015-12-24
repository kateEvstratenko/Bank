using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class CustomerCreditService : BaseService, ICustomerCreditService
    {
        public CustomerCreditService(IUnitOfWork uow) : base(uow) { }
        public void Add(int creditRequestId)
        {
            var creditRequest = Uow.CreditRequestRepository.Get(creditRequestId);
            var credit = new DomainCustomerCredit()
            {
                CreditId = creditRequest.CreditId,
                CreditSum = creditRequest.Sum,
                Currency = creditRequest.Currency,
                CustomerId = creditRequest.CustomerId,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddMonths(creditRequest.MonthCount),
                ContractNumber = GenerateContractNumber(),
                Bill = new DomainBill()
                {
                    Number = GenerateBillNumber(),
                    CustomerId = creditRequest.CustomerId
                }
            };

            var paymentPlan = new CalculationCreditService().CalculatePaymentPlan(credit.CreditSum,
                creditRequest.Credit.PercentRate, creditRequest.MonthCount, credit.StartDate).ToList();
            credit.CreditPaymentPlanItems = paymentPlan;

            Uow.CustomerCreditRepository.Add(Mapper.Map<CustomerCredit>(credit));
            Uow.SaveChanges();

            new CreditDocService().FillConcreteContract(credit);
        }
        public void Delete(int id)
        {
            Uow.CustomerCreditRepository.Delete(id);
            Uow.SaveChanges();
        }
        public void Update(DomainCustomerCredit domainCredit)
        {
            var credit = Mapper.Map<CustomerCredit>(domainCredit);
            Uow.CustomerCreditRepository.Update(credit);
            Uow.SaveChanges();
        }
        public DomainCustomerCredit Get(int id)
        {
            var credit = Uow.CustomerCreditRepository.Get(id);
            var domainCredit = Mapper.Map<DomainCustomerCredit>(credit);
            return domainCredit;
        }
        public IQueryable<DomainCustomerCredit> GetAll()
        {
            var credits = Uow.CustomerCreditRepository.GetAll().ToList();
            var domainCredits = Mapper.Map<List<CustomerCredit>, List<DomainCustomerCredit>>(credits);
            return domainCredits.AsQueryable();
        }

        public IQueryable<DomainCustomerCredit> GetAllByUser(string userId)
        {
            var user = Uow.AppUserRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
            var credits = Uow.CustomerCreditRepository.GetAll().Where(cc => cc.CustomerId == user.CustomerId).ToList();
            var domainCredits = Mapper.Map<List<CustomerCredit>, List<DomainCustomerCredit>>(credits);
            return domainCredits.AsQueryable();
        }

        private string GenerateContractNumber()
        {
            return RandomHelper.GetRandomString(10);
        }

        private string GenerateBillNumber()
        {
            return RandomHelper.GetRandomString(10);
        }
    }
}