using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CreditPaymentPlanItemRepository : BaseRepository<CreditPaymentPlanItem>, ICreditPaymentPlanItemRepository
    {
        public CreditPaymentPlanItemRepository(BankContext context)
            : base(context)
        {
        }
    }
}
