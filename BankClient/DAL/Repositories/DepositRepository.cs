using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class DepositRepository : BaseRepository<Deposit>, IDepositRepository
    {
        public DepositRepository(BankContext context)
            : base(context)
        {
        }
    }
}