﻿//using System;
//using System.Collections.Generic;
//using System.Web.Http;
//using AutoMapper;
//using BLL.Interfaces;
//using BLL.Models;
//using ClientApi.Models.CalculationModels;
//using Core;
//
//namespace ClientApi.Controllers
//{
////    [CheckToken]
//    public class CalculationCreditController : ApiController
//    {
//        private readonly ICalculationCreditService calculationCreditService;
//
//        private readonly ICreditService creditService;
//
//        public CalculationCreditController(ICalculationCreditService iCalculationCreditService, ICreditService iCreditService)
//            :base()
//        {
//            calculationCreditService = iCalculationCreditService;
//            creditService = iCreditService;
//        }
//
//        // GET /api/calculationcredit/paymentsplan?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&startdate=11-01-2015
//        [Route("api/calculationcredit/paymentsplan")]
//        public IHttpActionResult GetPaymentPlan([FromUri]CalculationCreditModelForPaymentPlan query)
//        {
//            try
//            {
//                var credit = creditService.Get(query.CreditId);
//                var payments = calculationCreditService
//                    .CalculatePaymentPlan(query.Sum, credit.PercentRate, query.MonthPeriod, query.StartDate);
//                var viewPayments =
//                    Mapper.Map<IEnumerable<DomainCreditPaymentPlanItem>, List<CreditPaymentPlanViewModel>>(payments);
//                return Ok(viewPayments);
//            }
//            catch (BankClientException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }
//
//        // GET /api/calculationcredit/solvencyrate?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&incomesum=INCOME&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
//        [Route("api/calculationcredit/solvencyrate")]
//        public IHttpActionResult GetSolvencyRate([FromUri]CalculationCreditModel query)
//        {
//            try
//            {
//                var credit = creditService.Get(query.CreditId);
//                var solvency = calculationCreditService.CalculateSolvencyRate(query.Sum, credit.PercentRate,
//                    query.MonthPeriod,
//                    query.IncomeSum, query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
//                return Ok(solvency);
//            }
//            catch (BankClientException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }
//
//        // GET /api/calculationcredit/maxsum?creditid=ID&monthperiod=MONTH_COUNT&incomesum=INCOME&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
//        [Route("api/calculationcredit/maxsum")]
//        public IHttpActionResult GetMaxCreditSum([FromUri]CalculationMaxCreditSumModel query)
//        {
//            try
//            {
//                var credit = creditService.Get(query.CreditId);
//                var maxSum = calculationCreditService.CalculateMaxCreditSum(credit.PercentRate, query.MonthPeriod,
//                    query.IncomeSum, query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
//                return Ok(maxSum);
//            }
//            catch (BankClientException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }
//
//        // GET /api/calculationcredit/income?sum=SUM&creditid=ID&monthperiod=MONTH_COUNT&utilitiespayments=UTILSUM&otherpayments=OTHERSUM
//        [Route("api/calculationcredit/income")]
//        public IHttpActionResult GetIncomeSum([FromUri]CalculationIncomeSumModel query)
//        {
//            try
//            {
//                var credit = creditService.Get(query.CreditId);
//                var incomeSum = calculationCreditService.CalculateIncomeForCredit(query.Sum, credit.PercentRate,
//                    query.MonthPeriod,
//                    query.OtherCreditPayments, query.UtilitiesPayments, query.OtherPayments);
//                return Ok(incomeSum);
//            }
//            catch (BankClientException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return InternalServerError(ex);
//            }
//        }
//    }
//}