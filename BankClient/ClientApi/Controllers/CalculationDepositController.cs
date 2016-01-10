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
                //                var credit = creditService.Get(query.CreditId);
                //                validationService.ValidateSum(query.Sum, credit.MinSum, credit.MaxSum, ModelState);
                //                validationService.ValidateMonthCount(query.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);
                //                if (!ModelState.IsValid)
                //                {
                //                    return BadRequest(ModelState);
                //                }

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
