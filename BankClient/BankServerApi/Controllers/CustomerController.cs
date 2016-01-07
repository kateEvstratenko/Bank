using System;
using System.Web.Http;
using BLL.Classes;
using BLL.Interfaces;
using Core;

namespace BankServerApi.Controllers
{
    [CheckToken]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _iCustomerService;
        public CustomerController(ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }

        public IHttpActionResult Get(string identificationNum)
        {
            try
            {
                return Ok(_iCustomerService.GetByIdentificationNumber(identificationNum));
            }
            catch (BankClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
