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
        private ICustomerDepositRepository _iCustomerDepositRepository;
        private ICreditPaymentPlanItemRepository _iCretitPaymentPlanItemRepository;
        private ICreditPaymentRepository _iCreditPaymentRepository;
        private IBillRepository _iBillRepository;

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

        public ICreditPaymentRepository CreditPaymentRepository
        {
            get { return (_iCreditPaymentRepository ?? (_iCreditPaymentRepository = new CreditPaymentRepository(_context))); }
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

        public ICustomerDepositRepository CustomerDepositRepository
        {
            get { return (_iCustomerDepositRepository ?? (_iCustomerDepositRepository = new CustomerDepositRepository(_context))); }
        }

        public ITokenRepository TokenRepository
        {
            get { return (_iTokenRepository ?? (_iTokenRepository = new TokenRepository(_context))); }
        }

        public ICreditRequestRepository CreditRequestRepository
        {
            get { return (_iCreditRequestRepository ?? (_iCreditRequestRepository = new CreditRequestRepository(_context))); }
        }

        public IBillRepository BillRepository
        {
            get { return (_iBillRepository ?? (_iBillRepository = new BillRepository(_context))); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Reload<T>(T entity) where T:class, IBaseEntity
        {
            _context.Entry(entity).Reload();
        }
    }
}