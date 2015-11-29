using System.Collections.Generic;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDepositService
    {
        void Add(DomainDeposit deposit);
        void Delete(int id);
        void Update(DomainDeposit deposit);
        DomainDeposit Get(int id);
        IEnumerable<DomainDeposit> GetAll();
    }
}