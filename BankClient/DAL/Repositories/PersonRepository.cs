using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AppUserRepository: BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(BankContext context) : base(context)
        {
        }

        public AppUser GetByEmail(string email)
        {
            return Context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}