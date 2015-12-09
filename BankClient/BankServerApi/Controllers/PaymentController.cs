﻿using System.Web.Http;
using BLL.Interfaces;
using Core.Enums;
using DataObjects.Requests.CreditRequest;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/Payment")]
    [CheckToken(Order = 0)]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("Add")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.Cashier })]
        public IHttpActionResult Add(AddPaymentRequest request)
        {
            _paymentService.Add(request.ContractNumber, request.Sum);
            return Ok();
        }
    }
}