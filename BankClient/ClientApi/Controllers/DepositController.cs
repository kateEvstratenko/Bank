using System.Web.Http;
using BLL.Classes;
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
        public CustomPagedList<DomainDeposit> Get(int? page = null)
        {
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return _depositService.GetAll(pageNumber, pageSize);
        }

        // GET api/deposit/5
        public DomainDeposit Get(int id)
        {
            return _depositService.Get(id);
        }
    }
}
