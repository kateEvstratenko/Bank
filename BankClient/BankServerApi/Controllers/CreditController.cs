﻿using System.Collections.Generic;
using System.Web.Http;
using BankServerApi.Models;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;

namespace BankServerApi.Controllers
{
    public class CreditController : ApiController
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService iCreditService)
            : base()
        {
            creditService = iCreditService;
        }

        // GET api/credit
        public IEnumerable<DomainCredit> Get()
        {
            return creditService.GetAll();
        }

        // GET api/credit/5
        public DomainCredit Get(int id)
        {
            return creditService.Get(id);
        }

        // POST api/credit
        public IHttpActionResult Post([FromBody]CreditBindingModel creditModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var credit = Mapper.Map<DomainCredit>(creditModel);
            creditService.Add(credit);
            return Ok();
        }

        // PUT api/credit/5
        public IHttpActionResult Put(int id, [FromBody]CreditBindingModel creditModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var credit = Mapper.Map<DomainCredit>(creditModel);
            credit.Id = id;
            creditService.Update(credit);
            return Ok();
        }

        // DELETE api/credit/5
        public IHttpActionResult Delete(int id)
        {
            creditService.Delete(id);
            return Ok();
        }
    }
}
