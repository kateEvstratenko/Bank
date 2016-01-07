using System;
using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using Core;

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

        public IHttpActionResult GetByCustomerId(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(Mapper.Map<CustomPagedList<ShortCustomerCredit>>(
                    _customerCreditService.GetAllByUser(tokenObj.UserId, pageNumber, pageSize)));
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
