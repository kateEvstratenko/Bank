using BLL.Models;

namespace BLL.Interfaces
{
    public interface IBillService
    {
        DomainBill GetByNumber(string number);
    }
}
