using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ClientApi.Models.CalculationModels;
using Core;

namespace ClientApi.Controllers
{
//    [CheckToken]
    public class CalculationDepositController : ApiController
    {
        private readonly ICalculationDepositService calculationDepositService;

        private readonly IDepositService depositService;

        public CalculationDepositController(ICalculationDepositService iCalculationDepositService, IDepositService iDepositService)
            :base()
        {
            calculationDepositService = iCalculationDepositService;
            depositService = iDepositService;
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
