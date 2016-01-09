using System;
using System.Web.Http;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CustomerDeposit;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CustomerDeposit")]
    public class CustomerDepositController : ApiController
    {
        private readonly ICustomerDepositService _customerDepositService;

        public CustomerDepositController(ICustomerDepositService customerDepositService)
        {
            _customerDepositService = customerDepositService;
        }

        [CheckAppToken]
        public IHttpActionResult Get(int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(pageNumber,
                        pageSize)));
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

        [CheckAppToken]
        public IHttpActionResult GetByCustomerId(int customerId, int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                return Ok(
                    Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(customerId,
                        pageNumber, pageSize)));
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
        [Route("Add")]
        [CheckAppToken(Roles = new[] { AppRoles.Operator })]
        public IHttpActionResult Add(AddDepositRequest request)
        {
            try
            {
                var domainCustomerDeposit = Mapper.Map<DomainCustomerDeposit>(request);
                _customerDepositService.Add(domainCustomerDeposit, request.MonthCount);
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

        [CheckAppToken]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _customerDepositService.Delete(id);
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

        [Route("GetContract")]
        [CheckAppToken]
        public IHttpActionResult GetContract(string contractNumber)
        {
            try
            {
                var baseLocalhostUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                var contract = _customerDepositService.GetContract(contractNumber, baseLocalhostUrl);
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
