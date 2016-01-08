using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class GlobalValuesRepository : BaseRepository<GlobalValues>, IGlobalValuesRepository
    {
        public GlobalValuesRepository(BankContext context)
            : base(context)
        {
        }
    }
}