using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        Bill GetByNumber(string number);
    }
}