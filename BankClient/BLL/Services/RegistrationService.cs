using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RegistrationService : IRegistrationService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public RegistrationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }
    }
}