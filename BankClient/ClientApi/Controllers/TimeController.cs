using System;
using System.Web.Http;
using BLL.Services;
using Core;

namespace ClientApi.Controllers
{
    [CheckToken]
    public class TimeController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(GlobalValues.BankDateTime);
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
