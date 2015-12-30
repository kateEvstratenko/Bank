using System.Web.Http;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
{
    //    [CheckToken]
    public class CreditController : ApiController
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService iCreditService)
            : base()
        {
            creditService = iCreditService;
        }

        // GET api/credit
        public CustomPagedList<DomainCredit> Get(int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return creditService.GetAll(pageNumber, pageSize);
        }

        // GET api/credit/5
        public DomainCredit Get(int id)
        {
            return creditService.Get(id);
        }
    }
}
