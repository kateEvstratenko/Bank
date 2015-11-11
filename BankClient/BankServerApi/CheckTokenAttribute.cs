using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BLL.Interfaces;
using Core;
using Core.Enums;
using DataObjects.Requests.CreditRequest;
using DataObjects.Responses;
using Microsoft.AspNet.Identity;

namespace BankServerApi
{
    public class CheckTokenAttribute : ActionFilterAttribute
    {
        public int Order { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var requestScope = actionContext.Request.GetDependencyScope();

                var authenticationService = requestScope.GetService(typeof(IAuthenticationService))
                    as IAuthenticationService;
                if (authenticationService == null)
                {
                    throw BankClientException.ThrowAutofacError("AuthenticationService is null");
                }
                var requestParams = ((AuthenticatedRequest)actionContext.ActionArguments.First().Value);
                var token = requestParams.Token;
                var parsedToken = authenticationService.CheckToken(token);
                requestParams.TokenObj = parsedToken;
            }

            catch (TokenExpiredException)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log("Token expired", "CheckToken", LogType.Warning);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.TokenExpired());
            }
            catch (BankClientException ex)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log(ex.ToString(), "CheckToken", LogType.Error);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }

            catch (Exception ex)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log(ex.ToString(), "CheckToken", LogType.Error);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }
        }
    }

    public class CheckRoleAttribute : ActionFilterAttribute
    {
        public int Order { get; set; }
        public AppRoles[] Roles { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var userManager = Startup.UserManagerFactory();
                var requestParams = ((AuthenticatedRequest)actionContext.ActionArguments.First().Value);
                var userId = requestParams.TokenObj.UserId;
                if (Roles.Any(role => userManager.IsInRole(userId, role.ToString())))
                {
                    return;
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            catch (TokenExpiredException)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.TokenExpired());
            }
            catch (BankClientException ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }

            catch (Exception ex)
            {
               actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }
        }
    } 
}
