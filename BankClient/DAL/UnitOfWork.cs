using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BankContext _context;
        private IPersonRepository _iPersonRepository;
        private ITokenRepository _iTokenRepository;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository
        {
            get { return (_iPersonRepository ?? (_iPersonRepository = new PersonRepository(_context))); }
        }

        public ITokenRepository TokenRepository
        {
            get { return (_iTokenRepository ?? (_iTokenRepository = new TokenRepository(_context))); }
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}