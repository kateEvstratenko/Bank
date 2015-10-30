using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Interfaces;
using DataObjects.Requests.CreditRequest;

namespace BankServerApi.Controllers
{
    [Authorize(Roles = "Operationer")]
    public class CreditRequestController : ApiController
    {
        private readonly ICreditRequestService _iCreditRequestService;
        public CreditRequestController(ICreditRequestService iCreditRequestService)
        {
            _iCreditRequestService = iCreditRequestService;
        }

        [HttpPost]
        public void Post(AddCreditRequest request)
        {
            _iCreditRequestService.
        }        
    }
}
