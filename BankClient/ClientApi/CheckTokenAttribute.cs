using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BLL.Interfaces;
using Core;

namespace ClientApi
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
//                var requestParams = ((AuthenticatedRequest)actionContext.ActionArguments.First().Value);
                var token = actionContext.Request.Headers.First(p => p.Key.ToLower() == "token").Value.First();
                var parsedToken = authenticationService.CheckToken(token);
                actionContext.Request.Properties.Add("tokenObj", parsedToken);
//                requestParams.TokenObj = parsedToken;
            }

            catch (TokenExpiredException)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log("Token expired", "CheckToken", LogType.Warning);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ResponseBase.TokenExpired());
            }
            catch (BankClientException ex)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log(ex.ToString(), "CheckToken", LogType.Error);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ResponseBase.Unsuccessful(ex));
            }

            catch (Exception ex)
            {
//                var logService = actionContext.Request.GetDependencyScope().GetService(typeof(ILogService)) as ILogService;
//                if (logService == null)
//                {
//                    throw BankClientException.ThrowAutofacError("LogService is null");
//                }
//                logService.Log(ex.ToString(), "CheckToken", LogType.Error);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ResponseBase.Unsuccessful(ex));
            }
        }
    }
}
