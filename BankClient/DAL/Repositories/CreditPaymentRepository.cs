using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CreditPaymentRepository : BaseRepository<CreditPayment>, ICreditPaymentRepository
    {
        public CreditPaymentRepository(BankContext context)
            : base(context)
        {
        }
    }
}

