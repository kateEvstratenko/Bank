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
                CustomerId = customerDb.Id
            };

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

        public IQueryable<DomainCustomerDeposit> GetAll()
        {
            var deposits = Uow.CustomerDepositRepository.GetAll().ToList();
            var domainDeposits = Mapper.Map<List<CustomerDeposit>, List<DomainCustomerDeposit>>(deposits);
            return domainDeposits.AsQueryable();
        }

        public IQueryable<DomainCustomerDeposit> GetAllByUser(string userId)
        {
            var user = Uow.AppUserRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
            var deposits = Uow.CustomerDepositRepository.GetAll().Where(cc => cc.CustomerId == user.CustomerId).ToList();
            var domainDeposits = Mapper.Map<List<CustomerDeposit>, List<DomainCustomerDeposit>>(deposits);
            return domainDeposits.AsQueryable();
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