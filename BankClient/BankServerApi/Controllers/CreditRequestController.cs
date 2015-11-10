using System;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DataObjects.Requests.CreditRequest;
using DataObjects.Responses;
using DataObjects.Responses.CreditRequest;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankServerApi.Controllers
{
//    [Authorize(Roles = "Operator")] 
    [CheckToken]
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
        public ResponseBase Add(AddCreditRequest request)
        {
            try
            {
                var baseUrl = String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority);
                _iCreditRequestService.Add(Mapper.Map<DomainCreditRequest>(request), request.TokenObj.UserId, 
                    request.CreditRequest.MilitaryId, request.CreditRequest.IncomeCertificate, baseUrl);
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
//        [Authorize(Roles = "CreditCommitteeMember, Security")]
        public GetUnconfirmedCreditResponse GetUnconfirmed(AuthenticatedRequest request)
        {
            try
            {
                var roleName = _userManager.GetRoles(request.TokenObj.UserId).FirstOrDefault();
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
    }
}
