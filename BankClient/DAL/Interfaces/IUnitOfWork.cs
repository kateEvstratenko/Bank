namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITokenRepository TokenRepository { get; }
        ICreditRequestRepository CreditRequestRepository { get; }
        void SaveChanges();
    }
}