using System.Web.Http;
using BLL.Classes;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    [CheckToken]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _iCustomerService;
        public CustomerController(ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }

        public ShortCustomer Get([FromUri]string documentNum)
        {
            return _iCustomerService.GetByDocumentNumber(documentNum);
        }
    }
}
