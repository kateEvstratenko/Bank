using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITokenRepository TokenRepository { get; }
        ICreditRequestRepository CreditRequestRepository { get; }
        ICreditRepository CreditRepository { get; }
        IDepositRepository DepositRepository { get; }
        ICustomerCreditRepository CustomerCreditRepository { get; }
        ICustomerDepositRepository CustomerDepositRepository { get; }
        ICreditPaymentPlanItemRepository CreditPaymentPlanItemRepository { get; }
        ICreditPaymentRepository CreditPaymentRepository { get; }
        IBillRepository BillRepository { get; }
        void SaveChanges();
        void Reload<T>(T entity) where T : class, IBaseEntity;
    }
}