using System.Linq;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: IBaseEntity
    {
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}