using System.Collections.Generic;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    // FIXME: It's a temporaty solution. I created it for testing of CalculationDebtService.
    public class CreditPaymentPlanItemController : ApiController
    {
        private readonly ICalculationDebtService calculationDebtService;

        public CreditPaymentPlanItemController(ICalculationDebtService iCalculationDebtService)
            : base()
        {
            calculationDebtService = iCalculationDebtService;
        }

        public IHttpActionResult Get()
        {
            calculationDebtService.CheckPayments();
            return Ok();
        }
    }
}
