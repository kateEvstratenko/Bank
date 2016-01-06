﻿using System.Web.Http;
using BankServerApi.DataObjects.Requests.Payment;
using BLL.Interfaces;
using Core.Enums;
using Core;
using BankServerApi.DataObjects.Responses.CreditPayment;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/Payment")]
    //[CheckToken(Order = 0)]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("Add")]
        //CheckRole(Order = 1, Roles = new[] { AppRoles.Cashier })]
        public IHttpActionResult Add(AddPaymentRequest request)
        {
            try 
            {
                _paymentService.Add(request.ContractNumber, request.Sum);
                return Ok();
            }
            catch(BankClientException ex)
            {
                var content = ResponseBase.Unsuccessful<UnsuccessfulCreditPaymentResponse>(ex);
                return Content<ResponseBase>(System.Net.HttpStatusCode.BadRequest, content);
            }
        }
    }
}
