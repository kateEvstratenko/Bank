using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Models;
using BLL.Interfaces;
using DataObjects.Requests.CreditRequest;

namespace BankServerApi.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IHttpActionResult Add(AddPaymentRequest request)
        {
            _paymentService.Add(request.ContractNumber, request.Sum);
            return Ok();
        }
    }
}
