using System.Linq;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerCreditService
    {
        void Add(int creditRequestId);
        void Delete(int id);
        void Update(DomainCustomerCredit credit);
        DomainCustomerCredit Get(int id);
        IQueryable<DomainCustomerCredit> GetAll();
        IQueryable<DomainCustomerCredit> GetAllByUser(string userId);
    }
}