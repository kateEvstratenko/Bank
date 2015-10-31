using System.Linq;
using System.Collections.Generic;
using AutoMapper;
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
        public IQueryable<DomainCredit> GetAll()
        {
            var credits = Uow.CreditRepository.GetAll().ToList();
            var domainCredits = new List<DomainCredit>();
            foreach (var credit in credits)
            {
                domainCredits.Add(Mapper.Map<DomainCredit>(credit));
            }
            return domainCredits.AsQueryable();
        }
    }
}