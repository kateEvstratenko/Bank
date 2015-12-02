using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class DepositService : BaseService, IDepositService
    {
        public DepositService(IUnitOfWork uow) : base(uow) { }
        public void Add(DomainDeposit domainDeposit)
        {
            var deposit = Mapper.Map<Deposit>(domainDeposit);
            Uow.DepositRepository.Add(deposit);
            Uow.SaveChanges();
        }
        public void Delete(int id)
        {
            Uow.DepositRepository.Delete(id);
            Uow.SaveChanges();
        }
        public void Update(DomainDeposit domainDeposit)
        {
            var deposit = Mapper.Map<Deposit>(domainDeposit);
            Uow.DepositRepository.Update(deposit);
            Uow.SaveChanges();
        }
        public DomainDeposit Get(int id)
        {
            var deposit = Uow.DepositRepository.Get(id);
            var domainDeposit = Mapper.Map<DomainDeposit>(deposit);
            return domainDeposit;
        }
        public IEnumerable<DomainDeposit> GetAll()
        {
            var deposits = Uow.DepositRepository.GetAll().ToList();
            var domainDeposits = Mapper.Map<List<Deposit>, List<DomainDeposit>>(deposits);
            return domainDeposits;
        }
    }
}