using System;
using System.Linq;
using System.Web.Http;
using BLL.Services;
using Core;
using DAL.Interfaces;

namespace ClientApi.Controllers
{
//    [CheckToken]
    public class TimeController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public TimeController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var item = _iUnitOfWork.GlobalValuesRepository.GetAll().FirstOrDefault();
                if (item != null)
                {
                    return Ok(item.BankDateTime);
                }
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
