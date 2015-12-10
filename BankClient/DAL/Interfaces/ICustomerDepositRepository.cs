using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICustomerDepositRepository : IRepository<CustomerDeposit>
    {
        CustomerDeposit GetByContractNumber(string contractNumber);
    }
}