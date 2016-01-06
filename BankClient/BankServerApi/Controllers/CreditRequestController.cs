using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CreditRequest;
using BankServerApi.DataObjects.Responses;
using BankServerApi.DataObjects.Responses.CreditRequest;
using BankServerApi.Models;
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
                _iCreditRequestService.Add(Mapper.Map<DomainCreditRequest>(request), request.MilitaryId, request.IncomeCertificate, request.Email, baseUrl);
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

        /// <summary>
        /// Необработанные заявки для роли
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnconfirmed")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.Security })]
        public GetUnconfirmedCreditResponse GetUnconfirmed(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                var roleName = _userManager.GetRoles(tokenObj.UserId).FirstOrDefault();
                var role = _roleManager.FindByName(roleName);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmed(role, pageNumber, pageSize);
                return new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(unconfirmedCreditRequests)
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

        /// <summary>
        /// Обработанные конкретным работником заявки, для которых еще не установлен статус начальника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetСonfirmed")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditCommitteeMember, AppRoles.Security })]
        public GetСonfirmedCreditResponse GetСonfirmed(int? page = null)
        {
            try
            {
                var chiefRole = _roleManager.Roles.FirstOrDefault(r => r.Name == AppRoles.CreditDepartmentChief.ToString());
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var сonfirmedCreditRequests = _iCreditRequestService.GetСonfirmed(tokenObj.UserId, chiefRole, pageNumber, pageSize);
                return new GetСonfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(сonfirmedCreditRequests)
                };
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful<GetСonfirmedCreditResponse>(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful<GetСonfirmedCreditResponse>(ex);
            }
        }

        /// <summary>
        /// Необработанные начальником, но обработанные остальными заявки
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnconfirmedByChief")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditDepartmentChief })]
        public GetUnconfirmedCreditResponse GetUnconfirmedByChief(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);
                var roleName = _userManager.GetRoles(tokenObj.UserId).FirstOrDefault();
                var role = _roleManager.FindByName(roleName);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var unconfirmedCreditRequests = _iCreditRequestService.GetUnconfirmedByChief(role, pageNumber, pageSize);
                return new GetUnconfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(unconfirmedCreditRequests)
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

        /// <summary>
        /// Обработанные конкретным начальником заявки, по которым еще не выдан кредит
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetСonfirmedByChief")]
        [CheckRole(Order = 1, Roles = new[] { AppRoles.CreditDepartmentChief })]
        public GetСonfirmedCreditResponse GetСonfirmedByChief(int? page = null)
        {
            try
            {
                var tokenObj = new ParsedTokenHelper().GetParsedToken(Request.Properties);

                const int pageSize = 10;
                var pageNumber = page ?? 1;
                var сonfirmedCreditRequests = _iCreditRequestService.GetConfirmedByChief(tokenObj.UserId, pageNumber, pageSize);
                return new GetСonfirmedCreditResponse()
                {
                    CreditRequests = Mapper.Map<CustomPagedList<ShortCreditRequest>>(сonfirmedCreditRequests)
                };
            }
            catch (BankClientException ex)
            {
                return ResponseBase.Unsuccessful<GetСonfirmedCreditResponse>(ex);
            }
            catch (Exception ex)
            {
                return ResponseBase.Unsuccessful<GetСonfirmedCreditResponse>(ex);
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
