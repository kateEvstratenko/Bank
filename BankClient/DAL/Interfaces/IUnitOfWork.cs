namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        ITokenRepository TokenRepository { get; }
        void SaveChanges();
    }
}