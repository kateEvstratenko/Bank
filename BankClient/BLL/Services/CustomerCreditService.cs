using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
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
                CreditSum =  creditRequest.Sum,
                Currency = creditRequest.Currency,
                CustomerId = creditRequest.CustomerId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(30*creditRequest.MonthCount),
                ContractNumber = GenerateContractNumber()
                //todo billId add
            };

            var paymentPlan = new CalculationCreditService().CalculatePaymentPlan(credit.CreditSum,
                credit.Credit.PercentRate, creditRequest.MonthCount, credit.StartDate).ToList();
            credit.CreditPaymentPlanItems = paymentPlan;

            Uow.CustomerCreditRepository.Add(Mapper.Map<CustomerCredit>(credit));
            Uow.SaveChanges();
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

        private string GenerateContractNumber()
        {
            return RandomHelper.GetRandomString(10);
        }
    }
}