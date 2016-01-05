using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CustomerService : ICustomerService 
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CustomerService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public DomainBill GetByNumber(string number)
        {
            var bill = _iUnitOfWork.BillRepository.GetByNumber(number);
            return bill == null ? null : Mapper.Map<DomainBill>(bill);
        }

        public ShortCustomer GetByDocumentNumber(string number)
        {
            var customer = Mapper.Map<ShortCustomer>(_iUnitOfWork.CustomerRepository.GetAll().FirstOrDefault(c => c.DocumentNumber == number));
            return customer;
        }
    }
}