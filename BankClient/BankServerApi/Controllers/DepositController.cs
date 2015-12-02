using System.Collections.Generic;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    public class DepositController : ApiController
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService iDepositService)
            : base()
        {
            _depositService = iDepositService;
        }

        // GET api/deposit
        public IEnumerable<DomainDeposit> Get()
        {
            return _depositService.GetAll();
        }

        // GET api/deposit/5
        public DomainDeposit Get(int id)
        {
            return _depositService.Get(id);
        }

        // POST api/deposit
        public IHttpActionResult Post([FromBody]DepositBindingModel depositModel)
        {
            var deposit = Mapper.Map<DomainDeposit>(depositModel);
            _depositService.Add(deposit);
            return Ok();
        }

        // PUT api/deposit/5
        public IHttpActionResult Put(int id, [FromBody]DepositBindingModel depositModel)
        {
            var deposit = Mapper.Map<DomainDeposit>(depositModel);
            deposit.Id = id;
            _depositService.Update(deposit);
            return Ok();
        }

        // DELETE api/deposit/5
        public IHttpActionResult Delete(int id)
        {
            _depositService.Delete(id);
            return Ok();
        }
    }
}
