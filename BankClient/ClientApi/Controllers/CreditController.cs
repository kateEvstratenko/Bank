using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using BLL.Models;
using Core;

namespace ClientApi.Controllers
{
//    [CheckToken]
    [RoutePrefix("api/Credit")]
    public class CreditController : ApiController
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService iCreditService)
            : base()
        {
            creditService = iCreditService;
        }

        // GET api/credit
//        [Route("Get")]
        public IHttpActionResult Get([FromUri]int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var result = Mapper.Map<CustomPagedList<ShortCredit>>(creditService.GetAll(pageNumber, pageSize));
                return Ok(result);
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

        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var result = creditService.GetAll();
                return Ok(result);
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

//        [Route("GetById")]
        // GET api/credit/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(creditService.Get(id));
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
