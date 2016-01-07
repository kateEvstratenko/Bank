using System.Linq;
using BLL.Classes;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerCreditService
    {
        void Add(int creditRequestId);
        void Delete(int id);
        void Update(DomainCustomerCredit credit);
        DomainCustomerCredit Get(int id);
        CustomPagedList<DomainCustomerCredit> GetAll(int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerCredit> GetAll(int customerId, int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerCredit> GetAllByUser(string userId, int pageNumber, int pageSize);

        DomainCustomerCredit GetByContractNumber(string contractNumber);
    }
}