using System.Web.Http;
using AutoMapper;
using BankServerApi.Models;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;

namespace ClientApi.Controllers
{
    [RoutePrefix("api/CustomerCredit")]
    [CheckToken(Order = 0)]
    public class CustomerCreditController : ApiController
    {
        private readonly ICustomerCreditService _customerCreditService;

        public CustomerCreditController(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public CustomPagedList<ShortCustomerCredit> GetByCustomerId(int? page = null)
        {
            var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
            const int pageSize = 10;
            var pageNumber = page ?? 1;
            return Mapper.Map<CustomPagedList<ShortCustomerCredit>>(_customerCreditService.GetAllByUser(tokenObj.UserId, pageNumber, pageSize));
        }
    }
}
