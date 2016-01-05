using System.Collections.Generic;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Classes;
using BLL.Models;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    [CheckToken]
    public class CreditController : ApiController
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService iCreditService)
            : base()
        {
            creditService = iCreditService;
        }

        // GET api/credit
        public CustomPagedList<ShortCredit> Get([FromUri]int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            var result = Mapper.Map<CustomPagedList<ShortCredit>>(creditService.GetAll(pageNumber, pageSize));
            return result;
        }

        // GET api/credit/5
        public DomainCredit Get(int id)
        {
            return creditService.Get(id);
        }

        [Route("GetAll")]
        public List<ShortCredit> GetAll()
        {
            var result = creditService.GetAll();
            return result;
        }

        // POST api/credit
        public IHttpActionResult Post([FromBody]CreditBindingModel creditModel)
        {
            var credit = Mapper.Map<DomainCredit>(creditModel);
            creditService.Add(credit);
            return Ok();
        }

        // PUT api/credit/5
        public IHttpActionResult Put(int id, [FromBody]CreditBindingModel creditModel)
        {
            var credit = Mapper.Map<DomainCredit>(creditModel);
            credit.Id = id;
            creditService.Update(credit);
            return Ok();
        }

        // DELETE api/credit/5
        public IHttpActionResult Delete(int id)
        {
            creditService.Delete(id);
            return Ok();
        }
    }
}
