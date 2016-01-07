using System;
using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using Core;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CustomerCredit")]
    [CheckAppToken]
    public class CustomerCreditController : ApiController
    {
        private readonly ICustomerCreditService _customerCreditService;

        public CustomerCreditController(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        [Route("Get")]
        public IHttpActionResult Get(int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerCredit>>(_customerCreditService.GetAll(pageNumber, pageSize)));
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
        [Route("GetByCustomerId")]
        public IHttpActionResult GetByCustomerId(int customerId, int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var result = Mapper.Map<CustomPagedList<ShortCustomerCredit>>
                    (_customerCreditService.GetAll(customerId, pageNumber, pageSize));
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

        [HttpPost]
        // id - creditRequestId
        public IHttpActionResult Add([FromBody]int id)
        {
            try
            {
                _customerCreditService.Add(id);
                return Ok();
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

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _customerCreditService.Delete(id);
                return Ok();
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
