using System.Collections.Generic;
using BLL.Classes;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDepositService
    {
        void Add(DomainDeposit deposit);
        void Delete(int id);
        void Update(DomainDeposit deposit);
        DomainDeposit Get(int id);
        CustomPagedList<DomainDeposit> GetAll(int pageNumber, int pageSize);
    }
}