using System.Web.Http;
using AutoMapper;
using BankServerApi.Models;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;

namespace ClientApi.Controllers
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

        public CustomPagedList<ShortCustomerDeposit> GetByCustomerId(int? page = null)
        {
            var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return Mapper.Map<CustomPagedList<ShortCustomerDeposit>>(_customerDepositService.GetAllByUser(tokenObj.UserId, pageNumber, pageSize));
        }
    }
}
