using System.Linq;
using System.Web.Http.ModelBinding;
using BLL.Classes;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerDepositService
    {
        CustomerDepositResult Add(DomainCustomerDeposit customerDeposit, int monthCount, string email, string baseLocalhostUrl, ModelStateDictionary modelState);
        void Delete(int id);
        void Update(DomainCustomerDeposit deposit);
        DomainCustomerDeposit Get(int id);
        CustomPagedList<DomainCustomerDeposit> GetAll(int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerDeposit> GetAll(int customerId, int pageNumber, int pageSize);
        CustomPagedList<DomainCustomerDeposit> GetAllByUser(string userId, int pageNumber, int pageSize);
        string GetContract(string contractNumber, string baseUrl);
        DomainCustomerDeposit GetByContractNumber(string contractNumber);
    }
}