using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CustomerDepositRepository : BaseRepository<CustomerDeposit>, ICustomerDepositRepository
    {
        public CustomerDepositRepository(BankContext context)
            : base(context)
        {
        }

        public CustomerDeposit GetByContractNumber(string contractNumber)
        {
            return Context.CustomerDeposits.FirstOrDefault(c => c.ContractNumber == contractNumber);
        }
    }
}