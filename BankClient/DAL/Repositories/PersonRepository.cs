using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PersonRepository: BaseRepository<Customer>, IPersonRepository
    {
        public PersonRepository(BankContext context) : base(context)
        {
        }
    }
}