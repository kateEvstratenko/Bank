using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;
using BankServerApi.Models.CalculationModels;
using System;
using Core;

namespace BankServerApi.Controllers
{
    [CheckAppToken]
    public class CalculationCreditController : ApiController
    {
        private readonly ICalculationCreditService calculationCreditService;
        private readonly ICreditService creditService;
        private readonly IValidationService validationService;

        public CalculationCreditController(ICalculationCreditService iCalculationCreditService, ICreditService iCreditService, IValidationService iValidationService)
            :base()
        {
            calculationCreditService = iCalculationCreditService;
            creditService = iCreditService;
            validationService = iValidationService;
        }

        // GET /api/calculationcredit/paymentsplan?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&startdate=11-01-2015
        [Route("api/calculationcredit/paymentsplan")]
        public IHttpActionResult GetPaymentPlan([FromUri]CalculationCreditModelForPaymentPlan query)
        {
            try
            {
                var credit = creditService.Get(query.CreditId);
                validationService.ValidateSum(query.Sum, credit.MinSum, credit.MaxSum, ModelState);
                validationService.ValidateMonthCount(query.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);              
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var payments = calculationCreditService
                    .CalculatePaymentPlan(query.Sum, credit.PercentRate, query.MonthCount, query.StartDate);
                var viewPayments =
                    Mapper.Map<IEnumerable<DomainCreditPaymentPlanItem>, List<CreditPaymentPlanViewModel>>(payments);
                return Ok(viewPayments);
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

        // GET /api/calculationcredit/solvencyrate?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&incomesum=INCOME&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
        [Route("api/calculationcredit/solvencyrate")]
        public IHttpActionResult GetSolvencyRate([FromUri]CalculationCreditModel query)
        {
            try
            {
                var credit = creditService.Get(query.CreditId);
                validationService.ValidateSum(query.Sum, credit.MinSum, credit.MaxSum, ModelState);
                validationService.ValidateMonthCount(query.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var solvency = calculationCreditService.CalculateSolvencyRate(query.Sum, credit.PercentRate,
                    query.MonthCount,
                    query.IncomeSum, query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
                return Ok(solvency);
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

        // GET /api/calculationcredit/maxsum?creditid=ID&monthperiod=MONTH_COUNT&incomesum=INCOME&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
        [Route("api/calculationcredit/maxsum")]
        public IHttpActionResult GetMaxCreditSum([FromUri]CalculationMaxCreditSumModel query)
        {
            try
            {
                var credit = creditService.Get(query.CreditId);
                validationService.ValidateMonthCount(query.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxSum = calculationCreditService.CalculateMaxCreditSum(credit.PercentRate, query.MonthCount,
                    query.IncomeSum, query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
                return Ok(maxSum);
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

        // GET /api/calculationcredit/income?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
        [Route("api/calculationcredit/income")]
        public IHttpActionResult GetIncomeSum([FromUri]CalculationIncomeSumModel query)
        {
            try
            {
                var credit = creditService.Get(query.CreditId);
                validationService.ValidateSum(query.Sum, credit.MinSum, credit.MaxSum, ModelState);
                validationService.ValidateMonthCount(query.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var incomeSum = calculationCreditService.CalculateIncomeForCredit(query.Sum, credit.PercentRate,
                    query.MonthCount,
                    query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
                return Ok(incomeSum);
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
