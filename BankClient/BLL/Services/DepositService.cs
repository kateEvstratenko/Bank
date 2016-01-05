using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
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
        public CustomPagedList<DomainDeposit> GetAll(int pageNumber, int pageSize)
        {
            var deposits = Uow.DepositRepository.GetAll();
            var domainDeposits = Mapper.Map<CustomPagedList<DomainDeposit>>(deposits.ToCustomPagedList(pageNumber, pageSize));
            return domainDeposits;
        }
        public List<ShortDeposit> GetAll()
        {
            var deposits = Uow.DepositRepository.GetAll().ToList();
            var domainDeposits = Mapper.Map<List<ShortDeposit>>(deposits);
            return domainDeposits;
        }
    }
}