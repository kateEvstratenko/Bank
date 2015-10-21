using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAppUserRepository: IRepository<AppUser>
    {
        AppUser GetByEmail(string email);
    }
}