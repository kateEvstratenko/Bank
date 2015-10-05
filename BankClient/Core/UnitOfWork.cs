using Core.Interfaces;
using Core.Repositories;
using DAL;

namespace Core
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BankContext _context;
        private IPersonRepository _iPersonRepository;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository
        {
            get { return (_iPersonRepository ?? (_iPersonRepository = new PersonRepository(_context))); }
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}