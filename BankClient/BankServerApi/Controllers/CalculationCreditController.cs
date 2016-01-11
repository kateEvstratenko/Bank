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
        [HttpPost]
        public IHttpActionResult GetPaymentPlan(CalculationCreditModelForPaymentPlan request)
        {
            try
            {
                var credit = creditService.Get(request.CreditId);
                validationService.ValidateSum(request.Sum, credit.MinSum, credit.MaxSum, ModelState);
                validationService.ValidateMonthCount(request.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);              
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var payments = calculationCreditService
                    .CalculatePaymentPlan(request.Sum, credit.PercentRate, request.MonthCount, request.StartDate);
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
        [HttpPost]
        public IHttpActionResult GetSolvencyRate(CalculationCreditModel query)
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
        [HttpPost]
        public IHttpActionResult GetMaxCreditSum(CalculationMaxCreditSumModel request)
        {
            try
            {
                var credit = creditService.Get(request.CreditId);
                validationService.ValidateMonthCount(request.MonthCount, credit.MinMonthPeriod, credit.MaxMonthPeriod, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxSum = calculationCreditService.CalculateMaxCreditSum(credit.PercentRate, request.MonthCount,
                    request.IncomeSum, request.OtherCreditPayments, request.UtilitiesPayments, request.OtherPayments);
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
        [HttpPost]
        public IHttpActionResult GetIncomeSum(CalculationIncomeSumModel query)
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
