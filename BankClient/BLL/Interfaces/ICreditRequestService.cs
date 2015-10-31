using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(DomainCreditRequest creditRequest, string userId);
    }
}