using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(CreditRequestBll creditRequest, string userId);
    }
}