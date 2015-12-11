using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
using DataObjects.Requests.CreditRequest;
using DataObjects.Responses;
using DataObjects.Responses.CreditRequest;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankServerApi.Controllers
{
    [RoutePrefix("api/CreditRequest")]
    [CheckToken(Order = 0)]
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
        [CheckRole(Order = 1, Roles = new[] { AppRoles.Operator })]
        public ResponseBase Add(AddCreditRequest request)
        {
            try
            {
                var baseUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                _iCreditRequestService.Add(Mapper.Map<DomainCreditRequest>(request), request.MilitaryId, request.IncomeCertificate, baseUrl);
                return new ResponseBase();
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
        }
        [HttpPost]
        [Route("GetUnconfirmed")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.CreditDepartmentChief, AppRoles.Security })]
        public GetUnconfirmedCreditResponse GetUnconfirmed()
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                var roleName = _userManager.GetRoles(tokenObj.UserId).FirstOrDefault();
                var role = _roleManager.FindByName(roleName);
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmed(role);
                return new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = unconfirmedCreditRequests
                };
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful<GetUnconfirmedCreditResponse>(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful<GetUnconfirmedCreditResponse>(ex);
            }
        }

        [HttpPost]
        [Route("SetStatus")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.CreditDepartmentChief, AppRoles.Security })]
        public ResponseBase SetStatus(SetStatusRequest request)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                _iCreditRequestService.SetStatus(tokenObj.UserId,
                    request.CreditRequestId, request.CreditRequestStatusInfo, request.Message);
                return new ResponseBase();
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful(ex);
            }
        }
    }
}
