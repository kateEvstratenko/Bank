using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
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
    }
}
