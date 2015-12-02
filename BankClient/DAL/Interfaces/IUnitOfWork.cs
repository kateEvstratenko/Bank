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
        void SaveChanges();
    }
}