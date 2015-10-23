using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CreditRepository : BaseRepository<Credit>, ICreditRepository
    {
        public CreditRepository(BankContext context)
            : base(context)
        {
        }
    }
}
