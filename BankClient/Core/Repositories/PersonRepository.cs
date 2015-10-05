using Core.Interfaces;
using DAL;
using DAL.Entities;

namespace Core.Repositories
{
    public class PersonRepository: BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(BankContext context) : base(context)
        {
        }
    }
}