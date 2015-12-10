using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;
using Core.Enums;
using DataObjects.Requests.CustomerDeposit;

namespace BankServerApi.Controllers
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

        public IEnumerable<DomainCustomerDeposit> Get()
        {
            return _customerDepositService.GetAll();
        }

        public IEnumerable<DomainCustomerDeposit> GetByCustomerId(int customerId)
        {
            return _customerDepositService.GetAll().Where(c => c.CustomerId == customerId).ToList();
        }

        [HttpPost]
        [Route("Add")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.Operator })]
        public IHttpActionResult Add(AddDepositRequest request)
        {
            _customerDepositService.Add(Mapper.Map<DomainCustomerDeposit>(request));             
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            _customerDepositService.Delete(id);
            return Ok();
        }
    }
}
