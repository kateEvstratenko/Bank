using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class CreditService : BaseService, ICreditService
    {
        public CreditService(IUnitOfWork uow) : base(uow) { }
        public void Add(DomainCredit domainCredit)
        {
            var credit = Mapper.Map<Credit>(domainCredit);
            Uow.CreditRepository.Add(credit);
            Uow.SaveChanges();
        }
        public void Delete(int id)
        {
            Uow.CreditRepository.Delete(id);
            Uow.SaveChanges();
        }
        public void Update(DomainCredit domainCredit)
        {
            var credit = Mapper.Map<Credit>(domainCredit);
            Uow.CreditRepository.Update(credit);
            Uow.SaveChanges();
        }
        public DomainCredit Get(int id)
        {
            var credit = Uow.CreditRepository.Get(id);
            var domainCredit = Mapper.Map<DomainCredit>(credit);
            return domainCredit;
        }
        public CustomPagedList<DomainCredit> GetAll(int pageNumber, int pageSize)
        {
            var credits = Uow.CreditRepository.GetAll();
            var domainCredits = Mapper.Map<CustomPagedList<DomainCredit>>(credits.ToCustomPagedList(pageNumber, pageSize));
            return domainCredits;
        }
        public List<ShortCredit> GetAll()
        {
            var credits = Uow.CreditRepository.GetAll().ToList();
            var domainCredits = Mapper.Map<List<ShortCredit>>(credits);
            return domainCredits;
        }
    }
}