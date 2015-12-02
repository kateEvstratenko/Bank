using System;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CustomerCreditRepository : BaseRepository<CustomerCredit>, ICustomerCreditRepository
    {
        public CustomerCreditRepository(BankContext context)
            : base(context)
        {
        }

        public CustomerCredit GetByContractNumber(string contractNumber)
        {
            return Context.CustomerCredits.FirstOrDefault(c => c.ContractNumber == contractNumber);
        }
    }
}