using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CustomerDeposit;
using BankServerApi.Models;
using BLL.Classes;
using BLL.Models;
using BLL.Interfaces;
using Core.Enums;

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

        public CustomPagedList<ShortCustomerDeposit> Get(int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(pageNumber, pageSize));
        }

        public CustomPagedList<ShortCustomerDeposit> GetByCustomerId(int customerId, int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAll(customerId, pageNumber, pageSize));
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
