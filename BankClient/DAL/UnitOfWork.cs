using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BankContext _context;
        private IAppUserRepository _iAppUserRepository;
        private ICreditRepository _iCreditRepository;
        private IDepositRepository _iDepositRepository;
        private ITokenRepository _iTokenRepository;
        private ICreditRequestRepository _iCreditRequestRepository;
        private ICustomerRepository _iCustomerRepository;
        private ICustomerCreditRepository _iCustomerCreditRepository;
        private ICreditPaymentPlanItemRepository _iCretitPaymentPlanItemRepository;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public IAppUserRepository AppUserRepository
        {
            get { return (_iAppUserRepository ?? (_iAppUserRepository = new AppUserRepository(_context))); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return (_iCustomerRepository ?? (_iCustomerRepository = new CustomerRepository(_context)));}
        }

        public ICreditPaymentPlanItemRepository CreditPaymentPlanItemRepository
        {
            get { return (_iCretitPaymentPlanItemRepository ?? (_iCretitPaymentPlanItemRepository = new CreditPaymentPlanItemRepository(_context)));  }
        }

        public ICreditRepository CreditRepository
        {
            get { return (_iCreditRepository ?? (_iCreditRepository = new CreditRepository(_context))); }
        }

        public IDepositRepository DepositRepository
        {
            get { return (_iDepositRepository ?? (_iDepositRepository = new DepositRepository(_context))); }
        }

        public ICustomerCreditRepository CustomerCreditRepository
        {
            get { return (_iCustomerCreditRepository ?? (_iCustomerCreditRepository = new CustomerCreditRepository(_context))); }
        }

        public ITokenRepository TokenRepository
        {
            get { return (_iTokenRepository ?? (_iTokenRepository = new TokenRepository(_context))); }
        }

        public ICreditRequestRepository CreditRequestRepository
        {
            get { return (_iCreditRequestRepository ?? (_iCreditRequestRepository = new CreditRequestRepository(_context))); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}