using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CreditRequestRepository: BaseRepository<CreditRequest>, ICreditRequestRepository
    {
        public CreditRequestRepository(BankContext context) : base(context)
        {
        }
    }
}