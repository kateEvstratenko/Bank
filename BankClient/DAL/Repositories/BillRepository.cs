using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(BankContext context)
            : base(context)
        {
        }

        public Bill GetByNumber(string number)
        {
            return Context.Bills.FirstOrDefault(b => b.Number == number);
        }
    }
}