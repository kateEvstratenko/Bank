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
using Core;

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

        [HttpGet]
        [Route("GetByContractNumber")]
        public IHttpActionResult GetByContractNumber(string contractNumber)
        {
            try
            {
                var customerCredit = _customerCreditService.GetByContractNumber(contractNumber);
                return Ok(customerCredit);
            }
            catch (BankClientException ex)
            {
                var content = ResponseBase.Unsuccessful(ex);
                return Content<ResponseBase>(System.Net.HttpStatusCode.BadRequest, content);
            }
            
        }

        [HttpPost]
        // id - creditRequestId
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
