using System;
using System.Web.Http;
using System.Web.Http.Results;
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
                var result = _customerCreditService.GetAllByUser(tokenObj.UserId, pageNumber, pageSize);
                return Ok(result);
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("GetContract")]
        public IHttpActionResult GetContract(string contractNumber)
        {
            try
            {
                var baseLocalhostUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                var contract = _customerCreditService.GetContract(contractNumber, baseLocalhostUrl);
                return Ok(contract);
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
