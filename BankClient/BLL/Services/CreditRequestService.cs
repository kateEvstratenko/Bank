using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CreditRequestService : ICreditRequestService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public CreditRequestService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public void Add(CreditRequestBll creditRequest, string userId)
        {
            var customer = _iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var creditRequestDal = Mapper.Map<CreditRequest>(creditRequest);
            creditRequest.CustomerId = customer.Id;
            _iUnitOfWork.CreditRequestRepository.Add(creditRequestDal);
            _iUnitOfWork.SaveChanges();
        }

        public void Get
    }
}