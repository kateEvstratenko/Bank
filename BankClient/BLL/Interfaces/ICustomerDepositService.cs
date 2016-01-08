using System.Linq;
using BLL.Classes;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerDepositService
    {
        void Add(DomainCustomerDeposit deposit);
        void Delete(int id);
        void Update(DomainCustomerDeposit deposit);
        DomainCustomerDeposit Get(int id);
        CustomPagedList<DomainCustomerDeposit> GetAll(int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerDeposit> GetAll(int customerId, int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerDeposit> GetAllByUser(string userId, int pageNumber, int pageSize);
        string GetContract(string contractNumber, string baseUrl);
    }
}