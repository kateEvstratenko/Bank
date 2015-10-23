using DAL.Interfaces;

namespace BLL.Services
{
    public class BaseService
    {
        protected IUnitOfWork Uow;
        protected BaseService(IUnitOfWork uow)
        {
            Uow = uow;
        }
    }
}
