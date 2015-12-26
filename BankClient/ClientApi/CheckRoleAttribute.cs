using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BLL;
using BLL.Helpers;
using Core;
using Core.Enums;
using DataObjects.Responses;
using Microsoft.AspNet.Identity;

namespace ClientApi
{
    public class CheckRoleAttribute : ActionFilterAttribute
    {
        public int Order { get; set; }
        public AppRoles[] Roles { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
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