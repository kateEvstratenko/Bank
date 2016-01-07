using System;
using System.Web.Http;
using BLL.Interfaces;
using Core;

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
            try
            {
                calculationDebtService.CheckPayments();
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
