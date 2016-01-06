using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CustomerService : ICustomerService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmailSender _iEmailSender;

        public CustomerService(IUnitOfWork iUnitOfWork, IEmailSender iEmailSender)
        {
            _iUnitOfWork = iUnitOfWork;
            _iEmailSender = iEmailSender;
        }

        public DomainBill GetByNumber(string number)
        {
            var bill = _iUnitOfWork.BillRepository.GetByNumber(number);
            return bill == null ? null : Mapper.Map<DomainBill>(bill);
        }

        public int Register(DomainCustomer customer, string email)
        {
            var secretCode = GenerateSecretCode();

            var customerDb = Mapper.Map<Customer>(customer);
            customerDb.SecretCode = secretCode;
            _iUnitOfWork.CustomerRepository.Add(customerDb);
            _iUnitOfWork.SaveChanges();

            _iEmailSender.SendSuperSecretCode(email, secretCode);

            return customerDb.Id;
        }

        public ShortCustomer GetByIdentificationNumber(string number)
        {
            var customer = Mapper.Map<ShortCustomer>(_iUnitOfWork.CustomerRepository.GetAll().FirstOrDefault(c => c.IdentificationNumber == number));
            return customer;
        }

        private string GenerateSecretCode()
        {
            return RandomHelper.GetRandomString(10);
        }
    }
}