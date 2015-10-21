using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BankContext _context;
        private IAppUserRepository _iAppUserRepository;
        private ITokenRepository _iTokenRepository;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public IAppUserRepository AppUserRepository
        {
            get { return (_iAppUserRepository ?? (_iAppUserRepository = new AppUserRepository(_context))); }
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