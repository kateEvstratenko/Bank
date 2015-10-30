using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PaymentService : IPaymentService 
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public PaymentService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public void Add()
        {
            
        }
    }
}