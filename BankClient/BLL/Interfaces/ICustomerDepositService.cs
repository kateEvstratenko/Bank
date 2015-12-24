using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerDepositService
    {
        void Add(DomainCustomerDeposit deposit);
        void Delete(int id);
        void Update(DomainCustomerDeposit deposit);
        DomainCustomerDeposit Get(int id);
        IQueryable<DomainCustomerDeposit> GetAll();
        IQueryable<DomainCustomerDeposit> GetAllByUser(string userId);
    }
}