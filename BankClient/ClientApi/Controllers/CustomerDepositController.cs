using System;
using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using Core;

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

        public IHttpActionResult GetByCustomerId(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(
                        _customerDepositService.GetAllByUser(tokenObj.UserId, pageNumber, pageSize)));
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
