using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Customer GetCustomerByUserId(string userId);
    }
}