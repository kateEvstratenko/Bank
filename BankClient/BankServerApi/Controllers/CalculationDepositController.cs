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

        public CalculationDepositController(ICalculationDepositService iCalculationDepositService)
            :base()
        {
            calculationDepositService = iCalculationDepositService;
        }

        // GET /api/calculationdeposit/capitalizationplan?sum=SUM&percentrate=PERCENT_RATE&monthperiod=MONTH_COUNT&startdate=11-01-2015
        [Route("api/calculationdeposit/capitalizationplan")]
        public IHttpActionResult GetCapitalizationPlan([FromUri]DepositModelForCapitalizationPlan query)
        {
            try
            {
                var plan = calculationDepositService.CalculateCapitalizationPlan(query.Sum, query.PercentRate, query.MonthPeriod, query.StartDate).ToList();
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
