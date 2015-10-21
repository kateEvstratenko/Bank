using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BaseRepository<T>: IRepository<T> where T: class, IBaseEntity
    {
        protected readonly BankContext Context;
        public BaseRepository(BankContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public T Get(int id)
        {
            return Entities.FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        } 

        private DbSet<T> Entities
        {
            get { return Context.Set<T>(); }
        } 
    }
}