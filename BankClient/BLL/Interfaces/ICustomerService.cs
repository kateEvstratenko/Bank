using BLL.Classes;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        int Register(DomainCustomer customer, string email);
        ShortCustomer GetByIdentificationNumber(string number);
    }
}