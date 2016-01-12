using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http.ModelBinding;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CustomerDepositService : BaseService, ICustomerDepositService
    {
        private readonly ICustomerService _iCustomerService;
        private readonly IValidationService _iValidationService;
        public CustomerDepositService(IUnitOfWork uow, ICustomerService iCustomerService, IValidationService iValidationService) : base(uow)
        {
            _iCustomerService = iCustomerService;
            _iValidationService = iValidationService;
        }

        public CustomerDepositResult Add(DomainCustomerDeposit customerDeposit, int monthCount, string email, string baseLocalhostUrl, ModelStateDictionary modelState)
        {
            var domainDeposit = Mapper.Map<DomainDeposit>(Uow.DepositRepository.Get(customerDeposit.DepositId));
            _iValidationService.ValidateSum(customerDeposit.InitialSum, domainDeposit.MinSum, domainDeposit.MaxSum, modelState, true);
            _iValidationService.ValidateMonthCount(monthCount, domainDeposit.MinMonthPeriod, domainDeposit.MaxMonthPeriod, modelState);

            customerDeposit.StartDate = GlobalValues.BankDateTime;
            customerDeposit.EndDate = customerDeposit.StartDate.AddMonths(monthCount);
            if (!modelState.IsValid)
            {
                return new CustomerDepositResult
                {
                    ModelState = modelState
                };
            }
            
            var customer = customerDeposit.Customer;//_iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var customerDb = Uow.CustomerRepository.GetAll()
                .FirstOrDefault(c => c.IdentificationNumber == customerDeposit.Customer.IdentificationNumber);
            if (customerDb == null)
            {
                var id = _iCustomerService.Register(customer, email);
                customerDeposit.CustomerId = id;
            }
            else
            {
                customerDeposit.CustomerId = customerDb.Id;
            }

            customerDeposit.StartDate = GlobalValues.BankDateTime;
            customerDeposit.ContractNumber = GenerateContractNumber();
            customerDeposit.Bill = new DomainBill
            {
                Number = GenerateBillNumber(),
                CustomerId = customerDb.Id,
                Sum = customerDeposit.InitialSum
            };

            customerDeposit.DepositPayments = new List<DomainDepositPayment>()
            {
                new DomainDepositPayment()
                {
                    Currency = Currency.Blr,
                    DateTime = customerDeposit.StartDate,
                    Sum = customerDeposit.InitialSum,
                    DestinationBill = customerDeposit.Bill
                }
            };
            var bankBill = Uow.BillRepository
                .GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber"));
            bankBill.Sum += customerDeposit.InitialSum;

            var dbDeposit = Mapper.Map<CustomerDeposit>(customerDeposit);
            dbDeposit.Customer = null;
            Uow.CustomerDepositRepository.Add(dbDeposit);
            Uow.SaveChanges();
            
//            Uow.Reload(dbDeposit);
//            deposit = Mapper.Map<DomainCustomerDeposit>(Uow.DepositRepository.Get(dbDeposit.Id));
            customerDeposit.Deposit = Mapper.Map<DomainDeposit>(Uow.DepositRepository.Get(customerDeposit.DepositId));
            new DepositDocService().FillConcreteContract(customerDeposit);
            return new CustomerDepositResult()
            {
                ModelState = null,
                DocPath = GetContract(dbDeposit.ContractNumber, baseLocalhostUrl)
            };
        }

        public void Delete(int id)
        {
            Uow.CustomerDepositRepository.Delete(id);
            Uow.SaveChanges();
        }

        public void Update(DomainCustomerDeposit domainCredit)
        {
            var deposit = Mapper.Map<CustomerDeposit>(domainCredit);
            Uow.CustomerDepositRepository.Update(deposit);
            Uow.SaveChanges();
        }

        public DomainCustomerDeposit Get(int id)
        {
            var deposit = Uow.CustomerDepositRepository.Get(id);
            var domainDeposit = Mapper.Map<DomainCustomerDeposit>(deposit);
            return domainDeposit;
        }

        public CustomPagedList<DomainCustomerDeposit> GetAll(int pageNumber, int pageSize)
        {
            var deposits = Uow.CustomerDepositRepository.GetAll();
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits.ToCustomPagedList(pageNumber, pageSize));
            return domainDeposits;
        }

        public CustomPagedList<DomainCustomerDeposit> GetAll(int customerId, int pageNumber, int pageSize)
        {
            var deposits = Uow.CustomerDepositRepository.GetAll().Where(d => d.CustomerId == customerId);
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits.ToCustomPagedList(pageNumber, pageSize));
            return domainDeposits;
        }

        public CustomPagedList<DomainCustomerDeposit> GetAllByUser(string userId, int pageNumber, int pageSize)
        {
            var user = Uow.AppUserRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
            var deposits = Uow.CustomerDepositRepository.GetAll().Where(d => d.CustomerId == user.CustomerId);
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits.ToCustomPagedList(pageNumber, pageSize));

            return domainDeposits;
        }

        public DomainCustomerDeposit GetByContractNumber(string contractNumber)
        {
            var userDeposit = Uow.CustomerDepositRepository.GetByContractNumber(contractNumber);
            if (userDeposit == null)
            {
                throw BankClientException.ThrowUserDepositNotFound();
            }
            return Mapper.Map<DomainCustomerDeposit>(userDeposit);
        }


        public string GetContract(string contractNumber, string baseUrl)
        {
            return string.Format("{0}/Content/DepositContracts/{1}.docx", baseUrl, contractNumber);
        }

        private string GenerateContractNumber()
        {
            return RandomHelper.GetRandomString(10);
        }

        private string GenerateBillNumber()
        {
            return RandomHelper.GetRandomString(10);
        }

        private void Validate(DomainCustomerDeposit customerDeposit, DomainDeposit deposit, int monthCount)
        {
            if (customerDeposit.InitialSum < deposit.MinSum)
            {
                throw BankClientException.ThrowSumLessThanMin();
            }

            if (customerDeposit.InitialSum > deposit.MaxSum)
            {
                throw BankClientException.ThrowSumMoreThanMax();
            }

            if (monthCount < deposit.MinMonthPeriod)
            {
                throw BankClientException.ThrowMonthLessThanMin();
            }

            if (monthCount > deposit.MaxMonthPeriod)
            {
                throw BankClientException.ThrowMonthMoreThanMax();
            }
        }
    }
}