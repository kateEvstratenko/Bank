using System;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Classes;
using BLL.Models;
using BLL.Interfaces;
using Core;

namespace BankServerApi.Controllers
{
    [CheckToken]
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

        // POST api/credit
        public IHttpActionResult Post([FromBody]CreditBindingModel creditModel)
        {
            try
            {
                var credit = Mapper.Map<DomainCredit>(creditModel);
                creditService.Add(credit);
                return Ok();
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

        // PUT api/credit/5
        public IHttpActionResult Put(int id, [FromBody]CreditBindingModel creditModel)
        {
            try
            {
                var credit = Mapper.Map<DomainCredit>(creditModel);
                credit.Id = id;
                creditService.Update(credit);
                return Ok();
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

        // DELETE api/credit/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                creditService.Delete(id);
                return Ok();
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
