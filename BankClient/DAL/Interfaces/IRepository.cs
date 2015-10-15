using System.Linq;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        void Add(T entity);
        void Delete(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}