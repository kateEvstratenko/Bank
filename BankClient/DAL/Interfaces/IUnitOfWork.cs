namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ITokenRepository TokenRepository { get; }
        void SaveChanges();
    }
}