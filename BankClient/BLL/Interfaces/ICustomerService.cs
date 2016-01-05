using BLL.Classes;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        ShortCustomer GetByDocumentNumber(string number);
    }
}