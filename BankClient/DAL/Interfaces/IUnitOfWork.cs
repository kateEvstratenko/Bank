namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ITokenRepository TokenRepository { get; }
        ICreditRepository CreditRepository { get; }
        void SaveChanges();
    }
}