using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext context) : base(context)
        {
        }

        public Customer GetCustomerByUserId(string userId)
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == userId);
            return user != null ? user.Customer : null;
        }
    }
}