using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BankServerApi.Models;
using BLL;
using BLL.Classes;
using BLL.Helpers;
using BLL.Models;
using BLL.Interfaces;
using PagedList;

namespace BankServerApi.Controllers
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

        [Route("Get")]
        public CustomPagedList<ShortCustomerCredit> Get(int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return Mapper.Map<CustomPagedList<ShortCustomerCredit>>(_customerCreditService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("GetByCustomerId")]
        public CustomPagedList<ShortCustomerCredit> GetByCustomerId(int customerId, int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            var result = Mapper.Map<CustomPagedList<ShortCustomerCredit>>
                (_customerCreditService.GetAll(customerId, pageNumber, pageSize));
            return result;
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
