using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CreditRequest;
using BankServerApi.DataObjects.Responses.CreditRequest;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CreditRequest")]
    public class CreditRequestController : ApiController
    {
        private readonly UserManager<AppUser> _userManager = Startup.UserManagerFactory();
        private readonly RoleManager<IdentityRole> _roleManager = Startup.RoleManagerFactory();
        private readonly ICreditRequestService _iCreditRequestService;
        
        public CreditRequestController(ICreditRequestService iCreditRequestService)
        {
            _iCreditRequestService = iCreditRequestService;
        }

        [HttpPost]
        [Route("Add")]
        [CheckAppToken(Roles = new[] { AppRoles.Operator })]
        public IHttpActionResult Add(AddCreditRequest request)
        {
            try
            {
                var baseLocalhostUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                var baseUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                var militaryArr = request.MilitaryId != null ? Convert.FromBase64String(request.MilitaryId) : null;
                var docPath = _iCreditRequestService.Add(Mapper.Map<DomainCreditRequest>(request), 
                    militaryArr, Convert.FromBase64String(request.IncomeCertificate),
                    request.Email, baseUrl, baseLocalhostUrl);
                return Ok(docPath);
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

        /// <summary>
        /// Необработанные заявки для роли
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnconfirmed")]
        [CheckAppToken(Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.Security })]
        public IHttpActionResult GetUnconfirmed(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                var roleName = _userManager.GetRoles(tokenObj.UserId).FirstOrDefault();
                var role = _roleManager.FindByName(roleName);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmed(role, pageNumber, pageSize);
                return Ok(new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(unconfirmedCreditRequests)
                });
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

        /// <summary>
        /// Обработанные конкретным работником заявки, для которых еще не установлен статус начальника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetConfirmed")]
        [CheckAppToken(Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.Security })]
        public IHttpActionResult GetConfirmed(int? page = null)
        {
            try
            {
                var chiefRole = _roleManager.Roles.FirstOrDefault(r => r.Name == AppRoles.CreditDepartmentChief.ToString());
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var сonfirmedCreditRequests = _iCreditRequestService.GetConfirmed(tokenObj.UserId, chiefRole, pageNumber, pageSize);
                return Ok(new GetConfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(сonfirmedCreditRequests)
                });
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

        /// <summary>
        /// Необработанные начальником, но обработанные остальными заявки
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnconfirmedByChief")]
        [CheckAppToken(Roles = new[] { AppRoles.CreditDepartmentChief })]
        public IHttpActionResult GetUnconfirmedByChief(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                var roleName = _userManager.GetRoles(tokenObj.UserId).FirstOrDefault();
                var role = _roleManager.FindByName(roleName);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmedByChief(role, pageNumber, pageSize);
                return Ok(new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(unconfirmedCreditRequests)
                });
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

        /// <summary>
        /// Обработанные конкретным начальником заявки, по которым еще не выдан кредит
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetConfirmedByChief")]
        [CheckAppToken(Roles = new[] { AppRoles.CreditDepartmentChief, AppRoles.Operator })]
        public IHttpActionResult GetConfirmedByChief(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var сonfirmedCreditRequests = _iCreditRequestService.GetConfirmedByChief(tokenObj.UserId, pageNumber, pageSize);
                return Ok(new GetConfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(сonfirmedCreditRequests)
                });
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

        [HttpPost]
        [Route("SetStatus")]
        [CheckAppToken(Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.CreditDepartmentChief, AppRoles.Security })]
        public IHttpActionResult SetStatus(SetStatusRequest request)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                _iCreditRequestService.SetStatus(tokenObj.UserId,
                    request.CreditRequestId, request.CreditRequestStatusInfo, request.Message);
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

        [Route("GetContract")]
        public IHttpActionResult GetContract([FromUri]int id)
        {
            try
            {
                var baseLocalhostUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                var contract = _iCreditRequestService.GetContract(id, baseLocalhostUrl);
                return Ok(contract);
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
