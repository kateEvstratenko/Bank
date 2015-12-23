using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
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
        public IEnumerable<DomainCredit> Get()
        {
            return creditService.GetAll();
        }

        // GET api/credit/5
        public DomainCredit Get(int id)
        {
            return creditService.Get(id);
        }
    }
}
