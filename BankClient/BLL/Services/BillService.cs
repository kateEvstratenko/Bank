using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BillService : IBillService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        
        public BillService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public DomainBill GetByNumber(string number)
        {
            var bill = _iUnitOfWork.BillRepository.GetByNumber(number);
            return bill == null ? null : Mapper.Map<DomainBill>(bill);
        }
    }
}