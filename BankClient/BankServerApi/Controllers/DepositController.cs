using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Models;
using BLL.Interfaces;
using Core;

namespace BankServerApi.Controllers
{
    [CheckToken]
    public class DepositController : ApiController
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService iDepositService)
            : base()
        {
            _depositService = iDepositService;
        }

        // GET api/deposit
        public IHttpActionResult Get(int? page = null)
        {
            try
            {
                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var result = Mapper.Map<CustomPagedList<ShortDeposit>>(_depositService.GetAll(pageNumber, pageSize));
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
                var result = _depositService.GetAll();
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

        // GET api/deposit/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_depositService.Get(id));
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

        // POST api/deposit
        public IHttpActionResult Post([FromBody]DepositBindingModel depositModel)
        {
            try
            {
                var deposit = Mapper.Map<DomainDeposit>(depositModel);
                _depositService.Add(deposit);
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

        // PUT api/deposit/5
        public IHttpActionResult Put(int id, [FromBody]DepositBindingModel depositModel)
        {
            try
            {
                var deposit = Mapper.Map<DomainDeposit>(depositModel);
                deposit.Id = id;
                _depositService.Update(deposit);
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

        // DELETE api/deposit/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _depositService.Delete(id);
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
