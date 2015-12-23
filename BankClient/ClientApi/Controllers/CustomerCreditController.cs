using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
{
    [RoutePrefix("api/CustomerCredit")]
    [CheckToken(Order = 0)]
    public class CustomerCreditController : ApiController
    {
        private readonly ICustomerCreditService _customerCreditService;

        public CustomerCreditController(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public IEnumerable<DomainCustomerCredit> GetByCustomerId(int customerId)
        {
            return _customerCreditService.GetAll().Where(c => c.CustomerId == customerId).ToList();
        }
    }
}
