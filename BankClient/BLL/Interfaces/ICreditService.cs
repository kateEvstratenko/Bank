using BLL.Classes;
using BLL.Models;
using System.Linq;

namespace BLL.Interfaces
{
    public interface ICreditService
    {
        void Add(DomainCredit credit);
        void Delete(int id);
        void Update(DomainCredit credit);
        DomainCredit Get(int id);
        CustomPagedList<DomainCredit> GetAll(int pageNumber, int pageSize);
    }
}
