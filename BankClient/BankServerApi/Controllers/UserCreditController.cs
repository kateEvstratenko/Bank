using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Models;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CustomerCredit")]
    public class CustomerCreditController : ApiController
    {
        private readonly ICustomerCreditService _customerCreditService;

        public CustomerCreditController(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public IEnumerable<DomainCustomerCredit> Get()
        {
            return _customerCreditService.GetAll();
        }

        public IEnumerable<DomainCustomerCredit> GetByCustomerId(int customerId)
        {
            return _customerCreditService.GetAll().Where(c => c.CustomerId == customerId).ToList();
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]int id)
        {
            _customerCreditService.Add(id);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            _customerCreditService.Delete(id);
            return Ok();
        }
    }
}
