using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BankServerApi.DataObjects.Responses;
using BLL.Helpers;
using BLL.Interfaces;
using Core;
using Core.Enums;
using Microsoft.AspNet.Identity;

namespace BankServerApi
{
    public class CheckAppTokenAttribute : ActionFilterAttribute
    {
//        public int Order { get; set; }
        public AppRoles[] Roles { get; set; }
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
                var token = actionContext.Request.Headers.First(p => p.Key.ToLower() == "token").Value.First();
                var parsedToken = authenticationService.CheckToken(token);
                actionContext.Request.Properties.Add("tokenObj", parsedToken);

                if (Roles != null)
                {
                    var userManager = Startup.UserManagerFactory();
                    var tokenObj = new ParsedTokenHelper().GetParsedToken(actionContext.Request.Properties);
                    var userId = tokenObj.UserId;
                    if (Roles.Any(role => userManager.IsInRole(userId, role.ToString())))
                    {
                        return;
                    }
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
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
}
