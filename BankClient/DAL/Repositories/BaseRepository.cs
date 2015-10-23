using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using System;

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
            if (entity != null)
            {
                Entities.Add(entity);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Delete(int id)
        {
            var entity = Entities.Find(id);
            if (entity != null)
            {
                //Entities.Attach(entity);
                Entities.Remove(entity);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                Entities.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                throw new Exception();
            }
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