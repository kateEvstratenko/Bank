using System;
using System.Configuration;
using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class CustomerDepositService : BaseService, ICustomerDepositService
    {
        public CustomerDepositService(IUnitOfWork uow) : base(uow) { }
        public void Add(DomainCustomerDeposit deposit)
        {
            var customer = deposit.Customer;//_iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var customerDb = Uow.CustomerRepository.GetAll()
                .FirstOrDefault(c => c.IdentificationNumber == deposit.Customer.IdentificationNumber);
            if (customerDb == null)
            {
                customerDb = Mapper.Map<Customer>(customer);
                Uow.CustomerRepository.Add(customerDb);
                Uow.SaveChanges();
            }

            deposit.StartDate = DateTime.Now.Date;
            deposit.ContractNumber = GenerateContractNumber();
            deposit.Bill = new DomainBill
            {
                Number = GenerateBillNumber(),
                CustomerId = customerDb.Id,
                Sum = deposit.InitialSum
            };
            var bankBill = Uow.BillRepository
                .GetByNumber(ConfigurationManager.AppSettings.Get("BankBillNumber"));
            bankBill.Sum += deposit.InitialSum;

            Uow.CustomerDepositRepository.Add(Mapper.Map<CustomerDeposit>(deposit));
            Uow.SaveChanges();

            new DepositDocService().FillConcreteContract(deposit);
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
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits);
            return domainDeposits;
        }

        public CustomPagedList<DomainCustomerDeposit> GetAll(int customerId, int pageNumber, int pageSize)
        {
            var deposits = Uow.CustomerDepositRepository.GetAll().Where(d => d.CustomerId == customerId);
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits);
            return domainDeposits;
        }

        public CustomPagedList<DomainCustomerDeposit> GetAllByUser(string userId, int pageNumber, int pageSize)
        {
            var user = Uow.AppUserRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
            var deposits = Uow.CustomerDepositRepository.GetAll();
            var domainDeposits = Mapper.Map<CustomPagedList<DomainCustomerDeposit>>(deposits.ToCustomPagedList(pageNumber, pageSize));

            return domainDeposits;
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