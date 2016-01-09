using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CreditRequestStatusRepository : BaseRepository<CreditRequestStatus>, ICreditRequestStatusRepository
    {
        public CreditRequestStatusRepository(BankContext context)
            : base(context)
        {
        }
    }
}