using System;
using System.Web.Http;
using AutoMapper;
using BLL.Classes;
using BLL.Interfaces;
using Core;

namespace ClientApi.Controllers
{
//    [CheckToken]
    public class DepositController : ApiController
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService iDepositService)
            : base()
        {
            _depositService = iDepositService;
        }

        // GET api/deposit
//        [Route("Get")]
        public IHttpActionResult Get([FromUri]int? page = null)
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
//        [Route("GetById")]
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
    }
}
