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
        public IHttpActionResult GetCapitalizationPlan([FromUri]DepositModelForCapitalizationPlan query)
        {
            try
            {
                var deposit = depositService.Get(query.DepositId);
                validationService.ValidateSum(query.Sum, deposit.MinSum, deposit.MaxSum, ModelState);
                validationService.ValidateMonthCount(query.MonthCount, deposit.MinMonthPeriod, deposit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var plan = calculationDepositService.CalculateCapitalizationPlan(query.Sum, query.PercentRate, query.MonthCount, query.StartDate).ToList();
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
