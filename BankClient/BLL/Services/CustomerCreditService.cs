using System;
using System.Configuration;
using System.IO;
using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
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
                    CustomerId = creditRequest.CustomerId,
                    Sum = creditRequest.Sum
                },
                CreditRequestId = creditRequestId
            };
            var bankBill = Uow.BillRepository.GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber"));
            bankBill.Sum -= creditRequest.Sum;

            var paymentPlan = new CalculationCreditService().CalculatePaymentPlan(credit.CreditSum,
                creditRequest.Credit.PercentRate, creditRequest.MonthCount, credit.StartDate).ToList();
            credit.CreditPaymentPlanItems = paymentPlan;

            Uow.CustomerCreditRepository.Add(Mapper.Map<CustomerCredit>(credit));
            Uow.SaveChanges();

            credit.Customer = Mapper.Map<DomainCustomer>(Uow.CustomerRepository.Get(credit.CustomerId));
            credit.Credit = Mapper.Map<DomainCredit>(Uow.CreditRepository.Get(credit.CreditId));
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
        public CustomPagedList<DomainCustomerCredit> GetAll(int pageNumber, int pageSize)
        {
            var credits = Uow.CustomerCreditRepository.GetAll();
            var domainCredits = Mapper.Map<CustomPagedList<DomainCustomerCredit>>(credits.ToCustomPagedList(pageNumber, pageSize));
            return domainCredits;
        }

        public CustomPagedList<DomainCustomerCredit> GetAll(int customerId, int pageNumber, int pageSize)
        {
            var credits = Uow.CustomerCreditRepository.GetAll().Where(c => c.CustomerId == customerId);
            var domainCredits = Mapper.Map<CustomPagedList<DomainCustomerCredit>>(credits.ToCustomPagedList(pageNumber, pageSize));
            return domainCredits;
        }

        public CustomPagedList<DomainCustomerCredit> GetAllByUser(string userId, int pageNumber, int pageSize)
        {
            var user = Uow.AppUserRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
            var credits = Uow.CustomerCreditRepository.GetAll().Where(cc => cc.CustomerId == user.CustomerId);
            var domainCredits = Mapper.Map<CustomPagedList<DomainCustomerCredit>>(credits.ToCustomPagedList(pageNumber, pageSize));
            return domainCredits;
        }

        public DomainCustomerCredit GetByContractNumber(string contractNumber)
        {
            var userCredit = Uow.CustomerCreditRepository.GetByContractNumber(contractNumber);
            if (userCredit == null)
            {
                throw BankClientException.ThrowUserCreditNotFound();
            }
            return Mapper.Map<DomainCustomerCredit>(userCredit);
        }

        public string GetContract(string contractNumber, string baseUrl)
        {
            return string.Format("{0}/Content/CreditContracts/{1}.docx", baseUrl, contractNumber);
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