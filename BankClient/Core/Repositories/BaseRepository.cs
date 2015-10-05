using System.Data.Entity;
using System.Linq;
using DAL;
using DAL.Entities;

namespace Core.Repositories
{
    public class BaseRepository<T> where T: BaseEntity
    {
        private readonly BankContext _context;
        public BaseRepository(BankContext context)
        {
            _context = context;
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
            get { return _context.Set<T>(); }
        } 
    }
}