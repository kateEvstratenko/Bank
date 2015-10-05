namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        void SaveChanges();
    }
}