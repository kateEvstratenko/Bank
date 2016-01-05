using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;

namespace ClientApi.Controllers
{
//    [CheckToken]
    public class DepositController : ApiController
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService iDepositService)
            : base()
        {
            _depositService = iDepositService;
        }

        // GET api/deposit
//        [Route("Get")]
        public CustomPagedList<ShortDeposit> Get([FromUri]int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            var result = Mapper.Map<CustomPagedList<ShortDeposit>>(_depositService.GetAll(pageNumber, pageSize));
            return result;
        }

        [Route("GetAll")]
        public List<ShortDeposit> GetAll()
        {
            var result = _depositService.GetAll();
            return result;
        }

        // GET api/deposit/5
//        [Route("GetById")]
        public DomainDeposit Get(int id)
        {
            return _depositService.Get(id);
        }
    }
}
