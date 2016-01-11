using System;
using System.Linq;
using System.Web.Http;
using BankServerApi.Models.CalculationModels;
using BLL.Interfaces;
using Core;

namespace BankServerApi.Controllers
{
    [CheckAppToken]
    public class CalculationDepositController : ApiController
    {
        private readonly ICalculationDepositService calculationDepositService;
        private readonly IDepositService depositService;
        private readonly IValidationService validationService;

        public CalculationDepositController(ICalculationDepositService iCalculationDepositService, IDepositService depositService, IValidationService validationService)
            :base()
        {
            calculationDepositService = iCalculationDepositService;
            this.depositService = depositService;
            this.validationService = validationService;
        }

        // GET /api/calculationdeposit/capitalizationplan?sum=SUM&percentrate=PERCENT_RATE&monthperiod=MONTH_COUNT&startdate=11-01-2015
        [Route("api/calculationdeposit/capitalizationplan")]
        [HttpPost]
        public IHttpActionResult GetCapitalizationPlan(DepositModelForCapitalizationPlan request)
        {
            try
            {
                var deposit = depositService.Get(request.DepositId);
                validationService.ValidateSum(request.Sum, deposit.MinSum, deposit.MaxSum, ModelState);
                validationService.ValidateMonthCount(request.MonthCount, deposit.MinMonthPeriod, deposit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var plan = calculationDepositService.CalculateCapitalizationPlan(request.Sum, request.PercentRate, request.MonthCount, request.StartDate).ToList();
                return Ok(plan);
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
