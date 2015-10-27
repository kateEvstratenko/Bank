using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CreditService : ICreditService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public CreditService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public void Add()
        {
            
        }
    }
}