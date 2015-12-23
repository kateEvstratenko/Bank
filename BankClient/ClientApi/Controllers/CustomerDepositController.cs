using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
{
    [RoutePrefix("api/CustomerDeposit")]
    [CheckToken(Order = 0)]
    public class CustomerDepositController : ApiController
    {
        private readonly ICustomerDepositService _customerDepositService;

        public CustomerDepositController(ICustomerDepositService customerDepositService)
        {
            _customerDepositService = customerDepositService;
        }

        public IEnumerable<DomainCustomerDeposit> GetByCustomerId(int customerId)
        {
            return _customerDepositService.GetAll().Where(c => c.CustomerId == customerId).ToList();
        }
    }
}
