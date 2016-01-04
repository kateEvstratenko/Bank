using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
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
        [Route("Get")]
        public CustomPagedList<ShortCredit> Get(int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            var result = Mapper.Map<CustomPagedList<ShortCredit>>(creditService.GetAll(pageNumber, pageSize));
            return result;
        }

        [Route("GetById")]
        // GET api/credit/5
        public DomainCredit Get(int id)
        {
            return creditService.Get(id);
        }
    }
}
