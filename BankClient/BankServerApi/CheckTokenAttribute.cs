using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BLL.Interfaces;
using Core;
using DataObjects.Requests.CreditRequest;
using DataObjects.Responses;

namespace BankServerApi
{
    public class CheckTokenAttribute : ActionFilterAttribute
    {
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
}
