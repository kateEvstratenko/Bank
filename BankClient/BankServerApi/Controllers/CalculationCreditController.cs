using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;
using BankServerApi.Models.CalculationModels;

namespace BankServerApi.Controllers
{
    public class CalculationCreditController : ApiController
    {
        private readonly ICalculationCreditService calculationCreditservice;

        public CalculationCreditController(ICalculationCreditService iCalculationCreditService)
            :base()
        {
            calculationCreditservice = iCalculationCreditService;
        }

        // GET api/calculationcredit?sum=SUM&percentrate=RATE&monthperiod=COUNT&startdate=DATE
        [Route("api/calculationcredit/paymentsplan")]
        public IHttpActionResult GetPaymentPlan([FromUri]CalculationCreditModelForPaymentPlan query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payments = calculationCreditservice
                .CalculatePaymentPlan(query.Sum, query.PercentRate, query.MonthPeriod, query.StartDate);
            var viewPayments = Mapper.Map<IEnumerable<DomainCreditPaymentPlan>, List<CreditPaymentPlanViewModel>>(payments);
            return Ok(viewPayments);
        }
        
        //GET api/calculationcredit/solvencyrate?sum=SUM&percentrate=RATE&monthperiod=COUNT&incomesum=INCOME&utilitiespayments=UTILITIES&otherpayments=OTHER
        [Route("api/calculationcredit/solvencyrate")]
        public IHttpActionResult GetSolvencyRate([FromUri]CalculationCreditModel query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var solvency = calculationCreditservice.CalculateSolvencyRate(query.Sum, query.PercentRate, query.MonthPeriod,
                query.IncomeSum, query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
            return Ok(solvency);
        }
    }
}
