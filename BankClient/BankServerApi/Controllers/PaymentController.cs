using System;
using System.Web.Http;
using BLL.Interfaces;
using Core.Enums;
using Core;
using BankServerApi.DataObjects.Requests.Payment;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [CheckAppToken(Roles = new[] { AppRoles.Cashier })]
        [Route("Add")]
        public IHttpActionResult Add(AddPaymentRequest request)
        {
            try 
            {
                _paymentService.Add(request.ContractNumber, request.Sum);
                return Ok();
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
